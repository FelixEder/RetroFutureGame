using UnityEngine;
using System.Collections;

public class CharPunch : MonoBehaviour {
	CharInventory charInventory;
	bool holdPunch;
	string attackType = "";

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
				attackType = "rock";
				return holdingItem.GetComponent<PickUpableItem>().damage;

			case "branch":
				//Play correct animation
				attackType = "branch";
				return holdingItem.GetComponent<PickUpableItem>().damage;

			default:
				return 1;
			}
		}
		else {
			//Play the standard animation
			Debug.Log("Normal punch");
			return 1;
		}
	}

	void OnTriggerStay2D(Collider2D victim) {
		if(Input.GetButton ("Attack") && !holdPunch) {
			holdPunch = true;
			int damage = ExecutePunch ();
			Debug.Log ("Am punching!");
			switch (victim.gameObject.tag) {

			case "door":
				victim.gameObject.GetComponent<Door> ().setInvisible ();
				break;

			case "softEnemy":
				victim.gameObject.GetComponent<SmallCritter>().getHurt(damage);
				break;

			case "barrier":
				if (attackType.Equals(victim.gameObject.GetComponent<Barriers>().getSpecial ())) {
					victim.gameObject.GetComponent<Barriers> ().getHurt ();
				}
				break;
			}
			Debug.Log (victim);
		}
	}
}
