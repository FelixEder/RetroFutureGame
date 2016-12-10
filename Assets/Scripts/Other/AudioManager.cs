using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	public bool playOnEnter, stopOnExit, fadeIn, fadeOut;
	public float fadeInSpeed, fadeOutSpeed;
	float targetVolume, controlVolume;
	AudioSource audio;
	AudioControl control;

	void Start() {
		audio = GetComponent<AudioSource> ();
		control = GameObject.Find ("Audio").GetComponent<AudioControl>();
		targetVolume = audio.volume;
		controlVolume = 100;
	}

	void Update() {
		if (controlVolume != control.GetMaster ()) {
			controlVolume = control.GetMaster ();
			if (controlVolume == 100)
				ResetVolume ();
			else
				SetVolume (controlVolume);
		}
	}

	void OnTriggerEnter2D() {
		if (playOnEnter) {
			if (fadeIn) {
				StopAllCoroutines();
				StartCoroutine (FadeIn ());
			}
			else
				audio.Play ();
		}
	}

	IEnumerator FadeIn() {
		audio.Play ();
		for (float volume = 0; volume < targetVolume; volume += fadeInSpeed) {
			audio.volume = volume;
			yield return new WaitForSeconds (0.1f);
		}
	}

	void OnTriggerExit2D() {
		if (stopOnExit) {
			if (fadeOut) {
				StopAllCoroutines();
				StartCoroutine (FadeOut ());
			}
			else
				audio.Stop ();
		}
	}

	IEnumerator FadeOut() {
		for (float volume = audio.volume; volume > 0; volume -= fadeOutSpeed) {
			audio.volume = volume;
			yield return new WaitForSeconds (0.1f);
		}
		audio.Stop ();
	}

	/**Control the volume of the audio track in percent.*/
	void SetVolume(float volume) {
		audio.volume = targetVolume * volume * 0.01f;
	}

	void ResetVolume() {
		audio.volume = targetVolume;
	}
}
