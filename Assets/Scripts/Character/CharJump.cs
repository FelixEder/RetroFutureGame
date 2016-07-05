using UnityEngine;
using System.Collections;

public class CharJump : MonoBehaviour {
	CharStatus status;
	PlatformLogic logic;
	public float jumpSpeed;
	bool jumpDown;

	void Start () {
		status = GetComponent<CharStatus> ();
		logic = GetComponent<PlatformLogic> ();
	}
		
	void FixedUpdate () {
		if (!Input.GetKey (KeyCode.Space) && status.spaceDown) {
			status.spaceDown = false;
		}
		if (Input.GetKey (KeyCode.Space) && Input.GetAxis ("Vertical") < -0.5 && status.onGround) {
			Physics2D.IgnoreLayerCollision (8, 9, true);
			jumpDown = true;
		}
		 else if (!Input.GetKey (KeyCode.Space) && Input.GetAxis ("Vertical") > -0.5 && jumpDown) {
			Physics2D.IgnoreLayerCollision (8, 9, false);
			jumpDown = false;
		}
		else if (Input.GetKey (KeyCode.Space) && status.onGround && !status.spaceDown) {
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpSpeed), ForceMode2D.Impulse);
			status.spaceDown = true;
		}
	}
}
