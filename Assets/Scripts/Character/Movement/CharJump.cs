using UnityEngine;
using System.Collections;

public class CharJump : MonoBehaviour {
	CharStatus status;
	Rigidbody2D rb2D;
	InputManager input;
	public float jumpSpeed, secondJumpSpeed;
	public bool jumpDown, holdJump, gotSecondJump, hasSecondJumped;
	[SerializeField] bool hasJumped, jumpingBackward;

	void Start () {
		status = GetComponent<CharStatus> ();
		rb2D = GetComponent<Rigidbody2D> ();
		input = GameObject.Find ("InputManager").GetComponent<InputManager> ();
	}
		
	void Update () {
		if (!input.GetKey("jump") && (hasJumped || hasSecondJumped) && status.grounded) {
			hasJumped = false;
			hasSecondJumped = false;
		}
		//enable jump button when not holding button and on surface
		if (!input.GetKey ("jump") && holdJump && status.grounded) {
			holdJump = false;
		}
		else if (!input.GetKey ("jump") && holdJump && gotSecondJump && !hasSecondJumped)
			holdJump = false;
		
		//jump down through platform when holding down and pressing jump
		if (input.GetAxis ("Y") < -0.3f && input.GetAxis ("Ybool") < 0f) {
			if (input.GetKey ("jump") && !holdJump && status.onPlatform) {
				Debug.Log ("JUMPDOWN");
				jumpDown = true;
				holdJump = true;
				rb2D.velocity = new Vector2 (rb2D.velocity.x, -1f);
				transform.GetChild (1).gameObject.SetActive (false);
			}
			if (jumpDown)
				rb2D.velocity = status.onPlatform ? new Vector2 (rb2D.velocity.x, -1f) : rb2D.velocity;
		}
		else if (jumpDown) {
			transform.GetChild (1).gameObject.SetActive (true);
			jumpDown = false;
		}
		//jump when on surface and pressing jump
		else if (input.GetKey ("jump") && !holdJump && status.grounded) {
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
		else if (!input.GetKey ("jump") && hasJumped && rb2D.velocity.y > jumpSpeed / 2f)
			rb2D.velocity = new Vector2 (rb2D.velocity.x, rb2D.velocity.y / 1.5f);
	}
}
