using UnityEngine;
using System.Collections;

public class PlatformLogic : MonoBehaviour {
	public bool jumpDown;

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name == "platformTrigger") {
			Physics2D.IgnoreLayerCollision (8, 9, true);
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.name == "platformTrigger") {
			Physics2D.IgnoreLayerCollision (8, 9, false);
		}
	}
}

