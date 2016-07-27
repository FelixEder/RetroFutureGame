using UnityEngine;
using System.Collections;

public class StompTrigger : MonoBehaviour {
	public float knockForce;

	void Start() {
		//Play stomp-animation and sound
	}

	void OnTriggerStay2D(Collider2D col) {
		switch (col.gameObject.tag) {
		
		case "SoftEnemy":
			col.gameObject.GetComponent<SmallCritter> ().TakeDamage (3);
			col.gameObject.GetComponent<EnemyKnockback> ().Knock (GameObject.Find ("Char"), knockForce);
			break;

		case "EyeEnemy":
			//Can't be hurt with stomp, play relevant things
			break;

		case "StompEnemy":
			ShellMan shellMan = col.gameObject.GetComponent<ShellMan> ();
			if (GameObject.Find ("char").GetComponent<CharStomp> ().groundStomping && !shellMan.deShelled) {
				shellMan.GetHurt (1);
			} else if (col.gameObject.GetComponent<CharStomp> ().groundStomping && shellMan.deShelled) {
				shellMan.GetHurt (2);
			}
			else {
				col.gameObject.GetComponent<CharHealth> ().TakeDamage (shellMan.damage);
				col.gameObject.GetComponent<Knockback> ().Knock (this.gameObject, knockForce);
			}
			break;

		case "HardEnemy":
			col.gameObject.GetComponent<SmallCritter> ().TakeDamage (2);
			col.gameObject.GetComponent<EnemyKnockback> ().Knock (GameObject.Find ("Char"), knockForce);
			break;
		}
	}
}