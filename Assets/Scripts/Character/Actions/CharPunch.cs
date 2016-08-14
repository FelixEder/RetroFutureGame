using UnityEngine;
using System.Collections;

public class CharPunch : MonoBehaviour {
	CharInventory CharInventory;
	CharStatus charStatus;
	InputManager input;
	public bool holdPunch;
	string attackType;

	void Start() {
		CharInventory = transform.parent.GetComponent<CharInventory> ();
		charStatus = transform.parent.GetComponent<CharStatus> ();
		input = GameObject.Find ("InputManager").GetComponent<InputManager> ();
	}

	void Update() {
		transform.localPosition = new Vector2 (0.1f, 0);
		transform.localPosition = new Vector2 (0, 0);
		attackType = "";
		if (!input.GetKey ("attack") && holdPunch)
			holdPunch = false;
		else if (input.GetKey ("attack") && !holdPunch && !charStatus.isSmall) {
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
		transform.parent.gameObject.GetComponent<Animator> ().SetTrigger ("Punching");
		if (CharInventory.IsHoldingItem ()) {
			GameObject holdingItem = CharInventory.GetHoldingItem ();
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
			if(attackType == "Branch") {
				int lifeCheck =	CharInventory.GetHoldingItem().GetComponent<PickUpableItem>().Break();
				if (lifeCheck <= 0) {
					CharInventory.SetHoldingItem (null);
				}
			}
			switch (victim.gameObject.tag) {

			case "Door":
				victim.gameObject.GetComponent<Door> ().SetInvisible ();
				break;
			
			case "Barrier":
				Debug.Log ("BarrierType: " + victim.gameObject.GetComponent<Barriers> ().GetBarrierType ());
				if (attackType == victim.gameObject.GetComponent<Barriers> ().GetBarrierType ()) {
					victim.gameObject.GetComponent<Barriers> ().TakeDamage (1);
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
				if (attackType == "Rock") {
					victim.gameObject.transform.parent.GetComponent<BigEyeGuy> ().GetHurt (1);
				}
				break;

			case "BigEyeBuyWeakSpot":
				if (attackType == "Branch") {
					victim.gameObject.transform.parent.GetComponent<BigEyeGuy> ().GetHurt (1);
				}
				break;
			}
			Debug.Log ("Punched a " + victim + " with tag: " + victim.gameObject.tag + "\nAttackType: " + attackType);
		}
	}
}
