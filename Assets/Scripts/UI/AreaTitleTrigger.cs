using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaTitleTrigger : MonoBehaviour {
	public string area;
	public AreaTitle areaTitle;

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag.Equals("Player")) {
			areaTitle.SetAreaText(area);
		}
	}
}	