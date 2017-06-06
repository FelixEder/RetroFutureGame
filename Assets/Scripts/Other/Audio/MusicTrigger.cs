using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour {
	public int playMusicFromIndex;
	public float withWolumeScale;
	public bool andFade;
	public GameObject musicObject;

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag.Equals ("Char"))
			musicObject.GetComponent<MusicPlayer> ().Play (playMusicFromIndex, withWolumeScale, andFade);
	}
}
