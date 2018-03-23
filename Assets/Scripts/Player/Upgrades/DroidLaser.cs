using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidLaser : MonoBehaviour {
	public int damage = 2;
	public GameObject player;
	public LayerMask hitLayers;
	public bool canShoot = true;

	bool holdShoot;

	Vector3 aimDir;
	Vector2 analogDir;

	Animator anim;
	LineRenderer line;
	PlayerStatus status;
	PlayerEnergy energy;
	InputManager input;
	RaycastHit2D raycastHit;


	void Start() {
		status = player.GetComponent<PlayerStatus>();
		energy = player.GetComponent<PlayerEnergy>();
		input = GameObject.Find("InputManager").GetComponent<InputManager>();

		anim = GetComponent<Animator>();
		line = GetComponent<LineRenderer>();
		line.useWorldSpace = true;
	}

	void Update() {
		var origin = transform.position + new Vector3(0, 0.3f, -5f);

		aimDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - origin;
		analogDir = new Vector2(Input.GetAxis("RightAnalogH"), Input.GetAxis("RightAnalogV"));
		if(analogDir.magnitude != 0)
			aimDir = analogDir;

		raycastHit = Physics2D.Raycast(origin, aimDir, Mathf.Infinity, hitLayers);

		if(raycastHit) {
			line.SetPosition(0, origin);
			line.SetPosition(1, raycastHit.point);
		}

		if(input.GetKey("shoot") && !holdShoot) {
			holdShoot = true;
		}

		if(!input.GetKey("shoot") && holdShoot && canShoot) {
			if(energy.UseEnergy(2)) {
				canShoot = false;
				StartCoroutine(ActivateLaser());
			}
		}

		if(!input.GetKey("shoot")) {
			holdShoot = false;
		}
	}

	IEnumerator ActivateLaser() {
		if(raycastHit) {
			line.enabled = true;
			HitByLaser(raycastHit);

			yield return new WaitForSeconds(0.1f);

			anim.SetTrigger("charge");
			line.enabled = false;

			yield return new WaitForSeconds(1f);

			canShoot = true;
		}
		else
			canShoot = true;
	}

	void HitByLaser(RaycastHit2D victim) {
		Debug.Log("Player shot: " + victim.collider.gameObject.name + " with tag: " + victim.collider.gameObject.tag);

		var enemyHealth = victim.collider.gameObject.GetComponent<EnemyHealth>();

		//NOTE: RaycastHit2D.transform returns parent transform, RaycastHit2D.collider returns the hit collider.
		switch(victim.collider.gameObject.tag) {

			//Add more cases as more types of enemies are added to the game
			case "SmallCritter":
			case "JumpingCritter":
				enemyHealth.TakeDamage(damage);
				break;

			case "HardCritter":
				//victim.transform.gameObject.GetComponent<HardCritter>().Rush();
				break;

			case "BigEyeGuy":
				//Can't be hurt by laser, play relevant things
				break;

			case "CrawlerCritter":
				if(victim.collider.gameObject.GetComponent<CrawlerCritter>().noShell) {
					enemyHealth.TakeDamage(damage);
				}
				else {
					//Can't be hurt by laser, play relevant things
				}
				break;

			case "ShellMan":
				ShellMan shellMan = victim.collider.gameObject.GetComponent<ShellMan>();
				if(shellMan.deShelled) {
					enemyHealth.TakeDamage(damage);
				}
				else {
					//Can't be hurt by laser, play relevant things
				}
				break;

			case "BirdBossWeakSpot":
				Debug.Log("Hit Bird in the Mouth!");
				victim.collider.gameObject.GetComponent<BigBadBird>().TakeDamage();
				break;

			case "BigEyeGuyWeakSpot":
				Debug.Log("Hit EyeGuy in the Eye!");
				victim.collider.gameObject.GetComponent<BigEyeGuy>().TakeDamage(damage);
				break;

			case "FinalBossArmor":
				Debug.Log("Hit the boss in the armor!");
				victim.collider.gameObject.GetComponent<Phase1>().Stunned(7f);
				break;

			case "FinalBossArmor2":
				victim.collider.gameObject.GetComponent<Phase2>().Stunned(7f);
				break;

			case "FinalBossWeakSpot":
				Debug.Log("Hit the boss in the weak spot!");
				victim.collider.gameObject.GetComponent<Phase1>().Fall();
				break;

			case "FinalBossHead":
				Debug.Log("Hit the boss in the head!");
				victim.collider.gameObject.GetComponent<Phase2>().LaserShot();
				break;

			case "FinalBossLastForm":
				Debug.Log("Shot last boss form");
				//Check so that it goes after the correct child
				victim.collider.gameObject.transform.GetChild(0).GetComponent<BigEyeGuyLaser>().Shoot();
				break;
		}
	}
}
