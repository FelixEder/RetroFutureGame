using UnityEngine;
using System.Collections;

public class PickUpableItem : MonoBehaviour {
	public Rigidbody2D rigidBody2D;
	public Transform originalParent, holdPosition;
	public float HoldPositionX = 0.341f, HoldPositionY = -0.332f;
	/**the type*/
	public string itemType;
	bool beingHeld;
	public int damage, health;
	GameObject chara;

	void Start() {
		rigidBody2D = GetComponent<Rigidbody2D> ();
		originalParent = transform.parent;
		holdPosition = GameObject.Find ("holdPosition").transform;
		chara = GameObject.Find ("Char");
	}

	void FixedUpdate() {
		if (beingHeld) {
			transform.position = holdPosition.position;
			//transform.localPosition += new Vector3 (0.1f, -0.1f, 0);
		}
		if (rigidBody2D.velocity.y < -8) {
			rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, -8);
		}
	}

	/**Sets the gameobject as child of "player" and freezes all it's movement.*/
	public void PickUp(GameObject player) {
		transform.SetParent (player.transform);
		GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
		transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, 0));
		beingHeld = true;
		Debug.Log ("Pickup " + gameObject);
		InvokeRepeating ("QuestionExistance", 20f, 15f);
	}

	/**
	 * Sets the gamobject as child of Items gameobject and allows all movement.
	 * If canThrow; adds a force to gamobject relative to input if player has horizontal input.
	 */
	public void Drop(bool canThrow) {
		transform.localPosition = new Vector2 (0.5f, 0);
		transform.SetParent (originalParent);
		GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None;
		if (canThrow && (Input.GetAxis ("Horizontal") > 0.2 || Input.GetAxis ("Horizontal") < -0.2)) {
			rigidBody2D.AddForce (Vector2.right * 500 * Mathf.Sign (Input.GetAxis ("Horizontal")));
		}
		beingHeld = false;
		Debug.Log ("Drop " + gameObject);
	}

	/**
	 * Breaks the item a bit when called.
	 * When health is 0 or below, the item is broken.
	 * Returns 1 if it still has health, returns 0 when it has been destroyed.
	 */
	public int Break() {
		health--;
		if (health <= 0)
			Kill ();
		//Play animation and such
		return health;
	}

	public void Kill() {
		Destroy (gameObject);
	}

	/**Returns the item type.*/
	public string GetItemType() {
		return itemType;
	}

	void QuestionExistance() {
		float Spawndist = Mathf.Abs (Vector3.Distance (gameObject.transform.position, transform.parent.position));
		float PlayDist = Mathf.Abs (Vector3.Distance (gameObject.transform.position, chara.transform.position));
		Debug.Log ("SpawnDist: " + Spawndist);
		Debug.Log ("PlayDist: " + PlayDist);
		if (Spawndist > 20f && PlayDist > 20f) {
			Debug.Log ("Removed illegal item!");
			Kill ();
		}
	}
}
