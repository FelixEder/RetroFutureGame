using UnityEngine;
using System.Collections;

public class CharStomp : MonoBehaviour {	
	Rigidbody2D rigidBody2D;
	CharStatus charStatus;
	public bool isStomping;

	// Use this for initialization
	void Start () {
		rigidBody2D = GetComponent<Rigidbody2D> ();
		charStatus = GetComponent<CharStatus> ();
		isStomping;
		//Change sprite, display correct tutorial and play theme.
	}
	
	void FixedUpdate() {
		if (charStatus.InAir () && (Input.GetAxis ("Vertical") < -0.7) && Input.GetButton ("Attack")) {
			isStomping = true;
			rigidBody2D.velocity = new Vector2 (0, 0);
			//Play stomp-animation
			Invoke ("Stomp", 1f);
		} else if (charStatus.OnGround) {
			//Play stomp-ground animation
			Invoke ("FinishedStomp", 1f);
		}
	}

	void FinishedStomp() {
		isStomping = false;
	}

	void Stomp() {
		rigidBody2D.velocity = new Vector2 (0, 9f);
	}	
}

