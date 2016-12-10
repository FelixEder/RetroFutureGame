using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour {
	float masterVolume;

	void Start() {
		masterVolume = 100;
	}

	public void SetMaster(float value) {
		masterVolume = value;
	}

	public float GetMaster() {
		return masterVolume;
	}
}
