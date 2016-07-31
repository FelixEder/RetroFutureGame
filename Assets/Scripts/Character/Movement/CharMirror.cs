using UnityEngine;
using System.Collections;

public class CharMirror : MonoBehaviour {
	CharStatus status;

	void Start () {
		status = GetComponent<CharStatus> ();
	}

	void Update () {
		//Rotate Character model
		if (Input.GetAxis("Horizontal") < 0 && (status.onSurface || status.onLeftWall || status.onRightWall)) {
			if (!status.isMirrored) {
				transform.rotation = Quaternion.Euler(0, 180, 0);
				status.isMirrored = true;
			}
		} else if (Input.GetAxis("Horizontal") > 0 && (status.onSurface || status.onLeftWall || status.onRightWall)) {
			if (status.isMirrored) {
				transform.rotation = Quaternion.Euler(0, 0, 0);
				status.isMirrored = false;
			}
		}
	}
}
