using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
	public AudioClip[] audioClips;
	public int currentAudio = -1;
	float masterVolume, fadeSpeed = 10;
	AudioSource audioSource;
	AudioControl control;

	void Start() {
		audioSource = GetComponent<AudioSource>();
		control = GameObject.Find("Audio").GetComponent<AudioControl>();
		//		Play (0, 1, false);
	}

	void Update() {
		if(masterVolume != control.GetMaster()) {
			masterVolume = control.GetMaster();
			audioSource.volume = masterVolume / 100;
		}
	}

	public void Play(int index, float volumeScale, bool fadeIn) {
		Debug.Log("[MUSIC] Play command Recieved");
		if(audioSource.isPlaying)
			StartCoroutine(SwapPlaying(index, volumeScale, fadeIn));
		else if(index < audioClips.Length && index >= 0) {
			Debug.Log("[MUSIC] Playing");
			audioSource.PlayOneShot(audioClips[index], volumeScale);
			currentAudio = index;
			if(fadeIn)
				StartCoroutine(FadeIn());
			StartCoroutine(Loop(index, volumeScale));
		}
		else
			Debug.Log("AudioPlayer: index out of range");
	}

	public void StopPlaying(bool fadeOut) {
		Debug.Log("[MUSIC] Stopping track");
		StopCoroutine("Loop");
		if(fadeOut)
			StartCoroutine(FadeOut());
		else {
			audioSource.Stop();
			currentAudio = -1;
		}
	}

	public void Mute(bool mute) {
		audioSource.mute = mute;
	}

	public void FadeSpeed(float speed) {
		fadeSpeed = speed;
	}

	IEnumerator Loop(int index, float volumeScale) {
		Debug.Log("[MUSIC] Loop waiting");
		//		yield return new WaitForSeconds (audioClips [index].length);
		while(audioSource.isPlaying)
			yield return 0;
		Debug.Log("[MUSIC] Loop engaged");
		Play(index, volumeScale, false);
	}

	IEnumerator FadeIn() {
		Debug.Log("[MUSIC] Fading in for " + 10 / fadeSpeed + " second(s)");
		for(float volume = 0; volume < masterVolume; volume += fadeSpeed) {
			audioSource.volume = volume / 100;
			yield return new WaitForSeconds(0.1f);
		}
		audioSource.volume = masterVolume / 100;
		Debug.Log("[MUSIC] Fade in complete");
	}

	IEnumerator FadeOut() {
		Debug.Log("[MUSIC] Fading out for " + 10 / fadeSpeed + " second(s)");
		for(float volume = audioSource.volume * 100; volume > 0; volume -= fadeSpeed) {
			audioSource.volume = volume / 100;
			yield return new WaitForSeconds(0.1f);
		}
		audioSource.volume = 0;
		Debug.Log("[MUSIC] Fade out complete");
		audioSource.Stop();
		currentAudio = -1;
	}

	IEnumerator SwapPlaying(int index, float volumeScale, bool fadeIn) {
		Debug.Log("[MUSIC] Swapping tracks");
		StopPlaying(true);
		Debug.Log("[MUSIC] Playing new track in " + 10 / fadeSpeed + " second(s)");
		//		yield return new WaitForSeconds (10 / fadeSpeed);
		while(audioSource.isPlaying)
			yield return 0;
		Play(index, volumeScale, fadeIn);
	}
}
