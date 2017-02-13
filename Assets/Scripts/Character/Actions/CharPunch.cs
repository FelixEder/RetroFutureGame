using UnityEngine;
using System.Collections;

public class CharPunch : MonoBehaviour {
	CharInventory CharInventory;
	CharStatus charStatus;
	InputManager input;
	public bool holdPunch, onCooldown, branchInv;
	string attackType;
	int damage;

	void Start() {
		CharInventory = transform.parent.GetComponent<CharInventory> ();
		charStatus = transform.parent.GetComponent<CharStatus> ();
		input = GameObject.Find ("InputManager").GetComponent<InputManager> ();
	}

	void Update() {
		transform.localPosition = new Vector2 (0.1f, 0);
		transform.localPosition = new Vector2 (0, 0);
		if (!input.GetKey ("attack") && holdPunch)
			holdPunch = false;
		else if (input.GetKey ("attack") && !holdPunch && !charStatus.isSmall && !onCooldown) {
			Debug.Log ("onCooldown = " + onCooldown);
			damage = ExecutePunch ();
		}
	}

	/**
	 * This method determines what kind of attack the player should do.
	 * It then plays the correct animation and sets the right damage amount.
	 */
	int ExecutePunch() {
		holdPunch = true;
		onCooldown = true;
		transform.parent.gameObject.GetComponent<Animator> ().SetTrigger ("Punching");
		if (CharInventory.IsHoldingItem ()) {
			GameObject holdingItem = CharInventory.GetHoldingItem ();
			switch (holdingItem.GetComponent<PickUpableItem>().GetItemType()) {

			case "Rock":
				//Play correct animation
				attackType = "Rock";
				StartCoroutine (TriggerPunch (1f, 0.5f));
				return holdingItem.GetComponent<PickUpableItem>().damage;

			case "Branch":
				//Play correct animation
				attackType = "Branch";
				StartCoroutine (TriggerPunch (1.5f, 0.75f));
				return holdingItem.GetComponent<PickUpableItem>().damage;

			default:
				return 1;
			}
		}
		else {
			//Play the standard animation
			StartCoroutine (TriggerPunch (1f, 0.5f));
			return 1;
		}
		attackType = "";
	}

	IEnumerator TriggerPunch(float sizeX, float posX) {
		GetComponent<BoxCollider2D> ().enabled = true;
		GetComponent<BoxCollider2D> ().size = new Vector2 (sizeX, 1.849279f);
		GetComponent<BoxCollider2D> ().offset = new Vector2 (posX, -0.2104849f);
		//visualization
		transform.GetChild(0).GetComponent<Transform>().localScale = new Vector3 (sizeX, 1.849279f, 1f);
		transform.GetChild(0).GetComponent<Transform>().localPosition = new Vector3 (posX, -0.2104849f, -1f);

		yield return new WaitForSeconds (0.1f);
		GetComponent<BoxCollider2D> ().enabled = false;
		yield return new WaitForSeconds (0.2f);
		onCooldown = false;
		branchInv = false;
	}

	//Triggered when player punches an object.
	void OnTriggerStay2D(Collider2D victim) {
		if(attackType == "Branch" && !branchInv) {
			if (CharInventory.GetHoldingItem ().GetComponent<PickUpableItem> ().Break () <= 0) {
				CharInventory.SetHoldingItem (null);
				attackType = "";
			}
			branchInv = true;
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
		Debug.Log ("Punched [ " + victim + " ] with tag [ " + victim.gameObject.tag + " ]\nAttackType [ " + attackType + " ]");
	}
}
