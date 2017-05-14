using UnityEngine;
using System.Collections;

//This class handles the movement for the player, including walking and jumps.
public class CharMovement : MonoBehaviour {
	CharStatus status;
	Rigidbody2D rigidBody2D;
	InputManager input;
	Animator childAnim;
	public float moveSpeed, airSpeed, maxMoveSpeed, maxFallSpeed;
	float axisH;

	void Start() {
		status = GetComponent<CharStatus> ();
		rigidBody2D = GetComponent<Rigidbody2D> ();
		input = GameObject.Find ("InputManager").GetComponent<InputManager> ();
		childAnim = transform.GetChild (0).GetComponent<Animator> ();
	}
		
	void FixedUpdate() {
		axisH = input.GetAxis("X");
		if (axisH != 0) {
			//Test if trying to move towards left wall and stop movement as well as decrease negative y velocity.
			if (status.onLeftWall && axisH < 0) {
				if (rigidBody2D.velocity.y < -2)
					rigidBody2D.velocity = new Vector2 (0, -2);
				else
					rigidBody2D.velocity = new Vector2 (0, rigidBody2D.velocity.y);
			}
			//Test if trying to move towards right wall and stop movement as well as decrease negative y velocity.
			else if (status.onRightWall && axisH > 0) {
				if (rigidBody2D.velocity.y < -2)
					rigidBody2D.velocity = new Vector2 (0, -2);
				else
					rigidBody2D.velocity = new Vector2 (0, rigidBody2D.velocity.y);
			}
			//Movement for when in air.
			else if (!status.onSurface) {
				if (Mathf.Abs (rigidBody2D.velocity.x) < maxMoveSpeed)
					rigidBody2D.velocity = new Vector2 (Mathf.Lerp (rigidBody2D.velocity.x, axisH * airSpeed, Time.deltaTime * 5), rigidBody2D.velocity.y);
				else if (Mathf.Sign (axisH) != Mathf.Sign (rigidBody2D.velocity.x))
					rigidBody2D.velocity += new Vector2 (axisH * airSpeed, 0);
			}
			//Movement
			else if (Mathf.Abs (rigidBody2D.velocity.x) < maxMoveSpeed)
				rigidBody2D.velocity = new Vector2 (axisH * moveSpeed, rigidBody2D.velocity.y);
			else if (Mathf.Sign (axisH) != Mathf.Sign (rigidBody2D.velocity.x))
				rigidBody2D.velocity = new Vector2 (axisH * moveSpeed, rigidBody2D.velocity.y);
			/*
			if (rigidBody2D.velocity.y < -maxFallSpeed)
				rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, -maxFallSpeed);
			*/
			if (Mathf.Abs (rigidBody2D.velocity.x) > 3f && status.onSurface)
				childAnim.SetTrigger ("run");
			else if (Mathf.Abs (rigidBody2D.velocity.x) > 0.5f && status.onSurface)
				childAnim.SetTrigger ("walk");
		}
		if (Mathf.Abs (rigidBody2D.velocity.x) < 0.5f && status.onSurface)
			childAnim.SetTrigger ("idle");
	}
}