using UnityEngine;
using System.Collections;

public class CharInventory : MonoBehaviour {
	//The upgrades-scripts, add more when more upgrades are included
	CharFloat charFloat;
	int healthCollectables = 0;
	//The item that can be picked up by the player
	public GameObject pickUpableItem;

	void Start() {
		//As we implement more upgrades in the game, more scripts will be added in the fields and here and disabled.
		charFloat = GetComponent<CharFloat> ();
		charFloat.enabled = false;
	}
		
	void Update() {
		if (healthCollectables == 4) {
			//Put a method here that permanently increases the players health
			//Also display some nice graphics
			healthCollectables = 0;
		}
	}

	/**
	 * Checks whether the player is holding an item or not.
	 */
	public bool isHoldingItem() {
		return (pickUpableItem != null);
	}

	/**
	 * Sets a gameobject to the pickUpableItem-field
	 */
	public void setHoldingItem(GameObject itemToHold) {
		pickUpableItem = itemToHold;
	}

	/**
	 * Returns the object held by the player or null if it's not holding anything.
	 */
	public GameObject getHoldingItem() {
		return pickUpableItem;
	}

}