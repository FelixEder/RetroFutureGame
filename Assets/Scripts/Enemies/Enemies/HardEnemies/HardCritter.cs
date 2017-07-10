using UnityEngine;
using System.Collections;

public class HardCritter : MonoBehaviour {
	int rushDamage, originalDamage;
	float rushSpeed, originalSpeed;
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
				if(Random.Range(0, 4) == 0)
					Rush();
				break;
		}
	}

	public void Rush() {
		//Enemy is rushing, play relevant things
		if(!rushing) {
			Debug.Log("Enemy is rushing");
			attack.damage = rushDamage;
			movement.moveSpeed = rushSpeed;
			rushing = true;
			Invoke("StopRush", 1f);
		}
	}

	void StopRush() {
		Debug.Log("Enemy stopped rushing");
		//Enemy stops rushing, play relevant things
		attack.damage = originalDamage;
		movement.moveSpeed = originalSpeed;
		rushing = false;
	}
}