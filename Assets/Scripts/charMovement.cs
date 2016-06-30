using UnityEngine;
using System.Collections;

/**
 * This class handles the movement for the player, including movement and jumps.
 */
public class CharMovement : MonoBehaviour {
	//Fields
	public float moveSpeed;
	public float jumpSpeed;

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
		if (Input.GetKeyDown(KeyCode.Space)) {
			Jump ();
		}
		GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
	}

	/**
	 * When called, makes the player character jump a set height.
	 */ 
	void Jump() {
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
	}
}