using UnityEngine;
using System.Collections;

public class WallJump : MonoBehaviour {
	PlayerStatus status;
	PlayerJump jump;
	Rigidbody2D rigidBody2D;
	InputManager input;
	public float WallJumpSpeed;
	bool holdJump;

	void Start() {
		status = GetComponent<PlayerStatus>();
		jump = GetComponent<PlayerJump>();
		rigidBody2D = GetComponent<Rigidbody2D>();
		input = GameObject.Find("InputManager").GetComponent<InputManager>();
	}

	void FixedUpdate() {
		if(!input.GetKey("jump")) {
			holdJump = false;
		}
		if(input.GetKey("jump") && (status.againstLeft || status.againstRight) && !holdJump && !status.grounded && !status.isSmall) {
			holdJump = true;
			float axisH = input.GetAxis("X");
			if(status.againstLeft && axisH > 0) {
				rigidBody2D.velocity = new Vector2(WallJumpSpeed, jump.jumpSpeed / 1.2f);
				jump.hasSecondJumped = false;
			}
			else if(status.againstRight && axisH < 0) {
				rigidBody2D.velocity = new Vector2(-WallJumpSpeed, jump.jumpSpeed / 1.2f);
				jump.hasSecondJumped = false;
			}
		}
	}

}