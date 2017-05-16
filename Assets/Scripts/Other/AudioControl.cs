using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AudioControl : MonoBehaviour {
	[SerializeField] float masterVolume = 100;
	[SerializeField] bool mute;

	void Update () {
		masterVolume = Mathf.Clamp (masterVolume, 0, 100);
	}

	public void SetMaster (float value) {
		masterVolume = value;
	}

	public float GetMaster () {
		if (mute)
			return 0;
		return masterVolume;
	}

	public void Mute (bool setMute) {
		mute = setMute;
	}
}
