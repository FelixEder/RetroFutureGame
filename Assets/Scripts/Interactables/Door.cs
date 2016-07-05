using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public void changeVisibility() {
		gameObject.renderer.enabled = false;
	}

	void OnBecameInvisible() {
		gameObject.renderer.enabled = true;
	}
}

