using UnityEngine;
using System.Collections;

public class CharMirror : MonoBehaviour {
	CharStatus status;
	InputManager input;
	Rigidbody2D rb2D;

	void Start () {
		status = GetComponent<CharStatus> ();
		input = GameObject.Find ("InputManager").GetComponent<InputManager> ();
		rb2D = GetComponent<Rigidbody2D> ();
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
		else if (input.GetAxis("X") < 0 && rb2D.velocity.x < -1 && !(status.onSurface || status.onLeftWall || status.onRightWall)) {
			if (!status.isMirrored) {
				transform.rotation = Quaternion.Euler(0, 180, 0);
				status.isMirrored = true;
			}
		}
		else if (input.GetAxis("X") > 0 && rb2D.velocity.x > 1 && !(status.onSurface || status.onLeftWall || status.onRightWall)) {
			if (status.isMirrored) {
				transform.rotation = Quaternion.Euler(0, 0, 0);
				status.isMirrored = false;
			}
		}
	}
}
