using UnityEngine;
using System.Collections;

public class CrawlerCritter : MonoBehaviour {
	public bool noShell;
	public float newKnockbackForce;
	public int newDamage;
	public FollowRange newFollowRange;

	Rigidbody2D rb2D;
	Animator anim;

	void Start() {
		rb2D = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	public void BreakShell() {
		anim.SetTrigger("next");
		noShell = true;

		var attack = GetComponent<EnemyAttack>();
		attack.damage = newDamage;
		attack.knockbackForce = newKnockbackForce;
		var movement = GetComponent<EnemyMovement>();
		movement.followRange = newFollowRange;
		var jump = GetComponent<EnemyJump>();
		jump.enabled = true;
	}
}