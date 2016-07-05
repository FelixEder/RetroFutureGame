using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public void setInvisible(GameObject smashedDoor) {
		smashedDoor.GetComponent<Renderer>().enabled = false;
	}

	void OnBecameInvisible() {
		gameObject.GetComponent<Renderer>().enabled = true;
	}
}

