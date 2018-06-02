using UnityEngine;
using System.Collections;

public class PlayerPunch : MonoBehaviour {
	public bool megaAquired;
	public int minLimit, maxLimit = 200;
	public float offset;
	public GameObject punchVisual;
	public LayerMask whatIsPunchable;

	bool animationCooldown, holdPunch, onCooldown, branchInv;
	string attackType;
	int damage, charge;
	float gizmoSizeX = 1f;

	PlayerEnergy energy;
	PlayerInventory inventory;
	PlayerStatus status;
	InputManager input;

	Collider2D[] victims;
	RaycastHit2D[] castVictims;

	void OnDrawGizmosSelected() {
		Gizmos.color = new Color(1, 0, 0, 0.5f);
		if(status) {
			if(status.isMirrored)
				Gizmos.DrawCube(transform.position + new Vector3(-gizmoSizeX / 2, offset), new Vector3(gizmoSizeX, 2f));
			else
				Gizmos.DrawCube(transform.position + new Vector3(gizmoSizeX / 2, offset), new Vector3(gizmoSizeX, 2f));
		}
		else
			Gizmos.DrawCube(transform.position + new Vector3(gizmoSizeX / 2, offset), new Vector3(gizmoSizeX, 2f));
	}

	void Start() {
		energy = GetComponent<PlayerEnergy>();
		inventory = GetComponent<PlayerInventory>();
		status = GetComponent<PlayerStatus>();
		input = GameObject.Find("InputManager").GetComponent<InputManager>();
	}

	void Update() {
		Debug.Log(charge + "  " + input.GetKey("attack"));
		if(!input.GetKey("attack") && holdPunch) {
			holdPunch = false;
			Debug.Log("Starting regular-punch");
		}
		else if(input.GetKey("attack") && !holdPunch && !status.isSmall) {
			Debug.Log("Entered the else-if");
			holdPunch = true;
			attackType = "Punch";
			damage = 1;
			if (!onCooldown) {
				Debug.Log("Not on cooldown");
				AttackType();
			}
			if (!onCooldown && megaAquired) {
				Debug.Log("Starting mega-punch");
				StartCoroutine(AttackCharge());
			}
		}
	}

	//MEGA
	IEnumerator AttackCharge() {
		while(input.GetKey("attack")) {
			while(charge < maxLimit && input.GetKey("attack")) {
				charge ++;
				if(charge == minLimit || charge == maxLimit)
					Debug.Log("MegaPunch charge [ " + charge + " ]");
				yield return 0;
			}
			yield return 0;
		}
		AttackType();
		charge = 0;
	}

	void AttackType() {
		onCooldown = true;
		if(charge == maxLimit && megaAquired) {
			if(energy.UseEnergy(3)) {
				Debug.Log("Full MegaPunch");
				attackType = "FullMega";
				damage = 5;
				StartCoroutine(DamageArea(2.5f));
				return;
			}
			else {
				Debug.Log("Not enough energy for Full MegaPunch");
				//No energy, play correct things
			}
		}
		else if(charge >= minLimit && megaAquired) {
			if(energy.UseEnergy(1)) {
				Debug.Log("Regular MegaPunch");
				attackType = "Mega";
				damage = 3;
				StartCoroutine(DamageArea(2.2f));
				return;
			}
			else {
				Debug.Log("Not enough energy for Regular MegaPunch");
				//No energy, play correct things
			}
		}
		else if(inventory.IsHoldingItem()) {
			GameObject holdingItem = inventory.GetHoldingItem();
			switch(holdingItem.GetComponent<PickUpableItem>().GetItemType()) {

				case "Rock":
					attackType = "Rock";
					damage = holdingItem.GetComponent<PickUpableItem>().damage;
					StartCoroutine(DamageArea(1.3f));
					return;

				case "Branch":
					attackType = "Branch";
					damage = holdingItem.GetComponent<PickUpableItem>().damage;
					StartCoroutine(DamageArea(1.6f));
					return;

				default:
					attackType = "ItemError";
					damage = 1;
					StartCoroutine(DamageArea(1.3f));
					return;
			}
		}
		else {
			StartCoroutine(DamageArea(1.3f));
			return;
		}
		//if none of the above is executed reset cooldown;
		onCooldown = false;
	}

	//OverlapBox check and damage all victims in area.
	IEnumerator DamageArea(float sizeX) {
		gizmoSizeX = sizeX;

		if(status.isMirrored)
			victims = Physics2D.OverlapBoxAll(transform.position + new Vector3(-sizeX / 2, offset), new Vector2(sizeX, 2f), 0, whatIsPunchable);
		else
			victims = Physics2D.OverlapBoxAll(transform.position + new Vector3(sizeX / 2, offset), new Vector2(sizeX, 2f), 0, whatIsPunchable);
		
		foreach(Collider2D victim in victims) {
			var enemyHealth = victim.gameObject.GetComponent<EnemyHealth>();
			if(attackType == "Branch" && !branchInv && victim.gameObject.tag != "Door") {
				if(inventory.GetHoldingItem().GetComponent<PickUpableItem>().Break() <= 0) {
					inventory.SetHoldingItem(null);
				}
				branchInv = true;
			}

			switch(victim.gameObject.tag) {

				case "Door":
					victim.gameObject.GetComponent<Door>().SetInvisible();
					attackType = "Door";
					break;

				case "Barrier":
					Debug.Log("BarrierType: " + victim.gameObject.GetComponent<Barrier>().GetBarrierType());
					if(attackType == victim.gameObject.GetComponent<Barrier>().GetBarrierType())
						victim.gameObject.GetComponent<Barrier>().TakeDamage(damage);
			//		attackType = "Barrier";
					break;

				case "SmallCritter":
				case "JumpingCritter":
					enemyHealth.TakeDamage(damage, gameObject, 4f);
					break;

				case "CrawlerCritter":
					if(victim.gameObject.GetComponent<CrawlerCritter>().noShell)
						enemyHealth.TakeDamage(damage, gameObject, 4f);
					else
						enemyHealth.Knockback(gameObject, 4f);
					break;

				case "ShellMan":
					ShellMan shellMan = victim.gameObject.GetComponent<ShellMan>();
					if(shellMan.deShelled)
						enemyHealth.TakeDamage(damage);
					else
						enemyHealth.Knockback(gameObject, 4f);
					break;

				case "HardCritter":
					if(attackType == "Rock")
						enemyHealth.TakeDamage(damage, gameObject, 4f);
					else if(attackType == "Mega" || attackType == "FullMega")
						enemyHealth.TakeDamage(damage, gameObject, 4f);
					else
						enemyHealth.Knockback(gameObject, 4f);
					break;

				case "BigEyeBuyWeakSpot":
					if(attackType == "Branch")
						victim.gameObject.transform.parent.GetComponent<BigEyeGuy>().TakeDamage(damage);
					break;

				case "StatueBossEye":
					if(attackType == "Branch") {
						if(inventory.IsHoldingItem()) {
							enemyHealth.TakeDamage(damage);
							inventory.GetHoldingItem().GetComponent<PickUpableItem>().Break(3);
							inventory.SetHoldingItem(null);
						}
					}
					break;

				case "FinalBossWeakSpot":
					if(attackType == "FullMega") {
						Debug.Log("Full MegaPunched the boss!");
						victim.gameObject.GetComponent<Phase1>().TakeDamage(damage);
					}
					else if(attackType == "Mega") {
						Debug.Log("Regular MegaPunched the boss!");
						victim.gameObject.GetComponent<Phase1>().TakeDamage(damage);
					}
					else {
						Debug.Log("Invulnerable to AttackType [ " + attackType + " ]");
					}
					break;

				case "FinalBossArmor2":
					if(victim.gameObject.GetComponent<Phase2>().blued) {
						if(attackType == "FullMega") {
							Debug.Log("Full MegaPunched the boss phase 2!");
							victim.gameObject.GetComponent<Phase2>().TakeDamage(damage);
						}
						else if(attackType == "Mega") {
							Debug.Log("Regular MegaPunched the boss phase 2!");
							victim.gameObject.GetComponent<Phase2>().TakeDamage(damage);
						}
						else {
							Debug.Log("Invulnerable to AttackType [ " + attackType + " ]");
						}
					}
					break;
			}
			Debug.Log("Punched [ " + victim + " ] - tag [ " + victim.gameObject.tag + " ] - attackType [ " + attackType + " ] - damage [ " + damage + " ]");
		}

		punchVisual.transform.localPosition = new Vector3(sizeX / 2, offset);
		punchVisual.GetComponent<Animator>().SetTrigger(attackType);

	//	if(!(attackType == "Door" || attackType == "Barrier"))
			GetComponent<AudioPlayer>().PlayClip(6, 1, 0.7f, 1.3f);

		yield return new WaitForSeconds(0.1f);

		onCooldown = false;
		branchInv = false;
	}
}
