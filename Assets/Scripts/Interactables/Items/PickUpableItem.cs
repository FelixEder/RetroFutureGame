using UnityEngine;
using System.Collections;

public class PickUpableItem : MonoBehaviour {
	public Sprite[] sprites;
	public string itemType;
	public int damage, health;
	public Transform originalParent;
	
	Rigidbody2D rb2D;
	GameObject player;

	void Start() {
		rb2D = GetComponent<Rigidbody2D>();
		originalParent = transform.parent;
		player = GameObject.Find("Player");
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
		//Destroy if health is 0 or less
		if(health <= 0) {
			GetComponent<AudioPlayer>().PlayClip(0, 1, 0.7f, 0.7f);
			if(!transform.GetChild(0).GetComponent<ParticleSystem>().isPlaying)
				transform.GetChild(0).GetComponent<ParticleSystem>().Play();
			GetComponent<Collider2D>().enabled = false;
			Invoke("Destroy", 0.5f);
		}
		return health;
	}

	public int Break(int damage) {
		health -= damage - 1;
		return Break();
	}

	public void Destroy() {
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
		//Debug.Log ("SpawnDist: " + Spawndist + "\nPlayDist: " + PlayDist);
		if(Spawndist > 20f && PlayDist > 20f) {
			Debug.Log("Removed illegal item!");
			Destroy();
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
					if(itemType == "Rock" && col.gameObject.GetComponent<ShellMan>().getDeShelled())
						col.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage, gameObject, 3f);
					break;
			}
		}
		if(rb2D.velocity.magnitude < 2f && gameObject.layer == LayerMask.NameToLayer("CPickupableItem"))
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
