using UnityEngine;
using System.Collections;

public class MegaPunch : MonoBehaviour {
	CharInventory charInventory;
	CharEnergy charEnergy;
	CharStatus charStatus;
	int chargeCounter, damage;
	public int chargeLimit = 250;
	public	bool holdPunch, megaPunch, chargedMegaPunch;

	void Start() {
		charInventory = transform.parent.GetComponent<CharInventory> ();
		charEnergy = transform.parent.GetComponent<CharEnergy> ();
		charStatus = transform.parent.GetComponent<CharStatus> ();
	}

	void Update() {
		if (Input.GetButtonDown ("MegaAttack") && !holdPunch) {
			charStatus.NoLongerMegaPunching ();
			holdPunch = true;
			if (chargeCounter < chargeLimit) {
				chargeCounter++;
				if (chargeCounter >= 50) {
					Debug.Log ("Charging weapon");
					//Play charging weapon soundFX and animation
				}
			} else {
				//SoundFX and animation should reflect that punch has been fully charged
			}
		} else if (Input.GetButtonUp ("MegaAttack") && holdPunch) {
			ExecuteMegaPunch ();
		}
	}

	void ExecuteMegaPunch() {
		if (chargeCounter >= chargeLimit) {
			if (charEnergy.UseEnergy (3)) {
				//Play correct animation for charged Mega Punch
				charStatus.chargedMegaPunch = true;
				damage = 5;
				chargeCounter = 0;
				holdPunch = false;
			} else {
				//No energy, play correct things
				chargeCounter = 0;
				holdPunch = false;
			}
		} else if (charEnergy.UseEnergy (1)) {
			charStatus.megaPunch = true;
			damage = 3;
			chargeCounter = 0;
			holdPunch = false;
		} else {
			//No energy, play correct things
			chargeCounter = 0;
			holdPunch = false;
		}
	}

	/**
	 * Triggered when player punches an object.
	 */
	void OnTriggerStay2D(Collider2D victim) {
		if(charStatus.IsMegaPunching()) {
			Debug.Log ("MegaPunch on Trigger!");
			switch (victim.gameObject.tag) {
				//Update this list with correct reactions to the mega-punch
				case "door":
				victim.gameObject.GetComponent<Door> ().SetInvisible ();
				break;

				case "softEnemy":
				victim.gameObject.GetComponent<SmallCritter>().TakeDamage(damage);
				break;

				case "eyeEnemy":
				//Can't be hurt with mega-punch, play relevant things
				break;

				case "stompEnemy":
				break;

				case "hardEnemy":
				break;
			}
		}
		Debug.Log (victim);
	}
}