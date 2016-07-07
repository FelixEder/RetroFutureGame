using UnityEngine;
using System.Collections;

public class CharTriggerFront : MonoBehaviour {
	//Right side character collission trigger
	CharStatus status;

	void Start () {
		status = GameObject.Find ("char").GetComponent<CharStatus> ();
	}
		
	void OnTriggerStay2D(Collider2D col) {
		if(col.gameObject.tag == "wall") {
			if (status.isMirrored) {
				status.onLeftWall = true;
			} else {
				status.onRightWall = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "wall") {
			status.onRightWall = false;
			status.onLeftWall = false;
		}
	}
}
