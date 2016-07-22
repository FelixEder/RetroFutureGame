using UnityEngine;
using System.Collections;

public class CharTriggerBack : MonoBehaviour {
	//Left side character collission trigger
	CharStatus status;

	void Start () {
		status = GameObject.Find ("char").GetComponent<CharStatus> ();
	}

	void OnTriggerStay2D(Collider2D col) {
		if(col.gameObject.tag == "wall") {
			if (status.isMirrored) {
				status.onRightWall = true;
			} else {
				status.onLeftWall = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "wall") {
			status.onLeftWall = false;
			status.onRightWall = false;
		}
	}
}
