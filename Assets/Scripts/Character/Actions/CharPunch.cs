using UnityEngine;
using System.Collections;

public class CharPunch : MonoBehaviour {
	CharEnergy charEnergy;
	CharInventory charInventory;
	CharStatus charStatus;
	InputManager input;
	public bool holdPunch, holdMega, onCooldown, branchInv, megaAquired;
	bool animationCooldown;
	string attackType;
	int damage, charge, limit = 200;

	void Start() {
		charEnergy = transform.parent.GetComponent<CharEnergy> ();
		charInventory = transform.parent.GetComponent<CharInventory> ();
		charStatus = transform.parent.GetComponent<CharStatus> ();
		input = GameObject.Find ("InputManager").GetComponent<InputManager> ();
	}

	void Update() {
		//PUNCH
		if (!input.GetKey ("attack") && holdPunch)
			holdPunch = false;
		else if (input.GetKey ("attack") && !holdPunch && !charStatus.isSmall) {
			holdPunch = true;
			if (!onCooldown)
				damage = ExecutePunch ();
		}

		//MEGA
		if (input.GetKey ("mega") && !charStatus.isSmall && !holdMega && megaAquired) {
			holdMega = true;
			StartCoroutine (ChargeMega ());
		}

		//TRIGGER
		transform.localPosition = new Vector2 (0.1f, 0);
		transform.localPosition = new Vector2 (0, 0);
	}

	//PUNCH
	int ExecutePunch() {
		onCooldown = true;
		GetComponent<AudioPlayer> ().PlayClip (0, 1, 0.7f,1.3f);
//anim		transform.parent.gameObject.GetComponent<Animator> ().SetTrigger ("Punching");
		if (charInventory.IsHoldingItem ()) {
			GameObject holdingItem = charInventory.GetHoldingItem ();
			switch (holdingItem.GetComponent<PickUpableItem>().GetItemType()) {

			case "Rock":
				//Play correct animation
				attackType = "Rock";
				StartCoroutine (ExecuteTrigger (1f, 0.5f));
				return holdingItem.GetComponent<PickUpableItem>().damage;

			case "Branch":
				//Play correct animation
				attackType = "Branch";
				StartCoroutine (ExecuteTrigger (1.5f, 0.75f));
				return holdingItem.GetComponent<PickUpableItem>().damage;

			default:
				attackType = "ItemError";
				return 1;
			}
		}
		else {
			//Play the standard animation
			attackType = "Punch";
			StartCoroutine (ExecuteTrigger (1f, 0.5f));
			return 1;
		}
	}
		
	//MEGA
	IEnumerator ChargeMega() {
		while (input.GetKey ("mega")) {
			while (charge < limit && input.GetKey("mega")) {
				charge++;
				if(charge == 50 || charge == limit)
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
				attackType = "FullMega";
				StartCoroutine (ExecuteTrigger (2f, 1f));
				return 5;
			} else {
				Debug.Log ("Not enough energy for Full MegaPunch");
				//No energy, play correct things
			}
		}
		else if (charge >= 50) {
			if (charEnergy.UseEnergy (1)) {
				Debug.Log ("Regular MegaPunch");
				attackType = "Mega";
				StartCoroutine (ExecuteTrigger (2f, 1f));
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

	//TRIGGER
	IEnumerator ExecuteTrigger(float sizeX, float posX) {
		GetComponent<BoxCollider2D> ().enabled = true;
		GetComponent<BoxCollider2D> ().size = new Vector2 (sizeX, 2f /*1.849279f*/);
		GetComponent<BoxCollider2D> ().offset = new Vector2 (posX, -0.2f /*-0.2104849f*/);

		//visualization
		transform.GetChild(0).GetComponent<Transform>().localScale = new Vector3 (sizeX, 1.849279f, 1f);
		transform.GetChild(0).GetComponent<Transform>().localPosition = new Vector3 (posX + 0.2f, -0.2104849f, -1f);
		//Animation
		if (!animationCooldown) {
			transform.GetChild (0).GetComponent<Animator> ().SetTrigger (attackType);
			animationCooldown = true;
		}

		yield return new WaitForSeconds (0.1f);
		GetComponent<BoxCollider2D> ().enabled = false;
		yield return new WaitForSeconds (0.1f);
		animationCooldown = false;
		onCooldown = false;
		branchInv = false;
	}

	void OnTriggerEnter2D(Collider2D victim) {
		if(attackType == "Branch" && !branchInv && victim.gameObject.tag != "Door") {
			if (charInventory.GetHoldingItem ().GetComponent<PickUpableItem> ().Break () <= 0) {
				charInventory.SetHoldingItem (null);
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
				victim.gameObject.GetComponent<Barriers> ().TakeDamage (damage);
			}
			break;

		case "SmallCritter" :
			victim.gameObject.GetComponent<SmallCritter>().Knockback(gameObject, 4);
			victim.gameObject.GetComponent<SmallCritter>().TakeDamage(damage);
			break;

		case "JumpingCritter":
			victim.gameObject.GetComponent<JumpingCritter>().TakeDamage(damage);
			break;

		case "CrawlerCritter":
			CrawlerCritter crawlerCritter = victim.gameObject.GetComponent<CrawlerCritter> ();
			if (crawlerCritter.deShelled)
				crawlerCritter.TakeDamage (damage);
			break;

		case "ShellMan":
			ShellMan shellMan = victim.gameObject.GetComponent<ShellMan> ();
			if (shellMan.deShelled)
				shellMan.TakeDamage (damage);
			break;

		case "HardEnemy":
			if (attackType == "Rock") {
				victim.gameObject.transform.parent.GetComponent<HardCritter> ().TakeDamage (1);
			}
			if (attackType == "Mega" || attackType == "FullMega") {
				victim.gameObject.GetComponent<HardCritter>().TakeDamage(damage);
			}
			break;

		case "BigEyeBuyWeakSpot":
			if (attackType == "Branch") {
				victim.gameObject.transform.parent.GetComponent<BigEyeGuy> ().TakeDamage (1);
			}
			break;

		case "StatueBossEye":
			if (attackType == "Branch") {
				victim.gameObject.GetComponent<StatueBossLaser> ().TakeDamage (1);
				charInventory.GetHoldingItem ().GetComponent<PickUpableItem> ().Break (3);
				charInventory.SetHoldingItem (null);
			}
			break;

		case "FinalBossWeakSpot":
			if (attackType == "FullMega") {
				Debug.Log ("Full MegaPunched the boss!"); 
				victim.gameObject.GetComponent<Phase1> ().TakeDamage (3);
			} else if (attackType == "Mega") {
				Debug.Log ("Regular MegaPunched the boss!"); 
				victim.gameObject.GetComponent<Phase1> ().TakeDamage (1);
			} else {
				Debug.Log ("Invulnerable to AttackType [ " + attackType + " ]");
			}
			break;

		case "FinalBossArmor":
			if (victim.gameObject.GetComponent<Phase2> ().blued) {
				if (attackType == "FullMega") {
					Debug.Log ("Full MegaPunched the boss phase 2!"); 
					victim.gameObject.GetComponent<Phase2> ().TakeDamage (3);
				} else if (attackType == "Mega"){
					Debug.Log ("Regular MegaPunched the boss phase 2!"); 
					victim.gameObject.GetComponent<Phase2> ().TakeDamage (1);
				} else {
					Debug.Log ("Invulnerable to AttackType [ " + attackType + " ]");
				}
			}
			break;
		}
		Debug.Log ("Punched [ " + victim + " ] - tag [ " + victim.gameObject.tag + " ] - attackType [ " + attackType + " ] - damage [ " + damage + " ]");
	}
}
