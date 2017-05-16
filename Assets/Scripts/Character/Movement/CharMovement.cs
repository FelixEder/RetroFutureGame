using UnityEngine;
using System.Collections;

//This class handles the movement for the player, including walking and jumps.
public class CharMovement : MonoBehaviour {
	CharStatus status;
	Rigidbody2D rb2D;
	InputManager input;
	Animator childAnim;
	public float moveSpeed, airSpeed, maxMoveSpeed, maxFallSpeed;
	float axisH;

	void Start() {
		status = GetComponent<CharStatus> ();
		rb2D = GetComponent<Rigidbody2D> ();
		input = GameObject.Find ("InputManager").GetComponent<InputManager> ();
		childAnim = transform.GetChild (0).GetComponent<Animator> ();
	}
		
	void FixedUpdate() {
		axisH = input.GetAxis("X");
		if (axisH != 0) {
			//Test if trying to move towards left wall and stop movement as well as decrease negative y velocity.
			if (status.againstLeft && axisH < 0) {
				if (rb2D.velocity.y < -2)
					rb2D.velocity = new Vector2 (0, -2);
				else
					rb2D.velocity = new Vector2 (0, rb2D.velocity.y);
			}
			//Test if trying to move towards right wall and stop movement as well as decrease negative y velocity.
			else if (status.againstRight && axisH > 0) {
				if (rb2D.velocity.y < -2)
					rb2D.velocity = new Vector2 (0, -2);
				else
					rb2D.velocity = new Vector2 (0, rb2D.velocity.y);
			}
			//Movement for when in air.
			else if (!status.grounded) {
				if (Mathf.Sign (axisH) != Mathf.Sign (rb2D.velocity.x))
					rb2D.velocity += new Vector2 (axisH * airSpeed / 2, 0);
				else if (Mathf.Abs (rb2D.velocity.x) < maxMoveSpeed)
					rb2D.velocity = new Vector2 (Mathf.Lerp (rb2D.velocity.x, axisH * airSpeed, Time.deltaTime * 5), rb2D.velocity.y);
			}
			//Movement
			else if (Mathf.Abs (rb2D.velocity.x) < maxMoveSpeed)
				rb2D.velocity = new Vector2 (axisH * moveSpeed, rb2D.velocity.y);
			else if (Mathf.Sign (axisH) != Mathf.Sign (rb2D.velocity.x))
				rb2D.velocity = new Vector2 (axisH * moveSpeed, rb2D.velocity.y);
			/*
			if (rigidBody2D.velocity.y < -maxFallSpeed)
				rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, -maxFallSpeed);
			*/
		}
	}

	void Update () {
		bool movingAxisH = Mathf.Abs (input.GetAxis ("X")) > 0 ? true : false;
		childAnim.SetBool ("axisX", movingAxisH);
		childAnim.SetFloat ("axisXvalue", input.GetAxis ("X"));
		childAnim.SetFloat ("velocityX", Mathf.Abs (rb2D.velocity.x));
		childAnim.SetBool ("grounded", status.grounded);
		childAnim.SetBool ("mirrored", status.isMirrored);
		/*
		if (axisH != 0) {
			if (Mathf.Abs (rb2D.velocity.x) > 3f && status.grounded)
				childAnim.SetTrigger ("run");
			else if (Mathf.Abs (rb2D.velocity.x) > 0.2f && status.grounded)
				childAnim.SetTrigger ("walk");
		}
		if (Mathf.Abs (rb2D.velocity.x) < 0.2f && status.grounded)
			childAnim.SetTrigger ("idle");
		*/
	}
}