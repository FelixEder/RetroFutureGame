using UnityEngine;
using System.Collections;

public class WallJump : MonoBehaviour {
	CharStatus status;
	CharJump charJump;
	public float WallJumpSpeed;
	Rigidbody2D rigidBody2D;

	void Start () {
		status = GetComponent<CharStatus> ();
		charJump = GetComponent<charJump> ();
		rigidBody2D = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate() {
		if(Input.GetButton("Jump") && (status.onLeftWall || status.onRightWall)) {
			axisH = Input.GetAxis("Horizontal");
			if(axisH > 0 && status.onLeftWall) {
				rigidBody2D.velocity = new Vector2 (WallJumpSpeed, charJump.jumpSpeed);
			}
			else if(axisH < 0 && status.onRightWall) {
				rigidBody2D.velocity = new Vector2 (-1 *WallJumpSpeed, charJump.jumpSpeed);
			}
		}
	}
}