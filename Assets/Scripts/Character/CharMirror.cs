using UnityEngine;
using System.Collections;

public class CharMirror : MonoBehaviour {
	CharStatus status;

	void Start () {
		status = GetComponent<CharStatus> ();
	}

	void Update () {
		//Rotate character model when moving and on ground
		if (status.onGround || status.onPlatform || status.onLeftWall || status.onRightWall) {
			if (Input.GetAxis("Horizontal") < 0) {
				if (!status.isMirrored) {
					transform.rotation = Quaternion.Euler(0, 180, 0);
					status.isMirrored = true;
				}
			} else if (Input.GetAxis("Horizontal") > 0) {
				if (status.isMirrored) {
					transform.rotation = Quaternion.Euler(0, 0, 0);
					status.isMirrored = false;
				}
			}
		}
	}
}
