using UnityEngine;
using System.Collections;

public class CharInventory : MonoBehaviour {
	//The upgrades
	public bool leaf, doubleJump, hat, shoes, laserCane;
	int healthCollectables = 0;
	//The item that can be picked up by the player
	public GameObject pickUpableItem;

	void Update() {
		if (healthCollectables == 4) {
			//Put a method here that permanently increases the players health
			//Also display some nice graphics
			healthCollectables = 0;
		}
	}

	/**
	 * Checks whether the playing is holding an item or not.
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