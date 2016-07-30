using UnityEngine;
using System.Collections;

public class CharTriggerBack : MonoBehaviour {
	//Left side Character collission trigger
	CharStatus status;

	void Start () {
		status = GameObject.Find ("Char").GetComponent<CharStatus> ();
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.tag == "Wall") {
			if (status.isMirrored) {
				status.onRightWall = true;
			} else {
				status.onLeftWall = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Wall") {
			status.onLeftWall = false;
			status.onRightWall = false;
		}
	}
}
