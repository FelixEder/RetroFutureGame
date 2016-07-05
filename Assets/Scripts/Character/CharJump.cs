using UnityEngine;
using System.Collections;

public class CharJump : MonoBehaviour {
	CharStatus status;
	public float jumpSpeed;
	bool jumpDown;

	void Start () {
		status = GetComponent<CharStatus> ();
	}
		
	void FixedUpdate () {
		if (!Input.GetKey (KeyCode.Space) && status.spaceDown) {
			status.spaceDown = false;
		}
		if (Input.GetKey (KeyCode.Space) && Input.GetAxis ("Vertical") < -0 && status.onGround) {
			Physics2D.IgnoreLayerCollision (8, 9, true);
			jumpDown = true;
		}
		 else if (!Input.GetKey (KeyCode.Space) && Input.GetAxis ("Vertical") > -0.5 && jumpDown) {
			Physics2D.IgnoreLayerCollision (8, 9, false);
			jumpDown = false;
		}
		else if (Input.GetKey (KeyCode.Space) && status.onGround && !status.spaceDown) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, jumpSpeed);
			status.spaceDown = true;
		}
	}
	/*
	function Update () {
		//tracks if the button combo for falling through is pressed
		//usually in video games this is down + jump
		if(Input.GetAxis("Vertical") == -1){
			//the layer moving platforms cannot collide with
			gameObject.layer = 9;
		}
		else{
			gameObject.layer = 0; //default layer
		}
	}
	*/
}
