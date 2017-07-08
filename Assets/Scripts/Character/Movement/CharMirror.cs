using UnityEngine;
using System.Collections;

public class CharMirror : MonoBehaviour {
	CharStatus status;
	InputManager input;
	Rigidbody2D rb2D;

	void Start() {
		status = GetComponent<CharStatus>();
		input = GameObject.Find("InputManager").GetComponent<InputManager>();
		rb2D = GetComponent<Rigidbody2D>();
	}

	void Update() {
		//Rotate Character model
		if(input.GetAxis("X") < 0 && (status.grounded || status.againstLeft || status.againstRight)) {
			if(!status.isMirrored) {
				transform.rotation = Quaternion.Euler(0, 180, 0);
				status.isMirrored = true;
			}
		}
		else if(input.GetAxis("X") > 0 && (status.grounded || status.againstLeft || status.againstRight)) {
			if(status.isMirrored) {
				transform.rotation = Quaternion.Euler(0, 0, 0);
				status.isMirrored = false;
			}
		}

		//Rotate when in air if veliocity > 1 in opposite direction
		else if(input.GetAxis("X") < 0 && rb2D.velocity.x < -3 && !(status.grounded || status.againstLeft || status.againstRight)) {
			if(!status.isMirrored) {
				transform.rotation = Quaternion.Euler(0, 180, 0);
				status.isMirrored = true;
			}
		}
		else if(input.GetAxis("X") > 0 && rb2D.velocity.x > 3 && !(status.grounded || status.againstLeft || status.againstRight)) {
			if(status.isMirrored) {
				transform.rotation = Quaternion.Euler(0, 0, 0);
				status.isMirrored = false;
			}
		}
	}
}
