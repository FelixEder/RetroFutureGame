using UnityEngine;
using System.Collections;

public class CharTriggerDown : MonoBehaviour {
	//Down facing character collission trigger
	CharStatus status;

	void Start () {
		status = GameObject.Find ("char").GetComponent<CharStatus> ();
	}

	void OnTriggerStay2D(Collider2D col) {
		if(col.gameObject.tag == "ground") {
			status.onGround = true;
		}
		if(col.gameObject.tag == "platform") {
			status.onPlatform = true;
		}
	}
		
	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "ground") {
			status.onGround = false;
		}
		if(col.gameObject.tag == "platform") {
			status.onPlatform = false;
		}
	}
}