using UnityEngine;
using System.Collections;

public class CharPickUp : MonoBehaviour {
	CharInventory charInventory;
	bool holdPickup;

	void Start() {
		charInventory = transform.parent.GetComponent<CharInventory> ();
	}

	void Update() {
		if (!Input.GetButton ("Pickup") && holdPickup) {
			holdPickup = false;
		}
		if (Input.GetButton ("Pickup") && !holdPickup && charInventory.isHoldingItem ()) {
			holdPickup = true;
			charInventory.getHoldingItem ().GetComponent<PickUpableItem> ().Dropped ();
			charInventory.setHoldingItem (null);
			//Here the item should be dropped from the player
		}
	}


	void OnTriggerStay2D(Collider2D col) {
		if(Input.GetButton ("Pickup") && !holdPickup && !charInventory.isHoldingItem()) {
			Debug.Log (col.gameObject);
			holdPickup = true;
			switch(col.gameObject.tag) {

			case "rock" :
				charInventory.setHoldingItem(col.gameObject);
				col.gameObject.GetComponent<PickUpableItem>().PickedUp (this.gameObject);
				break;
			}
		}
	}
}
