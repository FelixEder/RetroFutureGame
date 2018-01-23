using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraParallax : MonoBehaviour {
	public Camera backCamera, frontCamera;
	public float backOffset, backMultiplier, frontOffset, frontMultiplier;
	float ortSize;

	void Start () {
		
	}

	void LateUpdate() {
		ortSize = GetComponent<Camera>().orthographicSize;

		backCamera.fieldOfView = backOffset + ortSize * backMultiplier;
		frontCamera.fieldOfView = frontOffset + ortSize * frontMultiplier;
	}
}
