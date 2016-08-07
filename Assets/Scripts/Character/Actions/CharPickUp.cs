using UnityEngine;
using System.Collections;

public class CharPickUp : MonoBehaviour {
	CharStatus status;
	CharInventory inventory;
	InputManager input;
	bool holdPickup;

	void Start() {
		inventory = transform.parent.GetComponent<CharInventory> ();
		status = transform.parent.GetComponent<CharStatus> ();
		input = GameObject.Find ("InputManager").GetComponent<InputManager> ();
	}

	void Update() {
		if (!input.GetKey("grab") && holdPickup) {
			holdPickup = false;
		}
		if (input.GetKey ("grab") && !holdPickup && inventory.IsHoldingItem () && !status.isSmall) { //calls on drop method.
			holdPickup = true;
			inventory.GetHoldingItem ().GetComponent<PickUpableItem> ().Drop (true);
			inventory.SetHoldingItem (null);
		}
	}
		
	void OnTriggerStay2D(Collider2D col) {
		if (input.GetKey ("grab") && !holdPickup && !inventory.IsHoldingItem () && !status.isSmall) {
			holdPickup = true;
			Debug.Log ("Tried to pick up " + col.gameObject);
			switch (col.gameObject.GetComponent<PickUpableItem>().GetItemType()) {

				case "Rock":
				case "Branch":
					inventory.SetHoldingItem (col.gameObject);
					col.gameObject.GetComponent<PickUpableItem> ().PickUp (this.gameObject);
					break;
			}
		}
	}
}
