using UnityEngine;
using System.Collections;

public class Knockback : MonoBehaviour {
	float wayofKnock;
	CharInventory CharInventory;

	void Start() {
		CharInventory = GetComponent<CharInventory> ();
	}
		
	public void Knock(GameObject attacker, float force) {
		if (this.gameObject.transform.position.x <= attacker.transform.position.x)
			wayofKnock = -1;
		else {
			wayofKnock = 1;
		}
		this.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (force * wayofKnock, 2), ForceMode2D.Impulse);
		//Drops the item the player is holding.
		if (CharInventory.isHoldingItem ()) {
			CharInventory.getHoldingItem ().GetComponent<PickUpableItem> ().Drop (false);
			CharInventory.setHoldingItem (null);
		}
	}
}