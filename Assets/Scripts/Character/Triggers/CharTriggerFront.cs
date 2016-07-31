using UnityEngine;
using System.Collections;

//Right facing Character collission trigger
public class CharTriggerFront : MonoBehaviour {
	CharStatus status;

	void Start () {
		status = GameObject.Find ("Char").GetComponent<CharStatus> ();
	}
		
	void OnTriggerStay2D(Collider2D col) {
		if(col.gameObject.tag == "Wall") {
			if (status.isMirrored) {
				status.onLeftWall = true;
			} else {
				status.onRightWall = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Wall") {
			status.onRightWall = false;
			status.onLeftWall = false;
		}
	}
}
