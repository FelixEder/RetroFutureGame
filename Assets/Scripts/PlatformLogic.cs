using UnityEngine;
using System.Collections;

public class PlatformLogic : MonoBehaviour {
	void Update() {

	}

	void OnCollisionEnter2d(Collision2D col) {
		if (col.gameObject.name == "platformTrigger") {
			Physics2D.IgnoreCollision (col.collider, GameObject.Find ("char").GetComponent<Collider2D> (), true);
		}
	}

	void OnCollisionExit2d(Collision2D col) {
		if (col.gameObject.name == "platformTrigger") {
			Physics2D.IgnoreCollision (col.collider, GameObject.Find ("char").GetComponent<Collider2D> (), false);
		}
	}
}

