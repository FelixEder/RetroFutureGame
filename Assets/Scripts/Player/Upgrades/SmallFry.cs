using UnityEngine;
using System.Collections;

public class SmallFry : MonoBehaviour {
	PlayerStatus status;
	PlayerInventory inventory;
	InputManager input;
	Animator anim;
	bool holdSmall, somethingAbove;
	public LayerMask whatIsRoof;
	public Transform aboveCheck;

	void Start() {
		status = GetComponent<PlayerStatus>();
		inventory = GetComponent<PlayerInventory>();
		input = GameObject.Find("InputManager").GetComponent<InputManager>();
		anim = GetComponent<Animator>();
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
		
		if(status.grounded && status.isSmall)
			anim.speed = 1;
		else if(!status.grounded && status.isSmall)
			anim.speed = 0.7f;
	}

	void GrowSmall() {
		anim.SetBool("small", true);
		status.isSmall = true;
		if(inventory.IsHoldingItem()) {
			GetComponent<PlayerPickUp>().Drop(false);
		}
	}

	public void GrowBig() {
		anim.SetBool("small", false);
		status.isSmall = false;
	}
}