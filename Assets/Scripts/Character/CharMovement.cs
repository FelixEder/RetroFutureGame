using UnityEngine;
using System.Collections;
using System;

//This class handles the movement for the player, including walking and jumps.
public class CharMovement : MonoBehaviour {
	//Fields
	CharStatus status;
	Rigidbody2D rigidBody2D;
	public float moveSpeed, airMoveSpeed, maxMoveSpeed;
	public float moveAcceleration;

	//Start is called once on initialization
	void Start() {
		status = GetComponent<CharStatus> ();
		rigidBody2D = GetComponent<Rigidbody2D> ();
	}

	//Update is called once per frame
	void Update(){
	}

	//FixedUpdate executes with a set time interval and calculates all physics equations required.
	void FixedUpdate() {
		if (status.onLeftWall && Input.GetAxis("Horizontal") < 0) {
			rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
		} else if (status.onRightWall && Input.GetAxis ("Horizontal") > 0) {
			rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
		} else {
			moveAcceleration = rigidBody2D.velocity.x * moveSpeed + Input.GetAxis("Horizontal");
			if (moveAcceleration > maxMoveSpeed) {
				moveAcceleration = maxMoveSpeed;
			} else if(moveAcceleration < maxMoveSpeed * -1) {
				moveAcceleration = maxMoveSpeed * -1;
			}
			Vector2 vector2 = rigidBody2D.velocity;
			vector2.x = moveAcceleration * Math.Abs(Input.GetAxis("Horizontal"));
			rigidBody2D.velocity = vector2; //Set horizontal movement velocity relative to the current velocity.
		}
		if (rigidBody2D.velocity.y < -1 && status.onRightWall && Input.GetAxis ("Horizontal") > 0) {
			rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, -1);
		}
		if (rigidBody2D.velocity.y < -1 && status.onLeftWall && Input.GetAxis ("Horizontal") < 0) {
			rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, -1);
		}
	}
}