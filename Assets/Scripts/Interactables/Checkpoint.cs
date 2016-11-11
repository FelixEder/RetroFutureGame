using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	//public GameObject checkpoint;
	public Transform activeCheckpoint;

	void Start () {
		activeCheckpoint = GameObject.Find("CheckPoint (1)").transform;
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag.Equals("CheckPoint")) {
			SetCheckpoint(col.gameObject.transform);
		}
	}

	void SetCheckpoint(Transform location) {
		activeCheckpoint = location;
		//Maybe do some nice animation and add sound.
		Debug.Log("Checkpoint activated.\nLocation has been set to: " + location.position);
	}
}
