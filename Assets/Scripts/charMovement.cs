using UnityEngine;
using System.Collections;

/**
 * This class handles the movement for the player, including movement and jumps.
 */
public class CharMovement : MonoBehaviour {
	//Fields
	public float moveSpeed;
	public float jumpSpeed;
	bool isGrounded = false, onLeftWall = false, onRightWall = false;

	// Use this for initialization
	void Start() {
	}

	// Update is called once per frame
	void Update(){
	}

	/**
	 * Fixed update executes with a set time interval and calculates all physics equations required.
	 */
	void FixedUpdate() {
		if (onLeftWall && Input.GetAxis ("Horizontal") < 0) {
			GetComponent<Rigidbody2D> ().velocity.x = 0;
		} else if (onRightWall && Input.GetAxis ("Horizontal") > 0) {
			GetComponent<Rigidbody2D> ().velocity.x = 0;
		} else {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (Input.GetAxis ("Horizontal") * moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
			if (Input.GetKeyDown (KeyCode.Space) && isGrounded)
				Jump ();
		}
	}

	/**
	 * When called, makes the player character jump a set height.
	 */ 
	void Jump() {
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
	}

	/**
	 * Method is called when player object collides with the ground. 
	 */
	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "ground") {
			isGrounded = true;
		}
		if (col.gameObject.tag == "leftWall") {
			onLeftWall = true;
			GetComponent<Rigidbody2d> ().velocity.y - 10;
		}
		if (col.gameObject.tag == "rightWall") {
			onRightWall = true;
			GetComponent<Rigidbody2d> ().velocity.y - 10;
		}
	}

	/**
	 * Method is called when player object leaves the ground.
	 */
	void OnCollisionExit2D(Collision2D col) {
		if(col.gameObject.tag == "ground") {
			isGrounded = false;
		}
		if (col.gameObject.tag == "wall") {
			onWall = false;
		}
	}
}