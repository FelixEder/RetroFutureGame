using UnityEngine;
using System.Collections;

public class WallJump : MonoBehaviour {
	CharStatus status;
	CharJump CharJump;
	Rigidbody2D rigidBody2D;
	public float WallJumpSpeed;
	bool holdJump;

	void Start () {
		status = GetComponent<CharStatus> ();
		CharJump = GetComponent<CharJump> ();
		rigidBody2D = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate() {
		if (!Input.GetButton ("Jump")) {
			holdJump = false;
		}
		if(Input.GetButton("Jump") && (status.onLeftWall || status.onRightWall) && !holdJump && !status.onGround) {
			holdJump = true;
			float axisH = Input.GetAxis("Horizontal");
			if(axisH > 0 && status.onLeftWall) {
				rigidBody2D.velocity = new Vector2 (WallJumpSpeed, CharJump.jumpSpeed / 1.2f);
			}
			else if(axisH < 0 && status.onRightWall) {
				rigidBody2D.velocity = new Vector2 (-1 * WallJumpSpeed, CharJump.jumpSpeed / 1.2f);
			}
		}
	}
}