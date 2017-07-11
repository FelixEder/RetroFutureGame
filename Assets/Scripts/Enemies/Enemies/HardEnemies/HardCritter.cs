using UnityEngine;
using System.Collections;

public class HardCritter : MonoBehaviour {
	public float rushSpeed;
	public int rushDamage;
	[Range(1, 100)]
	public int rushChance = 1;
	[Space(10)]
	public Vector3 targetcheckPos = Vector2.one;
	public LayerMask targetMask = 256;

	float originalSpeed;
	int originalDamage;
	bool rushing, rushcheck;
	Rigidbody2D rb2D;
	EnemyMovement movement;
	EnemyAttack attack;

	void OnDrawGizmosSelected() {
		Gizmos.color = new Color(0, 1, 0, 0.5f); //green
		Gizmos.DrawCube(transform.position - new Vector3((targetcheckPos.x + targetcheckPos.z) / 2 * FrontCheckDir(), 0, 0), new Vector3(targetcheckPos.x - targetcheckPos.z, targetcheckPos.y, 0));
	}

	void Start() {
		rb2D = GetComponent<Rigidbody2D>();
		movement = GetComponent<EnemyMovement>();
		attack = GetComponent<EnemyAttack>();

		originalDamage = attack.damage;
		originalSpeed = movement.moveSpeed;
	}

	void Update() {
		rushcheck = Physics2D.OverlapBox(transform.position - new Vector3((targetcheckPos.x + targetcheckPos.z) / 2 * FrontCheckDir(), 0, 0), new Vector3(targetcheckPos.x - targetcheckPos.z, targetcheckPos.y, 0), 0, targetMask);

		if(rushcheck) {
			if(Random.Range(0, 100) < rushChance)
				StartCoroutine(Rush());
		}
	}


	void OnCollisionEnter2D(Collision2D col) {

		switch(col.gameObject.tag) {

			case "SmallCritter":
			case "JumpingCritter":
			case "HardEnemy":
			case "BigEyeGuy":
			case "CrawlerCritter":
			case "ShellMan":
				break;

			case "Char":
			case "Wall":
			case "Door":
			case "Barrier":
				break;
		}
	}

	IEnumerator Rush() {
		if(!rushing) {
			Debug.Log("HardCritter is preparing rush");
			rushing = true;
			movement.moveSpeed = 0;
			movement.wanderSpeed = 0;

			yield return new WaitForSeconds(1f);

			Debug.Log("HardCritter is rushing");
			attack.damage = rushDamage;
			movement.moveSpeed = rushSpeed;
			movement.wanderSpeed = rushSpeed;

			yield return new WaitForSeconds(1f);

			Debug.Log("HardCritter stopped rushing");
			attack.damage = originalDamage;
			movement.moveSpeed = originalSpeed;
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