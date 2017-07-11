using UnityEngine;
using System.Collections;

public class SmallFry : MonoBehaviour {
	PlayerStatus status;
	PlayerInventory inventory;
	InputManager input;
	bool holdSmall;
	//	public Sprite normal, smallFry;

	void Start() {
		status = transform.parent.GetComponent<PlayerStatus>();
		inventory = transform.parent.GetComponent<PlayerInventory>();
		input = GameObject.Find("InputManager").GetComponent<InputManager>();
	}

	void Update() {
		if(!input.GetKey("small") && holdSmall)
			holdSmall = false;
		if(input.GetKey("small") && !holdSmall) {
			holdSmall = true;
			if(status.isSmall) {
				GrowBig();
			}
			else {
				GrowSmall();
			}
		}
	}

	void GrowSmall() {
		transform.parent.GetComponent<PolygonCollider2D>().enabled = false;
		transform.parent.GetComponent<CircleCollider2D>().enabled = true;
		status.isSmall = true;
		if(inventory.IsHoldingItem())
			inventory.SetHoldingItem(null);
	}

	void GrowBig() {
		transform.parent.GetComponent<PolygonCollider2D>().enabled = true;
		transform.parent.GetComponent<CircleCollider2D>().enabled = false;
		status.isSmall = false;
	}
}