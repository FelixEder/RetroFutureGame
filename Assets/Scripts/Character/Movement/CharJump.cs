using UnityEngine;
using System.Collections;

public class CharJump : MonoBehaviour {
	CharStatus status;
	Rigidbody2D rb2D;
	InputManager input;
	Animator childAnim;
	public float jumpSpeed, secondJumpSpeed;
	public bool jumpDown, holdJump, gotSecondJump, hasSecondJumped;
	[SerializeField] bool hasJumped, jumpingBackward;

	void Start () {
		status = GetComponent<CharStatus> ();
		rb2D = GetComponent<Rigidbody2D> ();
		input = GameObject.Find ("InputManager").GetComponent<InputManager> ();
		childAnim = transform.GetChild (0).GetComponent<Animator> ();
	}
		
	void FixedUpdate () {
		if ((hasJumped || hasSecondJumped) && status.onSurface) {
			hasJumped = false;
			hasSecondJumped = false;
		}
		//enable jump button when not holding button and on surface
		if (!input.GetKey ("jump") && holdJump && status.onSurface) {
			holdJump = false;
		}
		else if (!input.GetKey ("jump") && holdJump && gotSecondJump && !hasSecondJumped)
			holdJump = false;
		//jump down through platform when holding down and pressing jump
		if (input.GetKey ("jump") && input.GetAxis ("Y") < -0.3f && input.GetAxis ("Ybool") < 0f && !jumpDown && !holdJump && status.onPlatform) {
			holdJump = true;
			jumpDown = true;
			rb2D.velocity = new Vector2 (rb2D.velocity.x, -1f);
		}
		//jump when on surface and pressing jump
		else if (input.GetKey ("jump") && !holdJump && status.onSurface) {
			rb2D.velocity = new Vector2 (rb2D.velocity.x, jumpSpeed);
			hasJumped = true;
			holdJump = true;
		}
		//jump in air when have secondjump and has not secondjumped.
		else if (input.GetKey ("jump") && gotSecondJump && !holdJump) {
			rb2D.velocity = new Vector2 (rb2D.velocity.x, secondJumpSpeed);
			hasSecondJumped = true;
			holdJump = true;
		}
		//decrease vertical velocity if let go of jump early
		else if (!input.GetKey ("jump") && hasJumped && rb2D.velocity.y > jumpSpeed / 1.8f)
			rb2D.velocity = new Vector2 (rb2D.velocity.x, jumpSpeed / 1.8f);

		//Animations
		if ((hasJumped || hasSecondJumped || status.InAir()) && !status.onSurface) {
			if (Mathf.Sign (input.GetAxis ("X")) == -Mathf.Sign (rb2D.velocity.x) && Mathf.Abs (rb2D.velocity.x) > 0.1f) {
				jumpingBackward = true;
				childAnim.SetTrigger ("jump_backward");
			}
			else if (jumpingBackward) {
				if (Mathf.Abs (rb2D.velocity.x) > 3) {
					childAnim.SetTrigger ("jump_forward");
					jumpingBackward = false;
				}
			}
			else if (rb2D.velocity.y > 0.1f)
				childAnim.SetTrigger ("jump_forward");
		}
		if (jumpingBackward && status.onSurface)
			jumpingBackward = false;
	}
}
