using UnityEngine;
using System.Collections;

public class CharInventory : MonoBehaviour {
	//The upgrades
	public bool leaf, doubleJump, hat, shoes, laserCane;
	int healthCollectables = 0;
	//The item that can be picked up by the player
	GameObject pickUpableItem;

	void Update() {
		if (healthCollectables == 4) {
			//Put a method here that permanently increases the players health;
			healthCollectables = 0;
		}
	}

	/**
	 * Checks whether the playing is holding an item or not.
	 */
	public bool isHoldingItem() {
		return (pickUpableItem != null);
	}
}

