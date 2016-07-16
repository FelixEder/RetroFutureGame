using UnityEngine;
using System.Collections;

public class CharStomp : MonoBehaviour {	
	Rigidbody2D rigidBody2D;
	CharStatus charStatus;
	GameObject triggerStomp;
	StompTrigger stompTrigger;
	public bool isStomping, groundStomping = false;
	bool holdStomp;

	// Use this for initialization
	void Start () {
		rigidBody2D = GetComponent<Rigidbody2D> ();
		charStatus = GetComponent<CharStatus> ();
		triggerStomp = GameObject.Find ("triggerStomp");
		stompTrigger = triggerStomp.GetComponent<StompTrigger> ();
		//Change sprite, display correct tutorial and play theme.
	}

	void Update() {
		if (!Input.GetButton ("Attack"))
			holdStomp = false;
		triggerStomp.SetActive(groundStomping);
	}
	
	void FixedUpdate() {
		if (charStatus.InAir () && !charStatus.isFloating && Input.GetAxis ("Vertical") < -0.7 && Input.GetButton ("Attack") && !holdStomp) {
			holdStomp = true;
			isStomping = true;
			rigidBody2D.velocity = new Vector2 (0, 0);
			rigidBody2D.gravityScale = 0.0f;
			//Play stomp-animation
			Invoke ("Stomp", 0.5f);
		} else if (charStatus.onSurface) {
			groundStomping = true;
			Invoke ("FinishedStomp", 1f);
		}
	}

	void FinishedStomp() {
		isStomping = false;
		groundStomping = false;
	}

	void Stomp() {
		rigidBody2D.gravityScale = 2.0f;
		rigidBody2D.velocity = new Vector2 (0, -9f);
	}	
}