using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
	[Range(0, 1)]
	public float masterVolume = 1;
	int current = -1, next;
	bool fading;

	public void PlayTrack(string track, float time) {
		StartCoroutine(VolumeFade(track, time));
	}

	AudioSource GetAS(int child) {
		return transform.GetChild(child).GetComponent<AudioSource>();
	}

	IEnumerator VolumeFade(string track, float time) {
		while(fading)
			yield return null;
		switch(track.ToLower()) {
			case "nature":
				next = 0;
				break;

			case "cave":
				next = 1;
				break;

			case "facility":
				next = 2;
				break;

			case "dungeon":
				next = 3;
				break;

			case "statueboss":
				next = 4;
				break;

			case "birdboss":
				next = 5;
				break;

			case "finalboss":
				next = 6;
				break;

			default:
				break;
		}
		if(next == current) {
			Debug.Log("MusicManager: Track already playing");
			yield break;
		}
		Debug.Log("MusicManager: Playing track " + next);
		fading = true;
		Debug.Log("MusicManager: Starting fade");
		for (int i = 0; i < 20; i++) {
			if(current >= 0)
				GetAS(current).volume -= 0.05f * masterVolume;
			if(GetAS(next).volume < masterVolume)
				GetAS(next).volume += 0.05f * masterVolume;
			yield return new WaitForSeconds(time / 20);
		}
		if(current >= 0)
			GetAS(current).volume = 0;
		GetAS(next).volume = masterVolume;
		current = next;
		fading = false;
		Debug.Log("MusicManager: Ending fade");
	}
}
