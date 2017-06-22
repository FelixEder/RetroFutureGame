using UnityEngine;
using System.Collections;

public class PlatformLogic : MonoBehaviour {
	CharJump jump;
/*
	void Start() {
		jump = GameObject.Find ("Char").GetComponent<CharJump> ();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name == "Char")
			ignoreCollision (true);
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
		if (col.gameObject.name == "Char")
			ignoreCollision (false);
	}
	public void ignoreCollision(bool input) {
		Physics2D.IgnoreCollision (GameObject.Find ("Char").GetComponent<Collider2D> (), transform.parent.GetComponent<Collider2D> (), input);
	}
	*/
}

