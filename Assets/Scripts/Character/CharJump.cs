using UnityEngine;
using System.Collections;

public class CharJump : MonoBehaviour {
	CharStatus status;
	PlatformLogic logic;
	public float jumpSpeed;

	void Start () {
		status = GetComponent<CharStatus> ();
		logic = GetComponent<PlatformLogic> ();
	}
		
	void FixedUpdate () {
		if (!Input.GetKey (KeyCode.Space) && status.spaceDown) {
			status.spaceDown = false;
		}
		if (Input.GetKey (KeyCode.Space) && Input.GetAxis ("Vertical") < -0.5 && status.onGround) {
			logic.IgnorePlatformCollision (true);
			logic.jumpDown = true;
		}
		 else if (!Input.GetKey (KeyCode.Space) && Input.GetAxis ("Vertical") > -0.5 && logic.jumpDown) {
			logic.IgnorePlatformCollision (false);
			logic.jumpDown = false;
		}
		else if (Input.GetKey (KeyCode.Space) && status.onGround && !status.spaceDown) {
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpSpeed), ForceMode2D.Impulse);
			status.spaceDown = true;
		}
	}
}
