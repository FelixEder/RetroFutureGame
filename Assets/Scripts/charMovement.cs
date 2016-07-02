using UnityEngine;
using System.Collections;
using System;

//This class handles the movement for the player, including walking and jumps.
public class charMovement : MonoBehaviour {
	//Fields
	public float moveSpeed, airMoveSpeed, maxMoveSpeed, jumpSpeed;
	float moveAcceleration;
	bool isGrounded = false, onLeftWall = false, onRightWall = false, mirrored = false;

	//Start is called once on initialization
	void Start() {
	}

	//Update is called once per frame
	void Update(){
	}

	//FixedUpdate executes with a set time interval and calculates all physics equations required.
	void FixedUpdate() {
		if (onLeftWall && Input.GetAxis ("Horizontal") < 0) {
			GetComponent<Rigidbody2D> ().velocity.x = 0;
		} else if (onRightWall && Input.GetAxis ("Horizontal") > 0) {
			GetComponent<Rigidbody2D> ().velocity.x = 0;
		} else {
			moveAcceleration = GetComponent<Rigidbody2D>().velocity.x * moveSpeed + Input.GetAxis("Horizontal");
			//Rotate charcter model when moving and isJumping is not true.
			if (!isGrounded) {
				if (Input.GetAxis ("Horizontal") < 0) {
					if (!mirrored) {
						transform.rotation = Quaternion.Euler(0, 180, 0);
						mirrored = true;
					}
				} else if (Input.GetAxis("Horizontal") > 0) {
					if (mirrored) {
						transform.rotation = Quaternion.Euler(0, 0, 0);
						mirrored = false;
					}
				}
			}
			if(moveAcceleration > maxMoveSpeed) {
				moveAcceleration = maxMoveSpeed;
			} else if(moveAcceleration < maxMoveSpeed * -1) {
				moveAcceleration = maxMoveSpeed * -1;
			}
			GetComponent<Rigidbody2D>().velocity = new Vector2(moveAcceleration * Math.Abs(Input.GetAxis("Horizontal")), GetComponent<Rigidbody2D>().velocity.y); //Set horizontal movement velocity relative to the current velocity.
			//Jump
			if (Input.GetKeyDown (KeyCode.Space) && isGrounded) {
				Jump ();
			}
		}
	}
	
	//When called, makes the player character jump with a set force. 
	void Jump() {
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
	}
	
	//Method is called when player object collides with the ground. 
	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "ground") {
			isGrounded = true;
		}
		if (col.gameObject.tag == "leftWall") {
			onLeftWall = true;
			GetComponent<Rigidbody2d> ().velocity.y.decrement(10);
		}
		if (col.gameObject.tag == "rightWall") {
			onRightWall = true;
			GetComponent<Rigidbody2d> ().velocity.y.decrement(10);
		}
	}


	//Method is called when player object leaves the ground.
	void OnCollisionExit2D(Collision2D col) {
		if(col.gameObject.tag == "ground") {
			isGrounded = false;
		}
		if (col.gameObject.tag == "wall") {
			onWall = false;
		}
	}
}