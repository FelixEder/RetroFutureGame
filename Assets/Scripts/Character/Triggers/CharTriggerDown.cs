using UnityEngine;
using System.Collections;

//Down facing Character collission trigger
public class CharTriggerDown : MonoBehaviour {
	public CharStatus status;

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