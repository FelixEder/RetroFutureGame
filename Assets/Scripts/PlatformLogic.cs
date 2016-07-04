using UnityEngine;
using System.Collections;

public class PlatformLogic : MonoBehaviour {
	void Update() {

	}

	void OnCollisionEnter2d(Collission2d col) {
		if (col.gameObject.tag == "charPlatformCollider") {
			GetComponent<Collider> ().isTrigger = true;
		}
	}

	void OnTriggerExit2d(Collider2d col) {
		if (col.gameObject.tag == "charPlatformCollider") {
			GetComponent<Collider> ().isTrigger = false;
		}
	}
}

