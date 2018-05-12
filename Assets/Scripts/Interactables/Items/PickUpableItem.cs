using UnityEngine;
using System.Collections;

public class PickUpableItem : MonoBehaviour {
	public Sprite[] sprites;
	public string itemType;
	public int damage, health;

	Transform originalParent, holdPosition;
	Rigidbody2D rb2D;
	GameObject player;
	bool beingHeld;

	void Start() {
		rb2D = GetComponent<Rigidbody2D>();
		originalParent = transform.parent;
		holdPosition = GameObject.Find("holdPosition").transform;
		player = GameObject.Find("Player");
	}

	void Update() {
		if(beingHeld) {
			transform.position = holdPosition.position;
			transform.rotation = holdPosition.rotation;

		}

	}

	/**Sets the gameobject as child of "player" and freezes all it's movement.*/
	public void PickUp(GameObject player) {
		transform.SetParent(player.transform);
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
		transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
		beingHeld = true;
		Debug.Log("Pickup " + gameObject);
	}

	/**
	 * Sets the gamobject as child of Items gameobject and allows all movement.
	 * If canThrow; adds a force to gamobject relative to input if player has horizontal input.
	 */
	public void Drop(bool canThrow) {
		transform.localPosition = new Vector2(0.5f, 0);
		gameObject.layer = LayerMask.NameToLayer("CPickupableItem");
		transform.SetParent(originalParent);
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		if(canThrow && (Input.GetAxis("Horizontal") > 0.2 || Input.GetAxis("Horizontal") < -0.2)) {
			rb2D.AddForce(Vector2.right * 500 * Mathf.Sign(Input.GetAxis("Horizontal")));
		}
		beingHeld = false;
		Debug.Log("Drop " + gameObject);
	}

	/**
	 * Breaks the item a bit when called.
	 * When health is 0 or below, the item is broken.
	 * Returns 1 if it still has health, returns 0 when it has been destroyed.
	 */
	public int Break() {
		health--;
		//Play animation and such
		GetComponent<SpriteRenderer>().sprite = sprites[Mathf.Max(0, health)];
		//Kill if health is 0 or less
		if(health <= 0) {
			GetComponent<AudioPlayer>().PlayClip(0, 1, 0.7f, 0.7f);
			if(!transform.GetChild(0).GetComponent<ParticleSystem>().isPlaying)
				transform.GetChild(0).GetComponent<ParticleSystem>().Play();
			GetComponent<Collider2D>().enabled = false;
			Invoke("Kill", 0.5f);
		}
		return health;
	}

	public int Break(int damage) {
		health -= damage - 1;
		return Break();
	}

	public void Kill() {
		CancelInvoke();
		Destroy(gameObject);
	}

	/**Returns the item type.*/
	public string GetItemType() {
		return itemType;
	}

	void QuestionExistance() {
		float Spawndist = Mathf.Abs(Vector3.Distance(gameObject.transform.position, transform.parent.position));
		float PlayDist = Mathf.Abs(Vector3.Distance(gameObject.transform.position, player.transform.position));
		//		Debug.Log ("SpawnDist: " + Spawndist + "\nPlayDist: " + PlayDist);
		if(Spawndist > 20f && PlayDist > 20f) {
			Debug.Log("Removed illegal item!");
			Kill();
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(rb2D.velocity.magnitude > 2f) {
			switch(col.gameObject.tag) {
				case "SmallCritter":
				case "JumpingCritter":
					col.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage, gameObject, 3f);
					if(itemType == "Branch")
						Break();
					break;

				case "HardCritter":
					if(itemType == "Rock")
						col.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage, gameObject, 3f);
					break;

				case "ShellMan":
					if(itemType == "Rock" && col.gameObject.GetComponent<ShellMan>().deShelled)
						col.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage, gameObject, 3f);
					break;
			}
		}
		if(gameObject.layer == LayerMask.NameToLayer("CPickupableItem"))
			Invoke("ResetLayer", 0.1f);
	}

	void ResetLayer() {
		gameObject.layer = LayerMask.NameToLayer("PickupableItem");
	}

	private void OnBecameInvisible() {
		Invoke("QuestionExistance", 20f);
	}

	private void OnBecameVisible() {
		CancelInvoke("QuestionExistance");
	}
}
