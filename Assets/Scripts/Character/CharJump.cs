using UnityEngine;
using System.Collections;

public class CharJump : MonoBehaviour {
	CharStatus status;
	public float jumpSpeed;

	void Start () {
		status = GetComponent<CharStatus> ();
	}
		
	void FixedUpdate () {
		if (!Input.GetKey (KeyCode.Space) && status.spaceDown) {
			status.spaceDown = false;
		}
			if (Input.GetKey (KeyCode.Space) && status.onGround && !status.spaceDown) {
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
			status.spaceDown = true;
		}
	}
}
