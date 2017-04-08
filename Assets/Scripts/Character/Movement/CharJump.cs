using UnityEngine;
using System.Collections;

public class CharJump : MonoBehaviour {
	CharStatus status;
	Rigidbody2D rigidBody2D;
	InputManager input;
	public float jumpSpeed, secondJumpSpeed;
	public bool jumpDown, holdJump, gotSecondJump, hasSecondJumped;
	bool hasJumped;

	void Start () {
		status = GetComponent<CharStatus> ();
		rigidBody2D = GetComponent<Rigidbody2D> ();
		input = GameObject.Find ("InputManager").GetComponent<InputManager> ();
	}
		
	void FixedUpdate () {
		if ((hasJumped || hasSecondJumped) && status.onSurface) {
			GetComponent<Animator> ().SetBool ("Jumping", false);
			hasJumped = false;
			hasSecondJumped = false;
		}
		//enable jump button when not holding button and on surface
		if (!input.GetKey ("jump") && holdJump && status.onSurface) {
			holdJump = false;
		}
		else if (!input.GetKey ("jump") && holdJump && gotSecondJump && !hasSecondJumped)
			holdJump = false;
		//jump down through platform when holding down and pressing jump
		if (input.GetKey ("jump") && input.GetAxis ("Y") < -0.3f && input.GetAxis ("Ybool") < 0f && !jumpDown && !holdJump && status.onPlatform) {
			holdJump = true;
			jumpDown = true;
			rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, -1f);
		}
		//jump when on surface and pressing jump
		else if (input.GetKey ("jump") && !holdJump && status.onSurface) {
			rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, jumpSpeed);
			GetComponent<Animator> ().SetBool ("Jumping", true);
			hasJumped = true;
			holdJump = true;
		}
		//jump in air when have secondjump and has not secondjumped.
		else if (input.GetKey ("jump") && gotSecondJump && !holdJump) {
			rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, secondJumpSpeed);
			hasSecondJumped = true;
			holdJump = true;
		}
		//decrease vertical velocity if let go of jump early
		else if (!input.GetKey ("jump") && hasJumped && rigidBody2D.velocity.y > jumpSpeed / 1.8f)
			rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, jumpSpeed / 1.8f);
	}
}
