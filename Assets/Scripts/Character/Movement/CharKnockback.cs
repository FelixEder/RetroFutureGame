using UnityEngine;
using System.Collections;

public class CharKnockback : MonoBehaviour {
	float wayofKnock;
	CharInventory charInventory;

	void Start() {
		charInventory = GetComponent<CharInventory> ();
	}
		
	public void Knockback(GameObject attacker, float force) {
		if (!GetComponent<CharStatus> ().Invulnerable ()) {
			if (transform.position.x < attacker.transform.position.x)
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (-force, 2);
			else
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (force, 2);
			//Drops the item the player is holding.
			if (charInventory.isHoldingItem ()) {
				charInventory.getHoldingItem ().GetComponent<PickUpableItem> ().Drop (false);
				charInventory.setHoldingItem (null);
			}
		}
	}
}