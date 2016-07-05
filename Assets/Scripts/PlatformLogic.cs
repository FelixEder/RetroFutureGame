using UnityEngine;
using System.Collections;

public class PlatformLogic : MonoBehaviour {
	public bool jumpDown;

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name == "platformTrigger") {
			IgnorePlatformCollision (true);
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.name == "platformTrigger") {
			IgnorePlatformCollision (false);
		}
	}

	public void IgnorePlatformCollision(bool input) {
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), GameObject.Find ("char").GetComponent<Collider2D> (), input);
	}
}

