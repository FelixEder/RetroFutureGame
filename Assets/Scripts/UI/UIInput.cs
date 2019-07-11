using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInput : MonoBehaviour, Controls.IUIActions {
	Controls controls;

	void Awake() {
		controls = new Controls();
		controls.UI.SetCallbacks(this);
	}

	void OnEnable() {
		controls.UI.Enable();
	}

	void OnDisable() {
		controls.UI.Disable();
	}

	public void OnNavigate(InputAction.CallbackContext context) {
		gameObject.SendMessage("NavigateInput", context.ReadValue<Vector2>());
		Debug.Log("Input: " + context.action + "\nValue: " + context.ReadValue<Vector2>());
	}

	public void OnSelect(InputAction.CallbackContext context) {
		gameObject.SendMessage("SelectInput", context.ReadValue<float>());
		Debug.Log("Input: " + context.action + "\nValue: " + context.ReadValue<float>());
	}


	public void OnBack(InputAction.CallbackContext context) {
		gameObject.SendMessage("BackInput", context.ReadValue<float>());
		Debug.Log("Input: " + context.action + "\nValue: " + context.ReadValue<float>());
	}
}
