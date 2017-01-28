using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallOnVictory : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	void OnCollisionEnter2D(Collision2D col) {
		GameObject.Find ("VictoryScreen").GetComponent<VictoryScreen> ().ShowVictory ();
	}
}
