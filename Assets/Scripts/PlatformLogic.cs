using UnityEngine;
using System.Collections;

public class PlatformLogic : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name == "char") {
			Physics2D.IgnoreLayerCollision (8, 9, false);
			Debug.Log ("enter trigger");
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.name == "char") {
			Physics2D.IgnoreLayerCollision (8, 9, true);
			Debug.Log ("exit trigger");
		}
	}
}

