using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
	public int damage = 1;
	public float knockbackForce = 5;

	void OnCollisionStay2D(Collision2D col) {
		switch(col.gameObject.tag) {

			case "Player":
				col.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage, gameObject.transform.position, knockbackForce);

				break;
		}
	}

	/*
	void OnBecameVisible() {
		if(initialFreezeTime == 0)
			InitializeMoveSpeed();
	}

	void InitializeMoveSpeed() {
		activeMoveSpeed = moveSpeed;
	}
	*/
}