using UnityEngine;
using System.Collections;

public class PlayerPickUp : MonoBehaviour {
	PlayerStatus status;
	PlayerInventory inventory;
	InputManager input;
	bool holdPickup;

	public LayerMask whatIsItem;

	void OnDrawGizmosSelected() {
		Gizmos.color = new Color(0, 0, 1, 0.5f);
		Gizmos.DrawCube(transform.position, new Vector2(1.2f, 2f));
	}

	void Start() {
		inventory = transform.parent.GetComponent<PlayerInventory>();
		status = transform.parent.GetComponent<PlayerStatus>();
		input = GameObject.Find("InputManager").GetComponent<InputManager>();
	}

	void Update() {
		if(!input.GetKey("grab") && holdPickup) {
			holdPickup = false;
		}
		if(input.GetKey("grab") && !holdPickup && inventory.IsHoldingItem() && !status.isSmall) { //calls on drop method.
			holdPickup = true;
			inventory.GetHoldingItem().GetComponent<PickUpableItem>().Drop(true);
			inventory.SetHoldingItem(null);
		}
		else if(input.GetKey("grab") && !holdPickup && !inventory.IsHoldingItem() && !status.isSmall) {
			holdPickup = true;
			PickupArea();
		}
	}

	void PickupArea() {
		Collider2D item = Physics2D.OverlapBox(transform.position, new Vector2(1.2f, 2f), 0, whatIsItem);
		if(item != null) {
			Debug.Log("Tried to pick up " + item);
			switch(item.gameObject.GetComponent<PickUpableItem>().GetItemType()) {
				case "Rock":
				case "Branch":
					inventory.SetHoldingItem(item.gameObject);
					item.gameObject.GetComponent<PickUpableItem>().PickUp(this.gameObject);
					break;
			}
		}
	}
}
