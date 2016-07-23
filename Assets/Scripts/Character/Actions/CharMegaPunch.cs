using UnityEngine;
using System.Collections;

public class MegaPunch : MonoBehaviour {
	CharInventory charInventory;
	CharEnergy charEnergy;
	int chargeCounter, damage;
	public int chargeLimit;
	public	bool holdPunch, megaPunch, chargedMegaPunch;

	void Start() {
		charInventory = transform.parent.GetComponent<CharInventory> ();
		charEnergy = transform.parent.GetComponent<CharEnergy> ();
	}

	void Update() {
		if (Input.GetButtonDown ("MegaAttack") && !holdPunch) {
			chargedMegaPunch = false;
			megaPunch = false;
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
			if (charEnergy.HasJuice (3)) {
				//Play correct animation for charged Mega Punch
				chargedMegaPunch = true;
				charEnergy.UseEnergy (3);
				damage = 5;
				chargeCounter = 0;
				holdPunch = false;
			} else {
				//No energy, play correct things
				chargeCounter = 0;
				holdPunch = false;
			}
		} else if (charEnergy.HasJuice (1)) {
			megaPunch = true;
			charEnergy.UseEnergy (1);
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
		if(megaPunch || chargedMegaPunch) {
			Debug.Log ("MegaPunch on Trigger!");
			switch (victim.gameObject.tag) {

				case "door":
				victim.gameObject.GetComponent<Door> ().SetInvisible ();
				break;

				case "softEnemy":
				victim.gameObject.GetComponent<SmallCritter>().GetHurt(damage);
				break;

		}
		Debug.Log (victim);
	}
}
