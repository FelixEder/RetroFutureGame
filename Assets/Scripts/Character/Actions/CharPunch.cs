using UnityEngine;
using System.Collections;

public class CharPunch : MonoBehaviour {
	CharInventory CharInventory;
	CharStatus charStatus;
	public bool holdPunch;
	string attackType;

	void Start() {
		CharInventory = transform.parent.GetComponent<CharInventory> ();
		charStatus = transform.parent.GetComponent<CharStatus> ();
	}

	void Update() {
		attackType = "";
		if (!Input.GetButton ("Attack") && holdPunch) {
			holdPunch = false;
		} else if (Input.GetButton ("Attack") && !holdPunch && !charStatus.isSmall) {
			Debug.Log ("Punched nothing");
			ExecutePunch ();
		}
	}

	/**
	 * This method determines what kind of attack the player should do.
	 * It then plays the correct animation and sets the right damage amount.
	 */
	int ExecutePunch() {
		holdPunch = true;
		if (CharInventory.isHoldingItem ()) {
			GameObject holdingItem = CharInventory.getHoldingItem ();

			switch (holdingItem.GetComponent<PickUpableItem>().GetItemType()) {

			case "Rock":
				//Play correct animation
				attackType = "Rock";
				return holdingItem.GetComponent<PickUpableItem>().damage;

			case "Branch":
				//Play correct animation
				attackType = "Branch";
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
		
	//Triggered when player punches an object.
	void OnTriggerStay2D(Collider2D victim) {
		if(Input.GetButton ("Attack") && !holdPunch && !charStatus.isSmall) {
			int damage = ExecutePunch ();
			if(attackType.Equals("branch")) {
				int lifeCheck =	CharInventory.getHoldingItem().GetComponent<PickUpableItem>().Break();
				if (lifeCheck == 0) {
					CharInventory.setHoldingItem (null);
				}
			}
			switch (victim.gameObject.tag) {

			case "Door":
				victim.gameObject.GetComponent<Door> ().SetInvisible ();
				break;
			
			case "Barrier":
				Debug.Log ("BarrierType: " + victim.gameObject.GetComponent<Barriers> ().GetBarrierType ());
				if (attackType.Equals (victim.gameObject.GetComponent<Barriers> ().GetBarrierType ())) {
					victim.gameObject.GetComponent<Barriers> ().TakeDamage ();
				}
				break;

			case "SmallCritter" :
				victim.gameObject.GetComponent<SmallCritter>().TakeDamage(damage);
				break;

			case "JumpingCritter":
				victim.gameObject.GetComponent<JumpingCritter>().TakeDamage(damage);
				break;

			case "CrawlerCritter":
				CrawlerCritter crawlerCritter = victim.gameObject.GetComponent<CrawlerCritter> ();
				if (crawlerCritter.deShelled)
					crawlerCritter.GetHurt (damage);
				break;

			case "ShellMan":
				ShellMan shellMan = victim.gameObject.GetComponent<ShellMan> ();
				if (shellMan.deShelled)
					shellMan.GetHurt (damage);
				break;

			case "HardEnemy":
				if (attackType.Equals ("Rock")) {
					victim.gameObject.transform.parent.GetComponent<BigEyeGuy> ().GetHurt (1);
				}
				break;

			case "BigEyeBuyWeakSpot":
				if (attackType.Equals ("Branch")) {
					victim.gameObject.transform.parent.GetComponent<BigEyeGuy> ().GetHurt (1);
				}
				break;
			}
			Debug.Log ("Punched a " + victim + " with tag: " + victim.gameObject.tag + "\nAttackType: " + attackType);
		}
	}
}
