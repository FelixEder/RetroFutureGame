using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, Controls.IPlayerActions {

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

	public void OnMove(InputAction.CallbackContext context) {
		gameObject.SendMessage("MoveInput", context.ReadValue<Vector2>());
		Debug.Log("Input: " + context.action + "\nValue: " + context.ReadValue<Vector2>());
	}

	public void OnAim(InputAction.CallbackContext context) {
		//gameObject.SendMessage("AimInput", context.ReadValue<Vector2>());
		//Debug.Log("Input: " + context.action + "\nValue: " + context.ReadValue<Vector2>());
	}

	public void OnJump(InputAction.CallbackContext context) {
		gameObject.SendMessage("JumpInput", context.ReadValue<float>());
		Debug.Log("Input: " + context.action + "\nValue: " + context.ReadValue<float>());
	}


	public void OnGrab(InputAction.CallbackContext context) {
		gameObject.SendMessage("GrabInput", context.ReadValue<float>());
		Debug.Log("Input: " + context.action + "\nValue: " + context.ReadValue<float>());
	}

	public void OnShoot(InputAction.CallbackContext context) {
		gameObject.SendMessage("ShootInput", context.ReadValue<float>());
		Debug.Log("Input: " + context.action + "\nValue: " + context.ReadValue<float>());
	}

	public void OnFloat(InputAction.CallbackContext context) {
		gameObject.SendMessage("FloatInput", context.ReadValue<float>());
		Debug.Log("Input: " + context.action + "\nValue: " + context.ReadValue<float>());
	}

	public void OnSmall(InputAction.CallbackContext context) {
		gameObject.SendMessage("SmallInput", context.ReadValue<float>());
		Debug.Log("Input: " + context.action + "\nValue: " + context.ReadValue<float>());
	}

	public void OnAttack(InputAction.CallbackContext context) {
		gameObject.SendMessage("AttackInput", context.ReadValue<float>());
		Debug.Log("Input: " + context.action + "\nValue: " + context.ReadValue<float>());
	}

	public void OnPause(InputAction.CallbackContext context) {
		gameObject.SendMessage("PauseInput", context.ReadValue<float>());
		Debug.Log("Input: " + context.action + "\nValue: " + context.ReadValue<float>());
	}
}
