using UnityEngine;
using System.Collections;

public class PlayerInventory : MonoBehaviour {
	private ArrayList acquiredUpgrades;
	//The item that can be picked up by the player
	public bool enableAllUpgrades;
	public GameObject pickupableItem;
	public Transform holdPos;
	int collectedItems;

	void Start() {
		//As we implement more upgrades in the game, more scripts will be added in the fields and here and disabled.
		acquiredUpgrades = new ArrayList();
		if(enableAllUpgrades) {
			acquiredUpgrades.Add("secondJump");
			acquiredUpgrades.Add("float");
			acquiredUpgrades.Add("laser");
			acquiredUpgrades.Add("small");
			acquiredUpgrades.Add("stomp");
			acquiredUpgrades.Add("mega");
			acquiredUpgrades.Add("wallJump");
		}
	}
	
	void Update() {
		if(pickupableItem != null) {
			pickupableItem.transform.position = holdPos.position;
			pickupableItem.transform.rotation = holdPos.rotation;
		}
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

	public void CollectItem() {
		collectedItems++;
	}

    public void AddUpgrade(string upgrade) {
        acquiredUpgrades.Add(upgrade);
    }

    public ArrayList GetUpgradeList() {
        return acquiredUpgrades;
    }
	
	public bool HasAcquired(object upgrade) {
		return acquiredUpgrades.Contains(upgrade);
	}
}