using UnityEngine;
using System.Collections;

public class CharJump : MonoBehaviour {
	CharStatus status;
	Rigidbody2D rigidBody2D;
	public float jumpSpeed, secondJumpSpeed;
	public bool jumpDown, holdJump, gotSecondJump, hasSecondJumped;
	bool hasJumped;

	void Start () {
		status = GetComponent<CharStatus> ();
		rigidBody2D = GetComponent<Rigidbody2D> ();
	}
		
	void FixedUpdate () {
		if ((hasJumped || hasSecondJumped) && status.onSurface) {
			transform.GetChild (0).gameObject.GetComponent<Animator> ().SetBool ("Jumping", false);
			hasJumped = false;
			hasSecondJumped = false;
		}
		//enable jump button when not holding button and on surface
		if (!Input.GetButton ("Jump") && holdJump && status.onSurface) {
			holdJump = false;
		}
		else if (!Input.GetButton ("Jump") && holdJump && gotSecondJump && !hasSecondJumped)
			holdJump = false;
		//jump down through platform when holding down and pressing jump
		if (Input.GetButton ("Jump") && Input.GetAxis ("Vertical") < -0.3f && status.onPlatform)
			jumpDown = true;
		//jump when on surface and pressing jump
		else if (Input.GetButton ("Jump") && !holdJump && status.onSurface) {
			rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, jumpSpeed);
			transform.GetChild (0).gameObject.GetComponent<Animator> ().SetBool ("Jumping", true);
			hasJumped = true;
			holdJump = true;
		}
		//jump in air when have secondjump and has not secondjumped.
		else if (Input.GetButton ("Jump") && gotSecondJump && !holdJump) {
			rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, secondJumpSpeed);
			hasSecondJumped = true;
			holdJump = true;
		}
		//decrease vertical velocity if let go of jump early
		else if (!Input.GetButton ("Jump") && hasJumped && rigidBody2D.velocity.y > jumpSpeed / 1.8f)
			rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, jumpSpeed / 1.8f);
	}
}
