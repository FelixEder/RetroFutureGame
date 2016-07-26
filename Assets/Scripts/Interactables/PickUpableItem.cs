using UnityEngine;
using System.Collections;

public class PickUpableItem : MonoBehaviour {
	Rigidbody2D rigidBody2D;
	public float HoldPositionX = 0.341f, HoldPositionY = -0.332f;
	/**the type*/
	public string itemType;
	bool beingHeld;
	public int damage, health;

	void Start() {
		rigidBody2D = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate() {
		if (beingHeld) {
			this.gameObject.transform.localPosition = new Vector2 (HoldPositionX, HoldPositionY);
		}
		if (rigidBody2D.velocity.y < -8) {
			rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, -8);
		}
	}

	/**Sets the gameobject as child of "player" and freezes all it's movement.*/
	public void PickUp(GameObject player) {
		this.gameObject.transform.SetParent (player.transform);
		this.gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
		beingHeld = true;
		Debug.Log ("Pickup " + this.gameObject);
	}

	/**
	 * Sets the gamobject as child of Items gameobject and allows all movement.
	 * If canThrow; adds a force to gamobject relative to input if player has horizontal input.
	 */
	public void Drop(bool canThrow) {
		this.gameObject.transform.localPosition = new Vector2 (0.5f, 0);
		this.gameObject.transform.SetParent (GameObject.Find("Items").transform, true);
		this.gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None;
		if (canThrow && (Input.GetAxis ("Horizontal") > 0.2 || Input.GetAxis ("Horizontal") < -0.2)) {
			rigidBody2D.AddForce (Vector2.right * 500 * Mathf.Sign (Input.GetAxis ("Horizontal")));
		}
		beingHeld = false;
		Debug.Log ("Drop " + this.gameObject);
	}

	/**
	 * Breaks the item a bit when called.
	 * When health is 0 or below, the item is broken.
	 * Returns 1 if it still has health, returns 0 when it has been destroyed.
	 */
	public int Break() {
		health--;
		if (health <= 0) {
			Destroy (gameObject);
			return 0;
			//Play animation and such
		}
		return 1;
	}

	/**Returns the item type.*/
	public string GetItemType() {
		return itemType;
	}
}
