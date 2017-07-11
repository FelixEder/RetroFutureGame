using UnityEngine;
using System.Collections;

public class PlatformLogic : MonoBehaviour {
	PlayerJump jump;
	/*
		void Start() {
			jump = GameObject.Find ("Player").GetComponent<PlayerJump> ();
		}

		void OnTriggerEnter2D(Collider2D col) {
			if (col.gameObject.name == "Player")
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
			if (col.gameObject.name == "Player")
				ignoreCollision (false);
		}
		public void ignoreCollision(bool input) {
			Physics2D.IgnoreCollision (GameObject.Find ("Player").GetComponent<Collider2D> (), transform.parent.GetComponent<Collider2D> (), input);
		}
		*/
}

