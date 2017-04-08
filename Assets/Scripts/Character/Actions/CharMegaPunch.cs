using UnityEngine;
using System.Collections;

public class CharMegaPunch : MonoBehaviour {
	CharEnergy charEnergy;
	CharStatus charStatus;
	InputManager input;
	int charge, damage, limit = 200;
	bool holdMega;

	void Start() {
		charEnergy = transform.parent.GetComponent<CharEnergy> ();
		charStatus = transform.parent.GetComponent<CharStatus> ();
		input = GameObject.Find ("InputManager").GetComponent<InputManager> ();
	}

	void Update() {
		if (input.GetKey ("mega") && !charStatus.isSmall && !holdMega) {
			holdMega = true;
			StartCoroutine (ChargeMega ());
		}
	}

	IEnumerator ChargeMega() {
		while (input.GetKey ("mega")) {
			while (charge < limit && input.GetKey("mega")) {
				charge++;
				Debug.Log ("MegaPunch charge [ " + charge + " ]");
				yield return 0;
			}
			yield return 0;
		}
		damage = ExecuteMega ();
		charge = 0;
		holdMega = false;
	}
		
	int ExecuteMega() {
		if (charge == limit) {
			if (charEnergy.UseEnergy (3)) {
				Debug.Log ("Full MegaPunch");
				return 5;
			} else {
				Debug.Log ("Not enough energy for Full MegaPunch");
				//No energy, play correct things
			}
		}
		else if (charge >= 50) {
			if (charEnergy.UseEnergy (1)) {
				Debug.Log ("Regular MegaPunch");
				return 3;
			} else {
				Debug.Log ("Not enough energy for Regular MegaPunch");
				//No energy, play correct things
			}
		}
		else {
			Debug.Log ("MegaPunch canceled");
			//Too low chargecounter, play correct things
		}
		return 1;
	}

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
				victim.gameObject.GetComponent<BigEyeGuy>().TakeDamage(damage);
				break;

				case "CrawlerCritter":
				CrawlerCritter crawlerCritter = victim.gameObject.GetComponent<CrawlerCritter>();
				if(crawlerCritter.deShelled)
					crawlerCritter.TakeDamage(damage);
				break;

				case "ShellMan":
				ShellMan shellMan = victim.gameObject.GetComponent<ShellMan>();
				if(shellMan.deShelled)
					shellMan.TakeDamage(damage);
				break;

				case "HardEnemy":
				victim.gameObject.GetComponent<HardCritter>().TakeDamage(damage);
				break;

				case "Barrier":
				if (victim.gameObject.GetComponent<Barriers> ().GetBarrierType () == "MegaPunch")
					victim.gameObject.GetComponent<Barriers> ().TakeDamage (2);
				break;

				case "FinalBossWeakSpot":
				if (charStatus.chargedMegaPunch) {
					Debug.Log ("Charge Mega-Punched the boss!"); 
					victim.gameObject.GetComponent<Phase1> ().TakeDamage (3);
				} else {
					Debug.Log ("Mega-Punched the boss!"); 
					victim.gameObject.GetComponent<Phase1> ().TakeDamage (1);
				}
				break;

			case "FinalBossArmor":
				if (victim.gameObject.GetComponent<Phase2> ().blued) {
					if (charStatus.chargedMegaPunch) {
						Debug.Log ("Charge Mega-Punched the boss phase 2!"); 
						victim.gameObject.GetComponent<Phase2> ().TakeDamage (3);
					} else {
						Debug.Log ("Mega-Punched the boss phase 2!"); 
						victim.gameObject.GetComponent<Phase2> ().TakeDamage (1);
					}
				}
				break;
			}
			Debug.Log (victim);
		}
	}
}