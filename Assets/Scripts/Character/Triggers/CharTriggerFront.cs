using UnityEngine;
using System.Collections;

public class CharTriggerFront : MonoBehaviour {
	//Right side Character collission trigger
	CharStatus status;

	void Start () {
		status = GameObject.Find ("Char").GetComponent<CharStatus> ();
	}
		
	void OnTriggerStay2D(Collider2D col) {
		if(col.gameObject.tag == "Wall") {
			Debug.Log ("Colliding with " + col);
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
