using UnityEngine;
using System.Collections;

public class SmallFry : MonoBehaviour {
	CharStatus status;
	CharInventory inventory;
	InputManager input;
	bool holdSmall;
	//	public Sprite normal, smallFry;

	void Start() {
		status = transform.parent.GetComponent<CharStatus>();
		inventory = transform.parent.GetComponent<CharInventory>();
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