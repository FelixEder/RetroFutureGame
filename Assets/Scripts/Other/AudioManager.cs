using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	public bool playOnEnter, stopOnExit, fadeIn, fadeOut;
	public float fadeInSpeed, fadeOutSpeed;
	float targetVolume, controlVolume;
	AudioSource audioSource;
	AudioControl control;

	void Start() {
		audioSource = GetComponent<AudioSource> ();
		control = GameObject.Find ("Audio").GetComponent<AudioControl>();
		targetVolume = audioSource.volume;
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
				audioSource.Play ();
		}
	}

	IEnumerator FadeIn() {
		audioSource.Play ();
		for (float volume = 0; volume < targetVolume; volume += fadeInSpeed) {
			audioSource.volume = volume;
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
				audioSource.Stop ();
		}
	}

	IEnumerator FadeOut() {
		for (float volume = audioSource.volume; volume > 0; volume -= fadeOutSpeed) {
			audioSource.volume = volume;
			yield return new WaitForSeconds (0.1f);
		}
		audioSource.Stop ();
	}

	/**Control the volume of the audio track in percent.*/
	void SetVolume(float volume) {
		audioSource.volume = targetVolume * volume * 0.01f;
	}

	void ResetVolume() {
		audioSource.volume = targetVolume;
	}
}
