﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaTitle : MonoBehaviour {
	public Text areaText;
	public Animator anim;
	private string areaToDisplay;
	int clip;

	// Update is called once per frame
	void Update () {
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("areaTextHidden")) {
			areaText.text = areaToDisplay;
		}
	}

	public void SetAreaText(string areaText) {
		//Only display area title if it isn't the area the player is currently in.
		if(!areaText.Equals(areaToDisplay)) {
			clip = 0;
			areaToDisplay = areaText;
			anim.SetTrigger("Start");
		}
	}

	public void SetBossDefeatText(string bossText) {
		clip = 1;
		areaText.text = bossText;
		anim.SetTrigger("Start");
	}
	
	public void PlayAudio() {
		GetComponent<AudioPlayer>().PlayClip(clip, 1f);
	}
}
