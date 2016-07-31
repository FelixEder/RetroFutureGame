using UnityEngine;
using System.Collections;

public class WallJump : MonoBehaviour {
	CharStatus status;
	CharJump charJump;
	Rigidbody2D rigidBody2D;
	public float WallJumpSpeed;
	bool holdJump;

	void Start () {
		status = GetComponent<CharStatus> ();
		charJump = GetComponent<CharJump> ();
		rigidBody2D = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate() {
		if (!Input.GetButton ("Jump")) {
			holdJump = false;
		}
		if(Input.GetButton("Jump") && (status.onLeftWall || status.onRightWall) && !holdJump && !status.onGround) {
			holdJump = true;
			charJump.hasSecondJumped = false;
			float axisH = Input.GetAxis("Horizontal");
			if(axisH > 0 && status.onLeftWall) {
				rigidBody2D.velocity = new Vector2 (WallJumpSpeed, charJump.jumpSpeed / 1.2f);
			}
			else if(axisH < 0 && status.onRightWall) {
				rigidBody2D.velocity = new Vector2 (-1 * WallJumpSpeed, charJump.jumpSpeed / 1.2f);
			}
		}
	}
}