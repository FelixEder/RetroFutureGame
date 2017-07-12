using UnityEngine;
using System.Collections;

public class Stomp : MonoBehaviour {
	Rigidbody2D rigidBody2D;
	PlayerStatus status;
	InputManager input;

	public float knockForce;
	bool holdStomp, isStomping;
	public LayerMask whatIsHurtable;
	public Transform stompCenter;
	Collider2D[] victims;

	// Use this for initialization
	void Start() {
		rigidBody2D = GetComponent<Rigidbody2D>();
		status = GetComponent<PlayerStatus>();
		input = GameObject.Find("InputManager").GetComponent<InputManager>();
		//Change sprite, display correct tutorial and play theme.
	}

	void FixedUpdate() {
		if(!input.GetKey("attack"))
			holdStomp = false;
		if(status.InAir() && !status.isSmall && !status.isFloating && input.GetAxis("Y") < -0.3f && input.GetAxis("ysign") < 0f && input.GetKey("attack") && !holdStomp) {
			holdStomp = true;
			isStomping = true;

			StartCoroutine(StartStomp());
		}
		else if(status.grounded && isStomping) {

			LandStomp();
		}
	}


	IEnumerator StartStomp() {
		Debug.Log("Started Stomp");
		rigidBody2D.velocity = new Vector2(0, 0);
		rigidBody2D.gravityScale = 0.0f;

		yield return new WaitForSeconds(0.2f);
		status.invulnerable = true;
		rigidBody2D.gravityScale = 2.0f;
		rigidBody2D.velocity = new Vector2(0, -9f);
	}

	void LandStomp() {
		Debug.Log("Finished Stomp");
		GetComponent<AudioPlayer>().PlayClip(5, 2f);
		isStomping = false;
		status.Invulnerable(0.2f);

		victims = Physics2D.OverlapBoxAll(stompCenter.position, new Vector2(4f, 2f), 0, whatIsHurtable);

		foreach(Collider2D victim in victims) {
			var enemyHealth = victim.gameObject.GetComponent<EnemyHealth>();
			switch(victim.gameObject.tag) {

				case "SmallCritter":
				case "JumpingCritter":
					enemyHealth.TakeDamage(3, gameObject, knockForce);
					break;

				case "CrawlerCritter":
					Debug.Log("Hit crawler!");
					var crawler = victim.gameObject.GetComponent<CrawlerCritter>();
					if(!crawler.noShell) {
						enemyHealth.TakeDamage(1);
						crawler.BreakShell();
					}
					else
						enemyHealth.TakeDamage(2);
					break;

				case "ShellMan":
					ShellMan shellMan = victim.gameObject.GetComponent<ShellMan>();
					if(!shellMan.deShelled) {
						enemyHealth.TakeDamage(1);
					}
					else if(shellMan.deShelled) {
						enemyHealth.TakeDamage(2);
					}
					break;

				case "FinalBossLastForm":
					victim.gameObject.GetComponent<Phase3Head>().TakeDamage();
					break;
			}
			Debug.Log("STOMPED: " + victim.gameObject.name + " with tag: " + victim.gameObject.tag);
		}
	}
}