using UnityEngine;
using System.Collections;

public class HardCritter : MonoBehaviour {
	public float rushSpeed;
	public int rushDamage;

	float originalSpeed;
	int originalDamage;
	bool rushing;
	Rigidbody2D rb2D;
	EnemyMovement movement;
	EnemyAttack attack;

	void Start() {
		rb2D = GetComponent<Rigidbody2D>();
		movement = GetComponent<EnemyMovement>();
		attack = GetComponent<EnemyAttack>();

		originalDamage = attack.damage;
		originalSpeed = movement.moveSpeed;
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
				if(Random.Range(0, 4) < 1)
					Rush();
				break;
			default:
				if(Random.Range(0, 10) < 1)
					Rush();
				break;
		}
	}

	IEnumerator Rush() {
		if(!rushing) {
			Debug.Log("Enemy is preparing rush");
			rushing = true;
			movement.moveSpeed = 0;
			movement.wanderSpeed = 0;
			yield return new WaitForSeconds(1f);

			Debug.Log("Enemy is rushing");
			attack.damage = rushDamage;
			movement.moveSpeed = rushSpeed;
			movement.wanderSpeed = rushSpeed;
			yield return new WaitForSeconds(1f);

			Debug.Log("Enemy stopped rushing");
			attack.damage = originalDamage;
			movement.moveSpeed = originalSpeed;
			movement.wanderSpeed = originalSpeed;
			rushing = false;
		}
	}
}