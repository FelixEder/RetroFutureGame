using UnityEngine;
using System.Collections;

public class MapCover : MonoBehaviour {
	public bool isInside;

	void OnTriggerStay2D(Collider2D col) {
		if(col.gameObject.name == "triggerMapCover" && !isInside)
			isInside = true;
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.name == "triggerMapCover")
			isInside = false;
	}
}
