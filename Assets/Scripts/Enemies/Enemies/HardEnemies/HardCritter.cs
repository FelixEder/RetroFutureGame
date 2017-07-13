using UnityEngine;
using System.Collections;

public class HardCritter : MonoBehaviour {
	public bool hide;
	public int timeBeforeHide;
	public float rushSpeed;
	public int rushDamage;
	[Range(0, 100)]
	public int rushChance = 1;
	[Space(10)]
	public Vector3 targetcheckPos = Vector2.one, emergedcheckPos = Vector2.one;
	public LayerMask targetMask = 256;

	float originalSpeed, count = 0;
	int originalDamage;
	bool rushing, playerCheck;
	Animator anim;
	EnemyMovement movement;
	EnemyAttack attack;

	void OnDrawGizmosSelected() {
		Gizmos.color = new Color(0, 1, 0, 0.5f); //green
		Gizmos.DrawCube(transform.position - new Vector3((targetcheckPos.x + targetcheckPos.z) / 2 * FrontCheckDir(), 0, 0), new Vector3(targetcheckPos.x - targetcheckPos.z, targetcheckPos.y, 0));
		Gizmos.DrawCube(transform.position - new Vector3((targetcheckPos.x + targetcheckPos.z) / 2 * FrontCheckDir(), 0, 0), new Vector3(targetcheckPos.x - targetcheckPos.z, targetcheckPos.y, 0));
	}

	//MAKE RANGE CHANGE AFTER SPOTTED AND MAKE GIZMO FOR OTHER RANGE.

	void Start() {
		anim = GetComponent<Animator>();
		movement = GetComponent<EnemyMovement>();
		attack = GetComponent<EnemyAttack>();

		originalDamage = attack.damage;
		originalSpeed = movement.followSpeed;

		movement.followSpeed = 0;
		movement.wanderSpeed = 0;
	}

	void Update() {
		playerCheck = Physics2D.OverlapBox(transform.position - new Vector3((targetcheckPos.x + targetcheckPos.z) / 2 * FrontCheckDir(), 0, 0), new Vector3(targetcheckPos.x - targetcheckPos.z, targetcheckPos.y, 0), 0, targetMask);

		if(playerCheck) {
			StartCoroutine(Emerge());
			//	if(Random.Range(0, 100) < rushChance)
			//		StartCoroutine(Rush());
		}

		if(hide && anim.GetBool("move")) {
			if(playerCheck)
				count = 0;
			else
				count += Time.deltaTime;
			if(count > timeBeforeHide)
				StartCoroutine(Hide());
		}
	}

	void OnCollisionEnter2D(Collision2D col) {

		switch(col.gameObject.tag) {

			case "SmallCritter":
			case "JumpingCritter":
			case "HardCritter":
			case "BigEyeGuy":
			case "CrawlerCritter":
			case "ShellMan":
				break;

			case "Player":
			case "Wall":
			case "Door":
			case "Barrier":
				break;
		}
	}

	IEnumerator Emerge() {
		anim.SetBool("move", true);

		yield return new WaitForSeconds(0.5f);

		movement.followSpeed = originalSpeed;
		movement.wanderSpeed = originalSpeed;
	}

	IEnumerator Hide() {
		anim.SetBool("move", false);

		yield return new WaitForSeconds(0.5f);

		movement.followSpeed = 0;
		movement.wanderSpeed = 0;
	}

	//GIVE RUSH TO ANOTHER ENEMY MAYBE?
	IEnumerator Rush() {
		if(!rushing) {
			Debug.Log("HardCritter is preparing rush");
			rushing = true;
			movement.followSpeed = 0;
			movement.wanderSpeed = 0;

			yield return new WaitForSeconds(1f);

			Debug.Log("HardCritter is rushing");
			attack.damage = rushDamage;
			movement.followSpeed = rushSpeed;
			movement.wanderSpeed = rushSpeed;

			yield return new WaitForSeconds(1f);

			Debug.Log("HardCritter stopped rushing");
			attack.damage = originalDamage;
			movement.followSpeed = originalSpeed;
			movement.wanderSpeed = originalSpeed;
			rushing = false;

			yield return new WaitForSeconds(2f);
			Debug.Log("HardCritter rush cooldown ended");
		}
	}

	int FrontCheckDir() {
		if(transform.rotation.y == 0)
			return 1;
		else
			return -1;
	}
}