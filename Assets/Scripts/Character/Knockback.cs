using UnityEngine;
using System.Collections;

public class Knockback : MonoBehaviour {
	float wayofKnock;
	CharInventory charInventory;

	void Start() {
		charInventory = GetComponent<CharInventory> ();
	}
		
	public void Knock(GameObject attacker, float force) {
		if (this.gameObject.transform.position.x <= attacker.transform.position.x)
			wayofKnock = -1;
		else {
			wayofKnock = 1;
		}
		this.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (force * wayofKnock, 2), ForceMode2D.Impulse);
		//Drops the item the player is holding.
		if (charInventory.isHoldingItem ()) {
			charInventory.getHoldingItem ().GetComponent<PickUpableItem> ().Dropped ();
			charInventory.setHoldingItem (null);
		}
	}
}

