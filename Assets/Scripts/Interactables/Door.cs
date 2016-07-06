using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public Sprite open, close;
	void Start() {
		
	}

	public void setInvisible() {
		GetComponent<SpriteRenderer> ().sprite = open;
		GetComponent<Collider2D> ().isTrigger = true;
	}

	void OnBecameInvisible() {
		GetComponent<SpriteRenderer> ().sprite = close;
		GetComponent<Collider2D> ().isTrigger = false;
	}
}

