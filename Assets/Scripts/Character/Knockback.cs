using UnityEngine;
using System.Collections;

public class Knockback : MonoBehaviour {
	float wayofKnock;

	public void Knock(GameObject attacker, float force) {
		if (this.gameObject.transform.position.x <= attacker.transform.position.x)
			wayofKnock = -1;
		else {
			wayofKnock = 1;
		}
		this.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (force * wayofKnock, 0), ForceMode2D.Impulse);
	}
}

