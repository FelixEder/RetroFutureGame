﻿using UnityEngine;
using System.Collections;

public class Throw : MonoBehaviour {
	int maxRocks;
	GameObject FBIM;

	void Start() {
		FBIM = GameObject.Find ("FinalBossInstanceManager");
	}

	void OnEnable() {
		Debug.Log ("Boss throws things");
		//Also play relevant soundFX
		Throwing();
	}
	void Throwing() {
		string spawnType = "";
		int item = Random.Range (0, 10);
		if (item < 8 && FBIM.transform.childCount < maxRocks) {
			spawnType = "Rock";
		} else if (item < 9) {
			spawnType = "HealthDrop";
		} else {
			spawnType = "EnergyDrop";
		}
		GameObject instance = Instantiate (Resources.Load (spawnType), transform.position, Quaternion.identity) as GameObject;
		instance.transform.SetParent (FBIM);
		if(spawnType.Equals( "Rock")) {
			instance.GetComponent<Rigidbody2D>().AddForce(new Vector2(8f, 3f), ForceMode2D.Impulse);
		}
	}
}