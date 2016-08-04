using UnityEngine;
using System.Collections;

public class CharMirror : MonoBehaviour {
	CharStatus status;
	InputManager input;

	void Start () {
		status = GetComponent<CharStatus> ();
		input = GameObject.Find ("InputManager").GetComponent<InputManager> ();
	}

	void Update () {
		//Rotate Character model
		if (input.GetAxis("X") < 0 && (status.onSurface || status.onLeftWall || status.onRightWall)) {
			if (!status.isMirrored) {
				transform.rotation = Quaternion.Euler(0, 180, 0);
				status.isMirrored = true;
			}
		}
		else if (input.GetAxis("X") > 0 && (status.onSurface || status.onLeftWall || status.onRightWall)) {
			if (status.isMirrored) {
				transform.rotation = Quaternion.Euler(0, 0, 0);
				status.isMirrored = false;
			}
		}
	}
}
