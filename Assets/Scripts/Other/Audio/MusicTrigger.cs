using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour {
	public int playMusicFromIndex;
	public float withWolumeScale;
	public bool andFade;
	public GameObject musicObject;
	
	public MusicManager manager;
	public string track;

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag.Equals("Player")) {
			/*
			MusicPlayer musicPlay = musicObject.GetComponent<MusicPlayer>();
			if(musicPlay.currentAudio != playMusicFromIndex) {
				Debug.Log("Playing music from index " + playMusicFromIndex);
				musicPlay.Play(playMusicFromIndex, withWolumeScale, andFade);
			}
			*/

			manager.PlayTrack(track, withWolumeScale);
		}
	}
}
