using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour, Controls.IPlayerActions {

	Controls controls;

	void Awake() {
		controls = new Controls();
		controls.Player.SetCallbacks(this);
	}

	void OnEnable() {
		controls.Player.Enable();
	}

	void OnDisable() {
		controls.Player.Disable();
	}

	public void OnJump(InputAction.CallbackContext context) {
		gameObject.SendMessage("JumpInput", context.ReadValue<float>());
	}

	public void OnMove(InputAction.CallbackContext context) {
		gameObject.SendMessage("MoveInput", context.ReadValue<Vector2>());
	}
}
