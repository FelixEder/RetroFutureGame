using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour {
	public Sprite regular, hatched;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer>().sprite = regular;
		Invoke("Hatch", 10f);
	}
	
	void Hatch() {
		GetComponent<SpriteRenderer>().sprite = hatched;
		//Here, Spawn random monster
		Invoke("Destroy", 2f);
	}

	void Destroy() {
		Destroy(gameObject);
	}
}