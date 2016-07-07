using UnityEngine;
using System.Collections;

//This class handles the movement for the player, including walking and jumps.
public class CharMovement : MonoBehaviour {
	//Fields
	CharStatus status;
	Rigidbody2D rigidBody2D;
	public float moveSpeed, airSpeed, maxMoveSpeed, moveForce, maxFallSpeed;
	float axisH;

	//Start is called once on initialization
	void Start() {
		status = GetComponent<CharStatus> ();
		rigidBody2D = GetComponent<Rigidbody2D> ();
	}

	//FixedUpdate executes with a set time interval and calculates all physics equations required.
	void FixedUpdate() {
		axisH = Input.GetAxis("Horizontal");
		//Test if trying to move towards wall and stop movement as well as decrease negative y velocity.
		if (status.onLeftWall && axisH < 0) {
			if (rigidBody2D.velocity.y < -2) {
				rigidBody2D.velocity = new Vector2 (0, -2);
			}
			else {
				rigidBody2D.velocity = new Vector2 (0, rigidBody2D.velocity.y);
			}
		}
		else if (status.onRightWall && axisH > 0) {
			if (rigidBody2D.velocity.y < -2) {
				rigidBody2D.velocity = new Vector2 (0, -2);
			}
			else {
				rigidBody2D.velocity = new Vector2 (0, rigidBody2D.velocity.y);
			}
		}
		//Movement for when in air.
		else if (!status.onGround) {
			if (Mathf.Abs (rigidBody2D.velocity.x) < maxMoveSpeed) {
				rigidBody2D.velocity = new Vector2 (axisH * airSpeed, rigidBody2D.velocity.y);
			}
			else if (Mathf.Sign (axisH) != Mathf.Sign (rigidBody2D.velocity.x)) {
				rigidBody2D.velocity = new Vector2 (axisH * airSpeed, rigidBody2D.velocity.y);
			}
		}
		//Movement
		else if (Mathf.Abs (rigidBody2D.velocity.x) < maxMoveSpeed) {
			rigidBody2D.velocity = new Vector2 (axisH * moveSpeed, rigidBody2D.velocity.y);
		}
		else if (Mathf.Sign (axisH) != Mathf.Sign (rigidBody2D.velocity.x)) {
			rigidBody2D.velocity = new Vector2 (axisH * moveSpeed, rigidBody2D.velocity.y);
		}
		if (rigidBody2D.velocity.y < maxFallSpeed * -1) {
			rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, maxFallSpeed * -1);
		}
	}
}