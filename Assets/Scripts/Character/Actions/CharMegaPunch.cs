using UnityEngine;
using System.Collections;

public class CharMegaPunch : MonoBehaviour {
	CharEnergy charEnergy;
	CharStatus charStatus;
	int chargeCounter, damage;
	int chargeLimit = 200;

	void Start() {
		chargeCounter = 0;
		charEnergy = transform.parent.GetComponent<CharEnergy> ();
		charStatus = transform.parent.GetComponent<CharStatus> ();
	}

	void Update() {
		if (Input.GetButton ("MegaAttack") && !charStatus.isSmall) {
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
		} else if (Input.GetButtonUp ("MegaAttack")) {
			Debug.Log ("Doing Mega-punch!");
			ExecuteMegaPunch ();
			Invoke ("SetNoLongerMegaPunch", 1);
		}
	}

	void SetNoLongerMegaPunch() {
		charStatus.NoLongerMegaPunching ();
	}

	void ExecuteMegaPunch() {
		if (chargeCounter >= chargeLimit) {
			if (charEnergy.UseEnergy (3)) {
				//Play correct animation for charged Mega Punch
				Debug.Log("Did charged MegaPunch!");
				charStatus.chargedMegaPunch = true;
				damage = 5;
				chargeCounter = 0;
			} else {
				Debug.Log ("Not enough energy for MegaPunch");
				//No energy, play correct things
				chargeCounter = 0;
			}
		} else if (charEnergy.UseEnergy (1)) {
			Debug.Log ("Did regular MegaPunch!");
			charStatus.megaPunch = true;
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
		if(charStatus.IsMegaPunching()) {
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
				if (victim.gameObject.GetComponent<Barriers> ().GetBarrierType ().Equals ("MegaPunch")) {
					victim.gameObject.GetComponent<Barriers> ().TakeDamage ();
				}
				break;
			}
			Debug.Log (victim);
		}
	}
}