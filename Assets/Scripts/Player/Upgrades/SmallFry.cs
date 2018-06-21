using UnityEngine;
using System.Collections;

public class SmallFry : MonoBehaviour {
	PlayerStatus status;
	PlayerInventory inventory;
	InputManager input;
	bool holdSmall, somethingAbove;
	public LayerMask whatIsRoof;
	public Transform aboveCheck;

	void Start() {
		status = GetComponent<PlayerStatus>();
		inventory = GetComponent<PlayerInventory>();
		input = GameObject.Find("InputManager").GetComponent<InputManager>();
	}

	void Update() {
		somethingAbove = Physics2D.OverlapBox(aboveCheck.position, new Vector2(0.7f, 1.2f), 0, whatIsRoof);

		if (!input.GetKey("small") && holdSmall)
			holdSmall = false;
		if(input.GetKey("small") && !holdSmall) {
			holdSmall = true;
			if(!status.isSmall) {
				GrowSmall();
			}
			else if (!somethingAbove) {
				GrowBig();
			}
			else {
				Debug.Log("Something above player");
			}
		}
	}

	void GrowSmall() {
		GetComponent<Animator>().SetBool("small", true);
		/*
		transform.parent.GetComponent<Collider2D>().enabled = false;
		GetComponent<Collider2D>().enabled = true;

		GetComponent<SpriteRenderer>().enabled = true;
		transform.parent.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
		*/
		status.isSmall = true;
		if(inventory.IsHoldingItem()) {
			inventory.GetHoldingItem().GetComponent<PickUpableItem>().Drop(false);
			inventory.SetHoldingItem(null);
		}
	}

	void GrowBig() {
		GetComponent<Animator>().SetBool("small", false);
		/*
		transform.parent.GetComponent<Collider2D>().enabled = true;
		GetComponent<Collider2D>().enabled = false;

		GetComponent<SpriteRenderer>().enabled = false;
		transform.parent.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
		*/
		status.isSmall = false;
	}
}