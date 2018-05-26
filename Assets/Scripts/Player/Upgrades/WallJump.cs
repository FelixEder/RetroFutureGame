using UnityEngine;
using System.Collections;

public class WallJump : MonoBehaviour {
	PlayerStatus status;
	PlayerJump jump;
	Rigidbody2D rigidBody2D;
	InputManager input;
	public float WallJumpSpeed;
	public bool holdJump, leftWall, rightWall;

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

		leftWall = Physics2D.OverlapBox(new Vector2(transform.position.x - 0.1f, transform.position.y), new Vector2(0.7f, 1f), 0, status.whatIsWall);
		rightWall = Physics2D.OverlapBox(new Vector2(transform.position.x + 0.1f, transform.position.y), new Vector2(0.7f, 1f), 0, status.whatIsWall);
		
		if(input.GetKey("jump") && !holdJump && !status.grounded && !status.isSmall) {
			holdJump = true;
			float axisH = input.GetAxis("X");
			if(leftWall && axisH > 0) {
				rigidBody2D.velocity = new Vector2(WallJumpSpeed, jump.jumpSpeed / 1.2f);
			}
			else if(rightWall && axisH < 0) {
				rigidBody2D.velocity = new Vector2(-WallJumpSpeed, jump.jumpSpeed / 1.2f);
			}
		}
	}

}