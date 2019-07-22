using UnityEngine;
using System.Collections;

public class PlayerPunch : MonoBehaviour {
	
	public int minLimit, maxLimit = 200;
	public float offset;
	public GameObject punchVisual;
	public LayerMask whatIsPunchable;

	bool animationCooldown, holdPunch, onCooldown, branchInv, megaAcquired;
	string damageType;
	int damage, charge;
	float gizmoSizeX = 1f;

	PlayerEnergy energy;
	PlayerInventory inventory;
	PlayerStatus status;
	InputManager input;

    bool punchInput;

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
        if(GetComponent<PlayerInventory>().HasAcquired("float") && !megaAcquired)
            megaAcquired = true;
		
		if(!input.GetKey("attack") && holdPunch) {
			holdPunch = false;
		}
		else if(input.GetKey("attack") && !holdPunch && !status.isSmall) {
			holdPunch = true;
			damageType = "Punch";
			damage = 1;
			if (!onCooldown) {
				Debug.Log("Calling Regular Punch");
				RegularPunch();
			}
			if (megaAcquired) {
				Debug.Log("Calling Mega Charge");
				StartCoroutine(MegaCharge());
			}
		}
	}
	
	//Regular Punch
	void RegularPunch() {
		if(inventory.IsHoldingItem()) {
			GameObject holdingItem = inventory.GetHoldingItem();
			switch(holdingItem.GetComponent<PickUpableItem>().GetItemType()) {

				case "Rock":
					damageType = "Rock";
					damage = holdingItem.GetComponent<PickUpableItem>().damage;
					StartCoroutine(DamageArea(1.3f));
					return;

				case "Branch":
					damageType = "Branch";
					damage = holdingItem.GetComponent<PickUpableItem>().damage;
					StartCoroutine(DamageArea(1.6f));
					return;

				default:
					damageType = "ItemError";
					damage = 1;
					StartCoroutine(DamageArea(1.3f));
					return;
			}
		}
		else {
			StartCoroutine(DamageArea(1.3f));
			return;
		}
	}

	//MEGA
	IEnumerator MegaCharge() {
		while(input.GetKey("attack")) {
			while(charge < maxLimit && input.GetKey("attack")) {
				charge ++;
				if(charge == minLimit || charge == maxLimit)
					Debug.Log("MegaPunch charge [ " + charge + " ]");
				yield return 0;
			}
			yield return 0;
		}
		MegaType();
		charge = 0;
	}

	void MegaType() {
		onCooldown = true;
		if(charge == maxLimit && megaAcquired) {
			if(energy.UseEnergy(3)) {
				Debug.Log("Full MegaPunch");
				damageType = "FullMega";
				damage = 5;
				StartCoroutine(DamageArea(2.5f));
				return;
			}
			else {
				Debug.Log("Not enough energy for Full MegaPunch");
				//No energy, play correct things
			}
		}
		else if(charge >= minLimit && megaAcquired) {
			if(energy.UseEnergy(1)) {
				Debug.Log("Regular MegaPunch");
				damageType = "Mega";
				damage = 3;
				StartCoroutine(DamageArea(2.2f));
				return;
			}
			else {
				Debug.Log("Not enough energy for Regular MegaPunch");
				//No energy, play correct things
			}
		}
		//if none of the above is executed reset cooldown;
		onCooldown = false;
	}

	//OverlapBox check and damage all victims in area.
	IEnumerator DamageArea(float sizeX) {
		gizmoSizeX = sizeX;
        Vector3 distance;
        if(status.isMirrored)
            distance = new Vector3(-sizeX / 2, offset);
		else
            distance = new Vector3(sizeX / 2, offset);
		victims = Physics2D.OverlapBoxAll(transform.position + distance, new Vector2(sizeX, 2f), 0, whatIsPunchable);
		
		foreach(Collider2D victim in victims) {
			var enemyHealth = victim.gameObject.GetComponent<EnemyHealth>();
			if(damageType == "Branch" && !branchInv && victim.gameObject.tag != "Door") {
				if(inventory.GetHoldingItem().GetComponent<PickUpableItem>().Break() <= 0) {
					inventory.SetHoldingItem(null);
				}
				branchInv = true;
			}

			switch(victim.gameObject.tag) {

				case "Door":
					victim.gameObject.GetComponent<Door>().SetInvisible();
					damageType = "Door";
					break;

				case "Barrier":
					Debug.Log("BarrierType: " + victim.gameObject.GetComponent<Barrier>().GetBarrierType());
					if(damageType == victim.gameObject.GetComponent<Barrier>().GetBarrierType())
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
					if(shellMan.getDeShelled())
						enemyHealth.TakeDamage(damage, gameObject, 4f);
					else {
						if(damageType == "Mega" || damageType == "FullMega") 
							shellMan.BreakShield(gameObject);
						else
							enemyHealth.Knockback(gameObject, 4f);
					}
					break;

				case "HardCritter":
					if(damageType == "Rock")
						enemyHealth.TakeDamage(damage, gameObject, 4f);
					else if(damageType == "Mega" || damageType == "FullMega")
						enemyHealth.TakeDamage(damage, gameObject, 4f);
					else
						enemyHealth.Knockback(gameObject, 4f);
					break;

				case "BigEyeGuy":
					enemyHealth.Knockback(gameObject, 4f);
					break;

				case "StatueBossEye":
					if(damageType == "Branch") {
						if(inventory.IsHoldingItem()) {
							enemyHealth.TakeDamage(damage);
							inventory.GetHoldingItem().GetComponent<PickUpableItem>().Break(3);
							inventory.SetHoldingItem(null);
						}
					}
					break;

				case "FinalBossWeakSpot":
					if(damageType == "FullMega") {
						Debug.Log("Full MegaPunched the boss!");
						victim.gameObject.GetComponent<Phase1>().TakeDamage(damage);
					}
					else if(damageType == "Mega") {
						Debug.Log("Regular MegaPunched the boss!");
						victim.gameObject.GetComponent<Phase1>().TakeDamage(damage);
					}
					else {
						Debug.Log("Invulnerable to AttackType [ " + damageType + " ]");
					}
					break;

				case "FinalBossArmor2":
					if(victim.gameObject.GetComponent<Phase2>().blued) {
						if(damageType == "FullMega") {
							Debug.Log("Full MegaPunched the boss phase 2!");
							victim.gameObject.GetComponent<Phase2>().TakeDamage(damage);
						}
						else if(damageType == "Mega") {
							Debug.Log("Regular MegaPunched the boss phase 2!");
							victim.gameObject.GetComponent<Phase2>().TakeDamage(damage);
						}
						else {
							Debug.Log("Invulnerable to AttackType [ " + damageType + " ]");
						}
					}
					break;
			}
			Debug.Log("Punched [ " + victim + " ] - tag [ " + victim.gameObject.tag + " ] - attackType [ " + damageType + " ] - damage [ " + damage + " ]");
		}

		punchVisual.transform.localPosition = new Vector3(sizeX / 2, offset);
		punchVisual.GetComponent<Animator>().SetTrigger(damageType);

	//	if(!(attackType == "Door" || attackType == "Barrier"))
			GetComponent<AudioPlayer>().PlayClip(6, 1, 0.7f, 1.3f);

		yield return new WaitForSeconds(0.1f);

		onCooldown = false;
		branchInv = false;
	}
}
