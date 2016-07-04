using UnityEngine;
using System.Collections;

public class CharJump : MonoBehaviour {
	CharStatus status;
	public float jumpSpeed;

	void Start () {
		status = GetComponent<CharStatus> ();
	}
		
	void FixedUpdate () {
		if (Input.GetKeyDown (KeyCode.Space) && status.onGround) {
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
		}
	}
}
