using UnityEngine;
using System.Collections;

public class CharStomp : MonoBehaviour {	
	Rigidbody2D rigidBody2D;
	CharStatus CharStatus;
	public GameObject triggerStomp;
	public bool isStomping, groundStomping;
	bool holdStomp;

	// Use this for initialization
	void Start () {
		rigidBody2D = GetComponent<Rigidbody2D> ();
		CharStatus = GetComponent<CharStatus> ();
		//Change sprite, display correct tutorial and play theme.
	}

	void Update() {
		if (!Input.GetButton ("Attack"))
			holdStomp = false;
		//triggerStomp.SetActive(groundStomping);
	}
	
	void FixedUpdate() {
		if (CharStatus.InAir () && !CharStatus.isFloating && Input.GetAxis ("Vertical") < -0.3f && Input.GetButton ("Attack") && !holdStomp) {
			holdStomp = true;
			isStomping = true;
			rigidBody2D.velocity = new Vector2 (0, 0);
			rigidBody2D.gravityScale = 0.0f;
			//Play stomp-animation
			Invoke ("Stomp", 0.5f);
		} else if (CharStatus.onSurface && isStomping) {
			//Play stomp-on-ground animation
			groundStomping = true;
			isStomping = false;
			Invoke ("FinishedStomp", 0.25f);
		}
	}

	void FinishedStomp() {
		groundStomping = false;
		triggerStomp.SetActive (false);
	}

	void Stomp() {
		rigidBody2D.gravityScale = 2.0f;
		rigidBody2D.velocity = new Vector2 (0, -9f);
		triggerStomp.SetActive (true);
	}	
}