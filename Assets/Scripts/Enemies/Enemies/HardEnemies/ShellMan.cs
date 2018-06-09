using UnityEngine;
using System.Collections;

public class ShellMan : MonoBehaviour {
	private bool deShelled;
	private Animator anim;
	public int newDamage;
	private float newKnockbackForce;
	public FollowRange newFollowRange;

	void Start() {
		anim = GetComponent<Animator>();
	}

	public void BreakShield() {
		anim.SetTrigger("next");
		deShelled = true;

		var attack = GetComponent<EnemyAttack>();
		attack.damage = newDamage;
		attack.knockbackForce = newKnockbackForce;

		var moment = GetComponent<EnemyMovement>();
		moment.followRange = newFollowRange;
		var jump = GetComponent<EnemyJump>();
		jump.enabled = true;
	}

	public bool getDeShelled() {
		return deShelled;
	}
}