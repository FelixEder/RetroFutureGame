using UnityEngine;
using System.Collections;

public class PlatformLogic : MonoBehaviour {
	CharJump jump;

	void Start() {
		jump = GameObject.Find ("char").GetComponent<CharJump> ();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name == "char") {
			ignoreCollision (true);
		}
			if (col.gameObject.name == "triggerPlatform" && jump.jumpDown) {
				ignoreCollision (true);
				jump.jumpDown = false;
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.name == "triggerPlatform" && jump.jumpDown) {
			ignoreCollision (true);
			jump.jumpDown = false;
		}
	}
	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.name == "char") {
			ignoreCollision (false);
		}
	}
	public void ignoreCollision(bool input) {
		Physics2D.IgnoreCollision (GameObject.Find ("char").GetComponent<Collider2D> (), transform.parent.GetComponent<Collider2D> (), input);
	}
}

