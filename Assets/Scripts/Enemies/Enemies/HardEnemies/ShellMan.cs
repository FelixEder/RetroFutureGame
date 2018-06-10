using UnityEngine;
using System.Collections;

public class ShellMan : MonoBehaviour {
	private bool deShelled;
	private Animator anim;
	public int newDamage;
	public float newKnockbackForce, newMoveSpeed, newFollowSpeed;

	void Start() {
		anim = GetComponent<Animator>();
	}

	public void BreakShield(GameObject attacker) {
		anim.SetTrigger("next");
		deShelled = true;

		GetComponent<EnemyHealth>().Knockback(attacker, 5f);

		var attack = GetComponent<EnemyAttack>();
		attack.damage = newDamage;

		var movement = GetComponent<EnemyMovement>();
		movement.wanderSpeed = newMoveSpeed;
		movement.followSpeed = newFollowSpeed;
	}

	public bool getDeShelled() {
		return deShelled;
	}
}