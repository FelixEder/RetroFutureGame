using UnityEngine;
using System.Collections;

public class StompTrigger : MonoBehaviour {
	public float knockForce;

	void Start() {
		//Play stomp-animation and sound
	}

	void OnTriggerStay2D(Collider2D col) {
		switch (col.gameObject.tag) {
		
		case "softEnemy":
			col.gameObject.GetComponent<SmallCritter> ().GetHurt (3);
			col.gameObject.GetComponent<EnemyKnockback> ().Knock (GameObject.Find ("char"), knockForce);
			break;

		case "eyeEnemy":
			//Can't be hurt with stomp, play relevant things
			break;

		case "stompEnemy":
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

		case "hardEnemy":
			col.gameObject.GetComponent<SmallCritter> ().GetHurt (2);
			col.gameObject.GetComponent<EnemyKnockback> ().Knock (GameObject.Find ("char"), knockForce);
			break;
		}
	}
}