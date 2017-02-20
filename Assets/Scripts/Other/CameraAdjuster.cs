using UnityEngine;
using System.Collections;

public class CameraAdjuster : MonoBehaviour {
	public float adjustX, adjustY, setSize, transitionSpeed;
	public GameObject focusTarget;
	public bool resetOnExit;

	public float GetX() {
		return adjustX;
	}

	public float GetY() {
		return adjustY;
	}

	public float GetSize() {
		return setSize;
	}

	public float GetSpeed() {
		return transitionSpeed;
	}

	public GameObject GetFocus() {
		return focusTarget;
	}

	public bool GetReset() {
		return resetOnExit;
	}
}
