using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaTitle : MonoBehaviour {
	public Text areaText;
	public Animator anim;
	private string areaToDisplay;
	
	// Update is called once per frame
	void Update () {
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("areaTextHidden")) {
			areaText.text = areaToDisplay;
		}
	}

	public void SetAreaText(string areaText) {
		areaToDisplay = areaText;
		anim.SetTrigger("Start");
	}
}
