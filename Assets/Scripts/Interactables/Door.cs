using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public void setInvisible() {
		GetComponent<Renderer>().enabled = false;
		GetComponent<Collider2D> ().isTrigger = true;
	}

	void OnBecameInvisible() {
		GetComponent<Renderer>().enabled = true;
		GetComponent<Collider2D> ().isTrigger = false;
	}
}

