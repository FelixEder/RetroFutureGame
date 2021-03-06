﻿using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	[SerializeField] bool inputEnabled = true, force;
	//jump, attack, grab, leaf, pause, shoot, mega, small;

	public void Disable(float duration) {
		if(force)
			return;
		Disable();
		CancelInvoke("Enable");
		Invoke("Enable", duration);
	}

	public void Disable() {
		if(force)
			return;
		CancelInvoke("Enable");
		inputEnabled = false;
	}

	public void Enable() {
		if(force)
			return;
		CancelInvoke("Enable");
		inputEnabled = true;
	}

	public bool Enabled() {
		return inputEnabled;
	}

	public void Force(bool input, bool _force) {
		inputEnabled = input;
		force = _force;
	}

	public bool GetKey(string key) {
		if(!inputEnabled)
			return false;
		switch(key.ToLower()) {

			case "jump":
				return Input.GetButton("Jump");

			case "attack":
				return Input.GetButton("Attack");

			case "grab":
				return Input.GetButton("Pickup");

			case "float":
				return Input.GetButton("Leaf") || Input.GetAxis("Leaf") > 0;

			case "shoot":
				return Input.GetButton("Shoot") || Input.GetAxis("Shoot") > 0;

			case "mega":
				return Input.GetButton("MegaAttack");

			case "small":
				return Input.GetButton("SmallButton");

			case "x":
				return Input.GetAxis("Horizontal") != 0;

			case "y":
				return Input.GetAxis("Vertical") != 0;

			default:
				return false;
		}
	}

	public float GetAxis(string axis) {
		if(!inputEnabled)
			return 0;
		switch(axis.ToLower()) {

			case "x":
				return Input.GetAxis("Horizontal");

			case "y":
				return Input.GetAxis("Vertical");

			case "ysign":
				return Mathf.Sign(Input.GetAxis("VerticalBool") + Input.GetAxis("VerticalBoolController"));

			case "rightx":
				return Input.GetAxis("RightAnalogH");

			case "righty":
				return Input.GetAxis("RightAnalogV");

			default:
				return 0;
		}
	}
}
