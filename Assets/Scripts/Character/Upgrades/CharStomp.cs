using UnityEngine;
using System.Collections;

public class CharStomp : MonoBehaviour {	
	Rigidbody2D rigidBody2D;
	CharStatus status;
	InputManager input;
	public GameObject triggerStomp;
	//I think groundStomping is unneccecary
	public bool isStomping, groundStomping;
	bool holdStomp;

	// Use this for initialization
	void Start () {
		rigidBody2D = GetComponent<Rigidbody2D> ();
		status = GetComponent<CharStatus> ();
		input = GameObject.Find ("InputManager").GetComponent<InputManager> ();
		//Change sprite, display correct tutorial and play theme.
	}

	void FixedUpdate() {
		if (!input.GetKey ("attack"))
			holdStomp = false;
		if (status.InAir () && !status.isSmall && !status.isFloating && input.GetAxis ("Y") < -0.3f && input.GetKey ("attack") && !holdStomp) {
			holdStomp = true;
			isStomping = true;
			rigidBody2D.velocity = new Vector2 (0, 0);
			rigidBody2D.gravityScale = 0.0f;
			//Play stomp-animation
			Invoke ("Stomp", 0.5f);
		}
		else if (status.onSurface && isStomping) {
			//Play stomp-on-ground animation
			Debug.Log("You are stomping something!");
			groundStomping = true;
			isStomping = false;
			Invoke ("FinishedStomp", 0.25f);
		}
	}

	void FinishedStomp() {
		status.invulnerable = false;
		groundStomping = false;
		triggerStomp.SetActive (false);
	}

	void Stomp() {
		status.invulnerable = true;
		rigidBody2D.gravityScale = 2.0f;
		rigidBody2D.velocity = new Vector2 (0, -9f);
		triggerStomp.SetActive (true);
	}	
}