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
		}
	}
}

