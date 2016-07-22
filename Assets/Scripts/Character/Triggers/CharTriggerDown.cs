using UnityEngine;
using System.Collections;

public class CharTriggerDown : MonoBehaviour {
	//Down facing character collission trigger
	CharStatus status;

	void Start () {
		status = GameObject.Find ("char").GetComponent<CharStatus> ();
	}

	void OnTriggerStay2D(Collider2D col) {
		switch (col.gameObject.tag) {
			case "ground":
				status.onGround = true;
				break;
			
			case "platform":
				status.onPlatform = true;
				break;
		}
		status.onSurface = true;
		Debug.Log (status.onSurface);
	}
		
	void OnTriggerExit2D(Collider2D col) {
		switch (col.gameObject.tag) {
			case "ground":
			status.onGround = false;
			break;

			case "platform":
			status.onPlatform = false;
			break;
		}
		status.onSurface = false;
		Debug.Log ("left trigger /trigger down");
	}
}