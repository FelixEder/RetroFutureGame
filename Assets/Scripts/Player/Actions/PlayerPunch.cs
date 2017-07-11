using UnityEngine;
using System.Collections;

public class PlayerPunch : MonoBehaviour {
	PlayerEnergy playerEnergy;
	PlayerInventory playerInventory;
	PlayerStatus playerStatus;
	InputManager input;
	public bool holdPunch, holdMega, onCooldown, branchInv, megaAquired;
	bool animationCooldown;
	string attackType;
	int damage, charge, limit = 200;

	Collider2D[] victims;
	RaycastHit2D[] castVictims;
	public LayerMask whatIsPunchable;
	float gizmoSizeX = 1f;

	void OnDrawGizmosSelected() {
		Gizmos.color = new Color(1, 0, 0, 0.5f);
		Gizmos.DrawCube(transform.position, new Vector3(gizmoSizeX, 2f, 1));
	}

	void Start() {
		playerEnergy = transform.parent.GetComponent<PlayerEnergy>();
		playerInventory = transform.parent.GetComponent<PlayerInventory>();
		playerStatus = transform.parent.GetComponent<PlayerStatus>();
		input = GameObject.Find("InputManager").GetComponent<InputManager>();
	}

	void Update() { //TODO: COMBINE BUTTONS TO SAME KEY.
		//PUNCH
		if(!input.GetKey("attack") && holdPunch)
			holdPunch = false;
		else if(input.GetKey("attack") && !(input.GetAxis("Ybool") < 0f && playerStatus.InAir()) && !holdPunch && !playerStatus.isSmall) {
			holdPunch = true;
			if(!onCooldown)
				ExecutePunch();
		}

		//MEGA
		if(input.GetKey("mega") && !playerStatus.isSmall && !holdMega && megaAquired) {
			holdMega = true;
			StartCoroutine(ChargeMega());
		}
	}

	//PUNCH
	void ExecutePunch() { //TODO: INSERT CODE INTO MEGAEXECUTE and actually return an int, run execute in damagearea coroutine starter.
		onCooldown = true;
		if(playerInventory.IsHoldingItem()) {
			GameObject holdingItem = playerInventory.GetHoldingItem();
			switch(holdingItem.GetComponent<PickUpableItem>().GetItemType()) {

				case "Rock":
					//Play correct animation
					attackType = "Rock";
					damage = holdingItem.GetComponent<PickUpableItem>().damage;
					StartCoroutine(DamageArea(1.1f));
					return;

				case "Branch":
					//Play correct animation
					attackType = "Branch";
					damage = holdingItem.GetComponent<PickUpableItem>().damage;
					StartCoroutine(DamageArea(1.5f));
					return;

				default:
					attackType = "ItemError";
					damage = 1;
					return;
			}
		}
		else {
			//Play the standard animation
			attackType = "Punch";
			damage = 1;
			StartCoroutine(DamageArea(1.1f));
		}
	}

	//MEGA
	IEnumerator ChargeMega() { //TODO: USE FOR HOLD KEY CHECK THEN CHECK CHARGE FOR TYPE. ATTACKCHARGE
		while(input.GetKey("mega")) {
			while(charge < limit && input.GetKey("mega")) {
				charge++;
				if(charge == 50 || charge == limit)
					Debug.Log("MegaPunch charge [ " + charge + " ]");
				yield return 0;
			}
			yield return 0;
		}
		damage = ExecuteMega();
		charge = 0;
		holdMega = false;
	}

	int ExecuteMega() { //TODO: ADD PUNCH EXECUTE AT END AND IF CHARGE TO LOW DO PUNCH INSTEAD. ATTACKTYPE()
		if(charge == limit) {
			if(playerEnergy.UseEnergy(3)) {
				Debug.Log("Full MegaPunch");
				attackType = "FullMega";
				StartCoroutine(DamageArea(2f));
				return 5;
			}
			else {
				Debug.Log("Not enough energy for Full MegaPunch");
				//No energy, play correct things
			}
		}
		else if(charge >= 50) {
			if(playerEnergy.UseEnergy(1)) {
				Debug.Log("Regular MegaPunch");
				attackType = "Mega";
				StartCoroutine(DamageArea(2f));
				return 3;
			}
			else {
				Debug.Log("Not enough energy for Regular MegaPunch");
				//No energy, play correct things
			}
		}
		else {
			Debug.Log("MegaPunch canceled");
			//Too low chargecounter, play correct things //new: and check punch instead.
		}
		return 1;
	}

	//OverlapBox check and damage all victims in area.
	IEnumerator DamageArea(float sizeX) {
		transform.localPosition = new Vector2(sizeX / 2, -0.2f);
		gizmoSizeX = sizeX;
		victims = Physics2D.OverlapBoxAll(transform.position, new Vector2(sizeX, 2f), 0, whatIsPunchable);
		

		foreach(Collider2D victim in victims) {
			var enemyHealth = victim.gameObject.GetComponent<EnemyHealth>();
			if(attackType == "Branch" && !branchInv && victim.gameObject.tag != "Door") {
				if(playerInventory.GetHoldingItem().GetComponent<PickUpableItem>().Break() <= 0) {
					playerInventory.SetHoldingItem(null);
				}
				branchInv = true;
			}

			switch(victim.gameObject.tag) {

				case "Door":
					victim.gameObject.GetComponent<Door>().SetInvisible();
					attackType = "Door";
					break;

				case "Barrier":
					Debug.Log("BarrierType: " + victim.gameObject.GetComponent<Barriers>().GetBarrierType());
					if(attackType == victim.gameObject.GetComponent<Barriers>().GetBarrierType())
						victim.gameObject.GetComponent<Barriers>().TakeDamage(damage);
					attackType = "Barrier";
					break;

				case "SmallCritter":
				case "JumpingCritter":
					enemyHealth.TakeDamage(damage, gameObject, 4f);
					break;

				case "CrawlerCritter":
					if(victim.gameObject.GetComponent<CrawlerCritter>().noShell)
						enemyHealth.TakeDamage(damage, gameObject, 4f);
					break;

				case "ShellMan":
					ShellMan shellMan = victim.gameObject.GetComponent<ShellMan>();
					if(shellMan.deShelled)
						enemyHealth.TakeDamage(damage);
					break;

				case "HardEnemy":
					if(attackType == "Rock")
						enemyHealth.TakeDamage(1);
					if(attackType == "Mega" || attackType == "FullMega")
						enemyHealth.TakeDamage(damage);
					break;

				case "BigEyeBuyWeakSpot":
					if(attackType == "Branch")
						victim.gameObject.transform.parent.GetComponent<BigEyeGuy>().TakeDamage(1);
					break;

				case "StatueBossEye":
					if(attackType == "Branch") {
						if(playerInventory.IsHoldingItem()) {
							victim.gameObject.GetComponent<StatueBossLaser>().TakeDamage(1);
							playerInventory.GetHoldingItem().GetComponent<PickUpableItem>().Break(3);
							playerInventory.SetHoldingItem(null);
						}
					}
					break;

				case "FinalBossWeakSpot":
					if(attackType == "FullMega") {
						Debug.Log("Full MegaPunched the boss!");
						victim.gameObject.GetComponent<Phase1>().TakeDamage(3);
					}
					else if(attackType == "Mega") {
						Debug.Log("Regular MegaPunched the boss!");
						victim.gameObject.GetComponent<Phase1>().TakeDamage(1);
					}
					else {
						Debug.Log("Invulnerable to AttackType [ " + attackType + " ]");
					}
					break;

				case "FinalBossArmor2":
					if(victim.gameObject.GetComponent<Phase2>().blued) {
						if(attackType == "FullMega") {
							Debug.Log("Full MegaPunched the boss phase 2!");
							victim.gameObject.GetComponent<Phase2>().TakeDamage(3);
						}
						else if(attackType == "Mega") {
							Debug.Log("Regular MegaPunched the boss phase 2!");
							victim.gameObject.GetComponent<Phase2>().TakeDamage(1);
						}
						else {
							Debug.Log("Invulnerable to AttackType [ " + attackType + " ]");
						}
					}
					break;
			}
			Debug.Log("Punched [ " + victim + " ] - tag [ " + victim.gameObject.tag + " ] - attackType [ " + attackType + " ] - damage [ " + damage + " ]");
		}

		transform.GetChild(0).GetComponent<Animator>().SetTrigger(attackType);
		if(!(attackType == "Door" || attackType == "Barrier"))
			GetComponent<AudioPlayer>().PlayClip(0, 1, 0.7f, 1.3f);

		yield return new WaitForSeconds(0.2f);
		onCooldown = false;
		branchInv = false;
	}
}
