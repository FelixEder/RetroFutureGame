using UnityEngine;
using System.Collections;

//This class handles the movement for the player, including walking and jumps.
public class CharMovement : MonoBehaviour {
	//Fields
	public float moveSpeed, airMoveSpeed, maxMoveSpeed, jumpSpeed;
	float moveAcceleration;
	bool isJumping = false, mirrored = false;

	//Start is called once on initialization
	void Start() {
	}

	//Update is called once per frame
	void Update(){
	}

	//FixedUpdate executes with a set time interval and calculates all physics equations required.
	void FixedUpdate() {
		//Walk
		if(isJumping) {
			moveAcceleration = GetComponent<Rigidbody2D>().velocity.x * airMoveSpeed + Input.GetAxis("Horizontal");
		}
		else {
			moveAcceleration = GetComponent<Rigidbody2D>().velocity.x * moveSpeed + Input.GetAxis("Horizontal");
			//Rotate charcter model when moving and isJumping is not true.
			if(Input.GetAxis("Horizontal") < 0){
				if(!mirrored){
					transform.rotation = Quaternion.Euler(0, 180, 0);
				}
			}
			if(Input.GetAxis("Horizontal") > 0){
				if(mirrored){
					transform.rotation = Quaternion.Euler(0, 0, 0);
				}
			}
		}
		if(moveAcceleration > maxMoveSpeed) {
			moveAcceleration = maxMoveSpeed;
		}
		else if(moveAcceleration < maxMoveSpeed * -1) {
			moveAcceleration = maxMoveSpeed * -1;
		}
		GetComponent<Rigidbody2D>().velocity = new Vector2(moveAcceleration * Math.Abs(Input.GetAxis("Horizontal")), GetComponent<Rigidbody2D>().velocity.y); //Set horizontal movement velocity relative to the current velocity.
		//Jump
		if(Input.GetKeyDown(KeyCode.Space) && !isJumping) {
			Jump();
			isJumping = true;
		}
	}
	
	//When called, makes the player character jump with a set force. 
	void Jump() {
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "floor") {
			isJumping = false;
		}
	}
}