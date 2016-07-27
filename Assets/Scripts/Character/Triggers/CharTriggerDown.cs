using UnityEngine;
using System.Collections;

public class CharTriggerDown : MonoBehaviour {
	//Down facing Character collission trigger
	CharStatus status;

	void Start () {
		status = GameObject.Find ("Char").GetComponent<CharStatus> ();
	}

	void OnTriggerStay2D(Collider2D col) {
		switch (col.gameObject.tag) {
			case "Ground":
				status.onGround = true;
				break;
			
			case "Platform":
				status.onPlatform = true;
				break;
		}
		status.onSurface = true;
	}
		
	void OnTriggerExit2D(Collider2D col) {
		switch (col.gameObject.tag) {
			case "Ground":
			status.onGround = false;
			break;

			case "Platform":
			status.onPlatform = false;
			break;
		}
		status.onSurface = false;
	}
}