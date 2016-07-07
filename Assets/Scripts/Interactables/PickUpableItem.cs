using UnityEngine;
using System.Collections;

public class PickUpableItem : MonoBehaviour {
	Rigidbody2D rigidBody2D;
	public float HoldPositionX = 0.341f, HoldPositionY = -0.332f;
	bool beingHeld;

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

	public void PickedUp(GameObject player) {
		this.gameObject.transform.SetParent (player.transform);
		this.gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
		this.gameObject.GetComponent<Collider2D> ().enabled = false;
		beingHeld = true;
	}

	public void Dropped() {
		this.gameObject.transform.SetParent (GameObject.Find("Items").transform, true);
		this.gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None;
		this.gameObject.GetComponent<Collider2D> ().enabled = true;
		beingHeld = false;
	}
}
