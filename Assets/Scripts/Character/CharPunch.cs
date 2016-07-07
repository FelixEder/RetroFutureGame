using UnityEngine;
using System.Collections;

public class CharPunch : MonoBehaviour {
	CharInventory charInventory;
	bool isPunching, holdPunch;
	int damage;

	void Start() {
		charInventory = GetComponent<CharInventory> ();
	}

	void Update() {
		if (!Input.GetKey (KeyCode.K) && holdPunch) {
			holdPunch = false;
		}
	}

	/**
	 * This method determines what kind of attack the player should do.
	 * It then plays the correct animation and sets the right damage amount.
	 */
	void executePunch() {
		if (charInventory.isHoldingItem ()) {
			GameObject holdingItem = charInventory.getHoldingItem ();

			switch (holdingItem.tag) {

			case "rock":
				//Play correct animation
				damage = holdingItem.damage;
				break;

			case "branch":
				//Play correct animation
				damage = holdingItem.damage;
				break;
			}
		}
		else {
			//Play the standard animation
			damage = 1;
		}
	}

	void OnTriggerStay2D(Collider2D victim) {
		if(Input.GetKey(KeyCode.K) && !holdPunch) {
			if(!isPunching) {
				isPunching = true;
				holdPunch = true;
				executePunch ();
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
				isPunching = false;
			}
		}
	}
}
