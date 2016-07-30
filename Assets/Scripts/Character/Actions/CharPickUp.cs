using UnityEngine;
using System.Collections;

public class CharPickUp : MonoBehaviour {
	CharStatus charStatus;
	CharInventory charInventory;
	bool holdPickup;

	void Start() {
		charInventory = transform.parent.GetComponent<CharInventory> ();
		charStatus = transform.parent.GetComponent<CharStatus> ();
	}

	void Update() {
		if (!Input.GetButton ("Pickup") && holdPickup) {
			Debug.Log ("Let go of Pickup button");
			holdPickup = false;
		}
		if (Input.GetButton ("Pickup") && !holdPickup && charInventory.isHoldingItem () && !charStatus.isSmall) { //calls on drop method.
			Debug.Log ("Call on drop.\nButton = " + Input.GetButton ("Pickup") + ". holdPickup = " + holdPickup + ". isholding = " + charInventory.isHoldingItem());
			holdPickup = true;
			charInventory.getHoldingItem ().GetComponent<PickUpableItem> ().Drop (true);
			charInventory.setHoldingItem (null);
		}
	}
		
	void OnTriggerStay2D(Collider2D col) {
		if (Input.GetButton ("Pickup") && !holdPickup && !charInventory.isHoldingItem () && !charStatus.isSmall) {
			holdPickup = true;
			Debug.Log ("Tried to pick up " + col.gameObject);
			Debug.Log ("holdPickup = " + holdPickup);
			switch (col.gameObject.GetComponent<PickUpableItem>().GetItemType()) {

				case "Rock":
					charInventory.setHoldingItem (col.gameObject);
					col.gameObject.GetComponent<PickUpableItem> ().PickUp (this.gameObject);
					break;

				case "Branch":
					charInventory.setHoldingItem (col.gameObject);
					col.gameObject.GetComponent<PickUpableItem> ().PickUp (this.gameObject);
					break;
			}
		}
	}
}
