using UnityEngine;
using System.Collections;

public class CharPickUp : MonoBehaviour {
	CharInventory charInventory;

	void Start() {
		charInventory = transform.parent.GetComponent<CharInventory> ();
	}

	void FixedUpdate() {
		if (Input.GetKey (KeyCode.L) && charInventory.isHoldingItem ()) {
			charInventory.setHoldingItem (null);
			//Here the item should be dropped from the player
		}
	}


	void OnTriggerStay2D(Collider2D col) {
		switch(col.gameObject.tag) {

		case "rock" :
			if(Input.GetKey(KeyCode.L) && !charInventory.isHoldingItem()) {
				charInventory.setHoldingItem(col.gameObject);
				col.gameObject.GetComponent<PickUpableItem>().PickedUp (this.gameObject);
			}
			break;
		}
	}
}
