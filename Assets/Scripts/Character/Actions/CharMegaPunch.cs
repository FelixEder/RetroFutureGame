using UnityEngine;
using System.Collections;

public class CharMegaPunch : MonoBehaviour {
	CharEnergy charEnergy;
	CharStatus status;
	InputManager input;
	int chargeCounter, damage, chargeLimit = 200;
	bool charging;

	void Start() {
		chargeCounter = 0;
		charEnergy = transform.parent.GetComponent<CharEnergy> ();
		status = transform.parent.GetComponent<CharStatus> ();
		input = GameObject.Find ("InputManager").GetComponent<InputManager> ();
	}

	void Update() {
		if (input.GetKey ("mega") && !status.isSmall) {
			charging = true;
			if (chargeCounter < chargeLimit) {
				chargeCounter++;
				Debug.Log (chargeCounter);
				if (chargeCounter >= 50) {
					Debug.Log ("Charging weapon");
					//Play charging weapon soundFX and animation
				}
			} else {
				Debug.Log ("Finished charging");
				//SoundFX and animation should reflect that punch has been fully charged
			}
		} else if (!input.GetKey("mega") && charging) {
			Debug.Log ("Doing Mega-punch!");
			charging = false;
			ExecuteMegaPunch ();
			Invoke ("SetNoLongerMegaPunch", 1);
		}
	}

	void SetNoLongerMegaPunch() {
		status.NoLongerMegaPunching ();
	}

	void ExecuteMegaPunch() {
		if (chargeCounter >= chargeLimit) {
			if (charEnergy.UseEnergy (3)) {
				//Play correct animation for charged Mega Punch
				Debug.Log("Did charged MegaPunch!");
				status.chargedMegaPunch = true;
				damage = 5;
				chargeCounter = 0;
			} else {
				Debug.Log ("Not enough energy for MegaPunch");
				//No energy, play correct things
				chargeCounter = 0;
			}
		} else if (charEnergy.UseEnergy (1)) {
			Debug.Log ("Did regular MegaPunch!");
			status.megaPunch = true;
			damage = 3;
			chargeCounter = 0;
		} else {
			Debug.Log ("Not enough energy for MegaPunch");
			//No energy, play correct things
			chargeCounter = 0;
		}
	}

	/**
	 * Triggered when player punches an object.
	 */
	void OnTriggerStay2D(Collider2D victim) {
		if (!GetComponent<CharMegaPunch> ().enabled)
			return;
		if(status.IsMegaPunching()) {
			Debug.Log ("MegaPunch on Trigger!");
			switch (victim.gameObject.tag) {
				//Update this list with correct reactions to the mega-punch
				case "Door":
				victim.gameObject.GetComponent<Door> ().SetInvisible ();
				break;

				case "SmallCritter":
				victim.gameObject.GetComponent<SmallCritter>().TakeDamage(damage);
				break;

				case "JumpingCritter":
				victim.gameObject.GetComponent<JumpingCritter>().TakeDamage(damage);
				break;

				case "BigEyeGuy":
				victim.gameObject.GetComponent<BigEyeGuy>().GetHurt(damage);
				break;

				case "CrawlerCritter":
				CrawlerCritter crawlerCritter = victim.gameObject.GetComponent<CrawlerCritter>();
				if(crawlerCritter.deShelled)
					crawlerCritter.GetHurt(damage);
				break;

				case "ShellMan":
				ShellMan shellMan = victim.gameObject.GetComponent<ShellMan>();
				if(shellMan.deShelled)
					shellMan.GetHurt(damage);
				break;

				case "HardEnemy":
				victim.gameObject.GetComponent<HardCritter>().GetHurt(damage);
				break;

				case "Barrier":
				if (victim.gameObject.GetComponent<Barriers> ().GetBarrierType () == "MegaPunch")
					victim.gameObject.GetComponent<Barriers> ().TakeDamage (2);
				break;

				case "FinalBossWeakSpot":
				if (status.chargedMegaPunch) {
					Debug.Log ("Charge Mega-Punched the boss!"); 
					victim.gameObject.GetComponent<Phase1> ().GetHurt (3);
				} else {
					Debug.Log ("Mega-Punched the boss!"); 
					victim.gameObject.GetComponent<Phase1> ().GetHurt (1);
				}
				break;

			case "FinalBossArmor":
				if (victim.gameObject.GetComponent<Phase2> ().blued) {
					if (status.chargedMegaPunch) {
						Debug.Log ("Charge Mega-Punched the boss phase 2!"); 
						victim.gameObject.GetComponent<Phase2> ().GetHurt (3);
					} else {
						Debug.Log ("Mega-Punched the boss phase 2!"); 
						victim.gameObject.GetComponent<Phase2> ().GetHurt (1);
					}
				}
				break;
			}
			Debug.Log (victim);
		}
	}
}