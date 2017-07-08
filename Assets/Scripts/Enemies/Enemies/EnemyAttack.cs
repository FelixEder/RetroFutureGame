using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
	public int damage;
	public float knockbackForce;

	void OnCollisionStay2D(Collision2D col) {
		switch(col.gameObject.tag) {

			case "Char":
				col.gameObject.GetComponent<CharHealth>().TakeDamage(damage, gameObject, knockbackForce);

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