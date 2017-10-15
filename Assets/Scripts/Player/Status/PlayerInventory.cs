using UnityEngine;
using System.Collections;

public class PlayerInventory : MonoBehaviour {
    private ArrayList acquiredUpgrades;
	//The item that can be picked up by the player
	public GameObject pickupableItem;
	int collectedItems;

	void Start() {
        //As we implement more upgrades in the game, more scripts will be added in the fields and here and disabled.
        acquiredUpgrades = new ArrayList();
	}

	/**
	 * Checks whether the player is holding an item or not.
	 */
	public bool IsHoldingItem() {
		return (pickupableItem != null);
	}

	/**
	 * Sets a gameobject to the pickUpableItem-field
	 */
	public void SetHoldingItem(GameObject itemToHold) {
		pickupableItem = itemToHold;
	}

	/**
	 * Returns the object held by the player or null if it's not holding anything.
	 */
	public GameObject GetHoldingItem() {
		return pickupableItem;
	}

	public void collectItem() {
		collectedItems++;
	}

    public void addUpgrade(string upgrade) {
        acquiredUpgrades.Add(upgrade);
    }

    public ArrayList getUpgradeList() {
        return acquiredUpgrades;
    }
}