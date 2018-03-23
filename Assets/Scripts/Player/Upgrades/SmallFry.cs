using UnityEngine;
using System.Collections;

public class SmallFry : MonoBehaviour {
	PlayerStatus status;
	PlayerInventory inventory;
	InputManager input;
	bool holdSmall;

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
		transform.parent.GetComponent<Collider2D>().enabled = false;
		GetComponent<Collider2D>().enabled = true;

		GetComponent<SpriteRenderer>().enabled = true;
		transform.parent.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;

		status.isSmall = true;
		if(inventory.IsHoldingItem())
			inventory.SetHoldingItem(null);
	}

	void GrowBig() {
		transform.parent.GetComponent<Collider2D>().enabled = true;
		GetComponent<Collider2D>().enabled = false;

		GetComponent<SpriteRenderer>().enabled = false;
		transform.parent.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

		status.isSmall = false;
	}
}