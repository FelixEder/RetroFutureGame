using UnityEngine;
using System.Collections;

public class WallJump : MonoBehaviour {
	CharStatus status;
	Rigidbody2D rigidBody2D;

	void Start () {
		status = GetComponent<CharStatus> ();
		rigidBody2D = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate() {
		if(Input.GetButton("Jump") && (status.onLeftWall || status.onRightWall)) {
			axisH = Input.GetAxis("Horizontal");
			if(axisH > 0 && status.onLeftWall) {
				//Do up-right jump using RidigBody2D
			}
			else if(axisH < 0 && status.onRightWall) {
				//Do up-left jump using RidigBody2D
			}
		}
	}
}