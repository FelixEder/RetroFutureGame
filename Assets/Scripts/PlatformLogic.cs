using UnityEngine;
using System.Collections;

public class PlatformLogic : MonoBehaviour {
	void Update() {
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name == "platformTrigger") {
			Debug.Log ("collision enter");
			Physics2D.IgnoreCollision (GetComponent<Collider2D>(), GameObject.Find("char").GetComponent<Collider2D>(), true);
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.name == "platformTrigger") {
			Debug.Log ("collision exit");
			Physics2D.IgnoreCollision (GetComponent<Collider2D>(), GameObject.Find("char").GetComponent<Collider2D>(), false);
		}
	}
}

