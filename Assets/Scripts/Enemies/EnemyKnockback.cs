using UnityEngine;
using System.Collections;

public class EnemyKnockback : MonoBehaviour {

	public void Knockback(GameObject attacker, float force) {
		if(transform.position.x < attacker.transform.position.x)
			GetComponent<Rigidbody2D>().velocity = new Vector2(-force, 2);
		else
			GetComponent<Rigidbody2D>().velocity = new Vector2(force, 2);
	}
}

