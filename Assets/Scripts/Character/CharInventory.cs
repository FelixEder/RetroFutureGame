using UnityEngine;
using System.Collections;

public class CharInventory : MonoBehaviour {
	//The upgrades-scripts, add more when more upgrades are included
//	CharFloat CharFloat;
	//The item that can be picked up by the player
	public GameObject pickupableItem;

	void Start() {
		//As we implement more upgrades in the game, more scripts will be added in the fields and here and disabled.
	//	CharFloat = GetComponent<CharFloat> ();
	//	CharFloat.enabled = false;
	}

	/**
	 * Checks whether the player is holding an item or not.
	 */
	public bool isHoldingItem() {
		return (pickupableItem != null);
	}

	/**
	 * Sets a gameobject to the pickUpableItem-field
	 */
	public void setHoldingItem(GameObject itemToHold) {
		pickupableItem = itemToHold;
	}

	/**
	 * Returns the object held by the player or null if it's not holding anything.
	 */
	public GameObject getHoldingItem() {
		return pickupableItem;
	}

}