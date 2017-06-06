using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour {
	public int playMusicFromIndex;
	public float withWolumeScale;
	public bool andFade;
	public GameObject musicObject;

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag.Equals ("Char")) {
			MusicPlayer musicPlay = musicObject.GetComponent<MusicPlayer> ();
			if (musicPlay.currentAudio != playMusicFromIndex) {
				Debug.Log ("Playing music from index " + playMusicFromIndex);
				musicPlay.Play (playMusicFromIndex, withWolumeScale, andFade);
			}
		}
	}
}
