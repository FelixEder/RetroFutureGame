using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	public GameObject checkPoint;

	void Start () {
		//Set the starting location of the game to current checkpoint here.
	}
	
	void OnTriggerEnter2D(Collider2D pointcheck) {
		if(pointcheck.gameObject.tag.Equals("CheckPoint")) {
			SetCheckPoint(pointcheck.gameObject);
		}
	}

	void SetCheckPoint(GameObject location) {
		checkPoint = location;
		//Maybe do some nice animation and add sound.
		Debug.Log("A location has been set!");
	}
}
