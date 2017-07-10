using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour {
	public AudioClip[] audioClips;
	float masterVolume;
	AudioSource audioSource;
	AudioControl control;

	void Start() {
		audioSource = GetComponent<AudioSource>();
		control = GameObject.Find("Audio").GetComponent<AudioControl>();
	}

	void Update() {
		if(masterVolume != control.GetMaster()) {
			masterVolume = control.GetMaster();
			if(audioSource != null)
				audioSource.volume = masterVolume / 100;
		}
	}

	public void PlayClip(int index, float volumeScale) {
		if(index < audioClips.Length && index >= 0)
			audioSource.PlayOneShot(audioClips[index], volumeScale);
		else
			Debug.Log("AudioPlayer: index out of range");
	}

	public void PlayClip(int index, float volumeScale, float minPitch, float maxPitch) {
		if(minPitch == maxPitch)
			audioSource.pitch = minPitch;
		else
			audioSource.pitch = Random.Range(minPitch, maxPitch);
		PlayClip(index, volumeScale);
	}

	public void StopPlaying() {
		audioSource.Stop();
	}

	public void PlayDetached(int index, float volumeScale, float minPitch, float maxPitch) {
		GameObject instance = Instantiate(Resources.Load("AudioSourceDrop"), transform.position, Quaternion.identity) as GameObject;
		instance.GetComponent<AudioPlayer>().PlayingDetached(audioClips[index], volumeScale, minPitch, maxPitch, audioClips[index].length);

	}

	public void Mute(bool mute) {
		audioSource.mute = mute;
	}

	public void PlayingDetached(AudioClip clip, float volumeScale, float minPitch, float maxPitch, float forTime) {
		GetComponent<AudioSource>().PlayOneShot(clip, volumeScale);
		GetComponent<AudioSource>().pitch = Random.Range(minPitch, maxPitch);
		GetComponent<AudioSource>().volume = GameObject.Find("Audio").GetComponent<AudioControl>().GetMaster();
		Invoke("Kill", forTime);
	}

	void Kill() {
		GameObject.Destroy(gameObject);
	}
}
