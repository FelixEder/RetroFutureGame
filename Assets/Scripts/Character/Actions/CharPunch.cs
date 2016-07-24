using UnityEngine;
using System.Collections;

public class CharPunch : MonoBehaviour {
	CharInventory charInventory;
	public bool holdPunch;
	string attackType;

	void Start() {
		charInventory = transform.parent.GetComponent<CharInventory> ();
	}

	void Update() {
		attackType = "";
		if (!Input.GetButton ("Attack") && holdPunch) {
			holdPunch = false;
		} else if (Input.GetButton ("Attack") && !holdPunch) {
			Debug.Log ("Trigger free attack!");
			ExecutePunch ();
		}
	}

	/**
	 * This method determines what kind of attack the player should do.
	 * It then plays the correct animation and sets the right damage amount.
	 */
	int ExecutePunch() {
		holdPunch = true;
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
			return 1;
		}
	}

	/**
	 * Triggered when player punches an object.
	 */
	void OnTriggerStay2D(Collider2D victim) {
		if(Input.GetButton ("Attack") && !holdPunch) {
			Debug.Log ("Attack on Trigger!");
			int damage = ExecutePunch ();
			if(attackType.Equals("branch")) {
				int lifeCheck =	charInventory.getHoldingItem().GetComponent<PickUpableItem>().GetBroken();
				if (lifeCheck == 0) {
					charInventory.setHoldingItem (null);
				}
			}
			switch (victim.gameObject.tag) {

			case "door":
				victim.gameObject.GetComponent<Door> ().SetInvisible ();
				break;

			case "softEnemy":
				victim.gameObject.GetComponent<SmallCritter>().GetHurt(damage);
				break;

			case "stompEnemy":
				GetComponent<Knockback> ().knock (victim.gameObject, 2f);
				break;

			case "hardEnemy":
				if (attackType.Equals ("rock")) {
					victim.gameObject.transform.parent.GetComponent<BigEyeGuy> ().GetHurt (1);
				}
				break;

			case "bigEyeBuyWeakSpot":
				if (attackType.Equals ("branch")) {
					victim.gameObject.transform.parent.GetComponent<BigEyeGuy> ().GetHurt (1);
				}
				break;

			case "barrier":
				if (attackType.Equals(victim.gameObject.GetComponent<Barriers>().GetSpecial ())) {
					victim.gameObject.GetComponent<Barriers> ().GetHurt ();
				}
				break;
			}
			Debug.Log (victim);
		}
	}
}
