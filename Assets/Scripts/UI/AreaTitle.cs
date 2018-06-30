using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaTitle : MonoBehaviour {
	public string area;
	public Text areaText;
	public Animator anim;

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag.Equals("Player")) {
			areaText.text = area;
			anim.SetTrigger("Start");
		}
	}
}