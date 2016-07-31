using UnityEngine;
using System.Collections;

public class CharJump : MonoBehaviour {
	CharStatus status;
	Rigidbody2D rigidBody2D;
	public float jumpSpeed;
	public bool jumpDown, hasJumped, holdJump;

	void Start () {
		status = GetComponent<CharStatus> ();
		rigidBody2D = GetComponent<Rigidbody2D> ();
	}
		
	void FixedUpdate () {
		//enable jump button when not holding button and on surface
		if (!Input.GetButton ("Jump") && holdJump && status.onSurface)
			holdJump = false;
		//jump down through platform when holding down and pressing jump
		if (Input.GetButton ("Jump") && Input.GetAxis ("Vertical") < -0.3f && status.onPlatform)
			jumpDown = true;
		//jump when on surface and pressing jump
		else if (Input.GetButton ("Jump") && !holdJump && status.onSurface) {
			rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, jumpSpeed);
			hasJumped = true;
			holdJump = true;
		}
		//decrease vertical velocity if let go of jump early
		else if (!Input.GetButton ("Jump") && hasJumped && rigidBody2D.velocity.y > jumpSpeed / 1.8f)
			rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, jumpSpeed / 1.8f);
	}
}
