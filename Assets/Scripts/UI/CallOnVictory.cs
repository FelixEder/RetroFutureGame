using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallOnVictory : MonoBehaviour {
	public bool enabled;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	void OnCollisionEnter2D(Collision2D col) {
		if(enabled)
			GameObject.Find ("VictoryScreen").GetComponent<VictoryScreen> ().ShowVictory ();
	}
}
