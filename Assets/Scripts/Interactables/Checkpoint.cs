﻿using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	//public GameObject checkpoint;
	public GameObject activeCheckpoint;

	void Start() {
		//activeCheckpoint = GameObject.Find("CheckPoint (1)");
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag.Equals("CheckPoint")) {
			if(!col.gameObject.Equals(activeCheckpoint))
				SetCheckpoint(col.gameObject);
		}
	}

	void SetCheckpoint(GameObject checkpoint) {
		activeCheckpoint = checkpoint;
		//Maybe do some nice animation and add sound.
		if (checkpoint.name.Contains("Invisible")) {

		}
		else {
			checkpoint.GetComponent<AudioPlayer>().PlayClip(0, 0.5f);
			GameObject.Find("tutorialPanel").GetComponent<TutorialManager>().DisplayTutorial("", "Checkpoint", false, 1.5f);
		}
		Debug.Log("Checkpoint activated.\nLocation has been set to: " + checkpoint.transform.position);
	}
}
