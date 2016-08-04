using UnityEngine;
using System.Collections;

public class WallJump : MonoBehaviour {
	CharStatus status;
	CharJump jump;
	CharMovement movement;
	Rigidbody2D rigidBody2D;
	InputManager input;
	public float WallJumpSpeed;
	bool holdJump;

	void Start () {
		status = GetComponent<CharStatus> ();
		jump = GetComponent<CharJump> ();
		movement = GetComponent<CharMovement> ();
		rigidBody2D = GetComponent<Rigidbody2D> ();
		input = GameObject.Find ("InputManager").GetComponent<InputManager> ();
	}

	void FixedUpdate() {
		if (!input.GetKey ("jump")) {
			holdJump = false;
		}
		if(input.GetKey ("jump") && (status.onLeftWall || status.onRightWall) && !holdJump && !status.onSurface) {
			holdJump = true;
			float axisH = input.GetAxis ("X");
			if(status.onLeftWall && axisH > 0) {
				rigidBody2D.velocity = new Vector2 (WallJumpSpeed, jump.jumpSpeed / 1.2f);
				jump.hasSecondJumped = false;
			}
			else if(status.onRightWall && axisH < 0) {
				rigidBody2D.velocity = new Vector2 (-WallJumpSpeed, jump.jumpSpeed / 1.2f);
				jump.hasSecondJumped = false;
			}
		}
	}

}