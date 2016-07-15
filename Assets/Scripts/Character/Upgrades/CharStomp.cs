using UnityEngine;
using System.Collections;

public class CharStomp : MonoBehaviour {	
	Rigidbody2D rigidBody2D;
	CharStatus charStatus;
	public bool isStomping;
	bool holdStomp;

	// Use this for initialization
	void Start () {
		rigidBody2D = GetComponent<Rigidbody2D> ();
		charStatus = GetComponent<CharStatus> ();
		//Change sprite, display correct tutorial and play theme.
	}

	void Update() {
		if (!Input.GetButton ("Attack"))
			holdStomp = false;
	}
	
	void FixedUpdate() {
		if (charStatus.InAir () && Input.GetAxis ("Vertical") < -0.7 && Input.GetButton ("Attack") && !holdStomp) {
			holdStomp = true;
			isStomping = true;
			rigidBody2D.velocity = new Vector2 (0, 0);
			rigidBody2D.gravityScale = 0.0f;
			//Play stomp-animation
			Invoke ("Stomp", 0.5f);
		} else if (charStatus.onGround) {
			//Play stomp-ground animation
			Invoke ("FinishedStomp", 1f);
		}
	}

	void FinishedStomp() {
		isStomping = false;
	}

	void Stomp() {
		rigidBody2D.gravityScale = 2.0f;
		rigidBody2D.velocity = new Vector2 (0, -9f);
	}	
}

