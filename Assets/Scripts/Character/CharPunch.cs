using UnityEngine;
using System.Collections;

public class CharPunch : MonoBehaviour {
	CharInventory charInventory;
	bool holdPunch;

	void Start() {
		charInventory = transform.parent.GetComponent<CharInventory> ();
	}

	void Update() {
		if (!Input.GetButton ("Attack") && holdPunch) {
			holdPunch = false;
		}
	}

	/**
	 * This method determines what kind of attack the player should do.
	 * It then plays the correct animation and sets the right damage amount.
	 */
	int ExecutePunch() {
		if (charInventory.isHoldingItem ()) {
			GameObject holdingItem = charInventory.getHoldingItem ();

			switch (holdingItem.tag) {

			case "rock":
				//Play correct animation
				return holdingItem.GetComponent<PickUpableItem>().damage;

			case "branch":
				//Play correct animation
				return holdingItem.GetComponent<PickUpableItem>().damage;

			default:
				return 1;
				
			}
		}
		else {
			//Play the standard animation
			return 1;
		}
	}

	void OnTriggerStay2D(Collider2D victim) {
		if(Input.GetButton ("Attack") && !holdPunch) {
			holdPunch = true;
			int damage = ExecutePunch ();
			switch (victim.gameObject.tag) {

			case "door":
				victim.gameObject.GetComponent<Door> ().setInvisible ();
				break;

			case "softEnemy":
				victim.gameObject.GetComponent<SmallCritter>().getHurt(damage);
				break;
			}
			Debug.Log (victim);
			//Make it so that this boolean is set to false only when the punch animation is finished
		}
	}
}
