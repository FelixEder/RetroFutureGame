using UnityEngine;
using System.Collections;

//This class handles the movement for the player, including walking and jumps.
public class PlayerMovement : MonoBehaviour {
	PlayerStatus status;
	Rigidbody2D rb2D;
	InputManager input;
	Animator anim;
	public float moveSpeed, airSpeed, maxMoveSpeed;
	float axisH, steppingSpeed;
	bool stunned;

	public bool smoothIncline;

	void Start() {
		status = GetComponent<PlayerStatus>();
		rb2D = GetComponent<Rigidbody2D>();
		input = GameObject.Find("InputManager").GetComponent<InputManager>();
		anim = GetComponent<Animator>();
	}

	void FixedUpdate() {
		axisH = input.GetAxis("X");
		steppingSpeed = moveSpeed - Mathf.Abs(rb2D.velocity.x);

		if(axisH != 0 && !stunned) {
			//Test if trying to move towards left wall and stop movement as well as decrease negative y velocity.
			if(status.againstLeft && axisH < 0) {
				if(rb2D.velocity.y < -2)
					rb2D.velocity = new Vector2(0, -2);
				else
					rb2D.velocity = new Vector2(0, rb2D.velocity.y);
			}
			//Test if trying to move towards right wall and stop movement as well as decrease negative y velocity.
			else if(status.againstRight && axisH > 0) {
				if(rb2D.velocity.y < -2)
					rb2D.velocity = new Vector2(0, -2);
				else
					rb2D.velocity = new Vector2(0, rb2D.velocity.y);
			}
			//Movement for when in air.
			else if(!status.grounded) {
				if(Mathf.Sign(axisH) != Mathf.Sign(rb2D.velocity.x))
					rb2D.velocity += new Vector2(axisH * airSpeed / 2, 0);
				else if(Mathf.Abs(rb2D.velocity.x) < maxMoveSpeed)
					rb2D.velocity = new Vector2(Mathf.Lerp(rb2D.velocity.x, axisH * airSpeed, Time.fixedDeltaTime * 5), rb2D.velocity.y);
			}
			//Movement
			else if(Mathf.Abs(rb2D.velocity.x) < maxMoveSpeed)
				rb2D.velocity = new Vector2(axisH * moveSpeed, rb2D.velocity.y);
			else if(Mathf.Sign(axisH) != Mathf.Sign(rb2D.velocity.x))
				rb2D.velocity = new Vector2(axisH * moveSpeed, rb2D.velocity.y);
			//Movement up steps and inclines
			if(!status.againstFront && status.againstStep && rb2D.velocity.y < steppingSpeed)
				rb2D.velocity = new Vector2(rb2D.velocity.x, steppingSpeed);
		}
		
		//Limit falling speed
		if(rb2D.velocity.y < -15)
            rb2D.velocity = new Vector2(rb2D.velocity.x, -15);
	}

	void Update() {
		bool movingAxisH = Mathf.Abs(input.GetAxis("X")) > 0 ? true : false;
		anim.SetBool("axisX", movingAxisH);
		anim.SetFloat("axisXvalue", input.GetAxis("X"));
		anim.SetFloat("velocityX", Mathf.Abs(rb2D.velocity.x));
		anim.SetBool("grounded", status.grounded);
		anim.SetBool("mirrored", status.isMirrored);
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
	
	public void Stun(float duration) {
		if(stunned)
			return;
		stunned = true;
		Invoke("RemoveStun", duration);
	}
	
	void RemoveStun() {
		stunned = false;
	}
}