using UnityEngine;
using System.Collections;

//Left facing Character collission trigger
public class CharTriggerBack : MonoBehaviour {
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
