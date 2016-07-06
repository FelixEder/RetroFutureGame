using UnityEngine;
using System.Collections;

public class CharJump : MonoBehaviour {
	CharStatus status;
	public float jumpSpeed;
	public bool jumpDown;

	void Start () {
		status = GetComponent<CharStatus> ();
	}
		
	void FixedUpdate () {
		if (Input.GetAxis ("Jump") == 0 && status.spaceDown) {
			status.spaceDown = false;
		}
		if (Input.GetAxis ("Jump") > 0 && Input.GetAxis ("Vertical") < 0 && status.onPlatform) {
			jumpDown = true;
		}
		else if (Input.GetAxis ("Jump") > 0 && !status.spaceDown && (status.onGround || status.onPlatform)) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, jumpSpeed);
			status.spaceDown = true;
		}
	}
}
