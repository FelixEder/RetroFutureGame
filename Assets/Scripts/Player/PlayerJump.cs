using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour {
	PlayerStatus status;
	Rigidbody2D rb2D;
	InputManager input;
	public LayerMask whatIsCeiling;
	public Transform ceilingCheck;
	public float jumpSpeed, secondJumpSpeed;
	public bool secondJumped;
	bool jumped, secondJumpAcquired;
	
	public AreaTitle title;

	Vector2 moveInput;
	int jumpInput;

	void Start() {
		status = GetComponent<PlayerStatus>();
		rb2D = GetComponent<Rigidbody2D>();
		input = GameObject.Find("InputManager").GetComponent<InputManager>();
	}
	
	void Update() {
		if(GetComponent<PlayerInventory>().HasAcquired("secondJump") && !secondJumpAcquired)
			secondJumpAcquired = true;
	}

	void FixedUpdate() {
		if(status.grounded && jumped) {
			jumped = false;
			secondJumped = false;
		}
		
		//jump down through platform when holding down and pressing jump
		if(moveInput.y < -0.3f) {
			if(jumpInput > 0 && status.onPlatform) {
				jumpInput = -1;
				rb2D.velocity = new Vector2(rb2D.velocity.x, -1f);
				transform.GetChild(1).gameObject.SetActive(false);
			}
			if(false)
				rb2D.velocity = status.onPlatform ? new Vector2(rb2D.velocity.x, -1f) : rb2D.velocity;
		}
		else if(jumpInput == 0) {
			transform.GetChild(1).gameObject.SetActive(true);
		}

		//swim in water
		else if(jumpInput > 0 && status.inWater) {
			jumpInput = -1;
			rb2D.velocity = rb2D.velocity.y < jumpSpeed / 2 ? new Vector2(rb2D.velocity.x, rb2D.velocity.y + (jumpSpeed / 20)) : rb2D.velocity;
		}

		//jump when on surface and pressing jump
		else if(jumpInput > 0 && status.grounded && !jumped) {
			jumpInput = -1;
			jumped = true;
			rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
		}
		
		//jump in air when have secondjump and has not secondjumped.
		else if(jumpInput > 0 && secondJumpAcquired && !secondJumped && !status.isSmall) {
			jumpInput = -1;
			secondJumped = true;
			rb2D.velocity = new Vector2(rb2D.velocity.x, secondJumpSpeed);
		}

		//decrease vertical velocity if let go of jump early
		else if(jumpInput == 0 && jumped && rb2D.velocity.y > jumpSpeed / 2f)
			rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y / 1.5f);
			//Should probably change to adding a downwards force rather than setting it to a fraction. (Maybe doesn't make a difference.
			
		if(Physics2D.OverlapBox(ceilingCheck.position, new Vector2(0.5f, 0.3f), 0, whatIsCeiling) && rb2D.velocity.y > 0 && !status.isSmall) {
			rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
		}
	}

	public void JumpInput(float value) {
		//jumpInput = value == 0 ? false : true;
		jumpInput = (int)value;
	}
}
