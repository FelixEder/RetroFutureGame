using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	public bool playOnEnter, stopOnExit, fadeIn, fadeOut;
	public float fadeInSpeed, fadeOutSpeed;
	float targetVolume;
	AudioSource audio;

	void Start() {
		audio = GetComponent<AudioSource> ();
		targetVolume = audio.volume;
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
}
