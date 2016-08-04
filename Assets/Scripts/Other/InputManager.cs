using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	bool inputEnabled = true;
	//jump, attack, grab, leaf, pause, shoot, mega, small;

	public void Disable(float time) {
		Disable ();
		Invoke ("Enable", time);
	}

	public void Disable() {
		inputEnabled = false;
	}

	public void Enable() {
		inputEnabled = true;
	}

	public bool Enabled() {
		return inputEnabled;
	}

	public bool GetKey(string key) {
		if (!inputEnabled)
			return false;
		switch (key.ToLower()) {

		case "jump":
			return Input.GetButton("Jump");

		case "attack":
			return Input.GetButton ("Attack");

		case "grab":
			return Input.GetButton ("Pickup");

		case "float":
			return (Input.GetButton ("Leaf") || Input.GetAxis("Leaf") > 0);

		case "shoot":
			return Input.GetButton ("Shoot");

		case "mega":
			return Input.GetButton ("MegaAttack");

		case "small":
			return Input.GetButton ("SmallButton");

		default:
			return false;
		}
	}

	public float GetAxis(string axis) {
		if (!inputEnabled)
			return 0;
		switch (axis.ToLower()) {

		case "x":
			return Input.GetAxis ("Horizontal");

		case "y":
			return Input.GetAxis ("Vertical");

		default:
			return 0;
		}
	}
}
