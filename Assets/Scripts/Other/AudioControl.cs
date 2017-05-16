using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class AudioControl : MonoBehaviour {
	[SerializeField] float masterVolume = 100, multiplyVolume = 100;
	[SerializeField] bool mute;
	public GameObject volumeSlider;

	void Start () {
		volumeSlider.GetComponent<Slider> ().value = masterVolume;
	}

	void Update () {
		masterVolume = Mathf.Clamp (masterVolume, 0, 100);
	}

	public void SetMaster (float value) {
		masterVolume = value;
	}

	public float GetMaster () {
		if (mute)
			return 0;
		return masterVolume * (multiplyVolume / 100);
	}

	public void Mute (bool setMute) {
		mute = setMute;
	}

	public void SetMultiplier (float value) {
		multiplyVolume = value;
	}
}
