using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {
	public AudioClip[] audioClips;

	AudioSource audioSource;

	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}

	public void PlayClip (int index, float volumeScale) {
		if (index < audioClips.Length)
			audioSource.PlayOneShot (audioClips [index], volumeScale);
		else
			Debug.Log ("AudioPlayer: index out of range");
	}

	public void PlayClip (int index, float volumeScale, float minPitch, float maxPitch) {
		if (minPitch == maxPitch)
			audioSource.pitch = minPitch;
		else
			audioSource.pitch = Random.Range (minPitch, maxPitch);
		PlayClip (index, volumeScale);
	}

	public void StopPlaying () {
		audioSource.Stop ();
	}

	public void PlayDetached (int index, float volumeScale, float minPitch, float maxPitch) {
		GameObject instance = Instantiate (Resources.Load ("AudioSourceDrop"), transform.position, Quaternion.identity) as GameObject;
		instance.GetComponent<AudioPlayer> ().PlayingDetached(audioClips[index], volumeScale, minPitch, maxPitch, audioClips[index].length);

	}

	public void PlayingDetached (AudioClip clip, float volumeScale, float minPitch, float maxPitch, float forTime) {
		GetComponent<AudioSource> ().PlayOneShot (clip, volumeScale);
		GetComponent<AudioSource> ().pitch = Random.Range (minPitch, maxPitch);
		Invoke("Kill", forTime);
	}

	void Kill () {
		GameObject.Destroy (gameObject);
	}
}
