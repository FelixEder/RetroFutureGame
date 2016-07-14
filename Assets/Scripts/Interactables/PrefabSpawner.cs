using UnityEngine;
using System.Collections;

/**
 * This class spawns prefabs with regular intervals.
 */
public class PrefabSpawner : MonoBehaviour {
	//The amount of time in seconds between each spawn
	public float spawnInterval;
	//The name of the prefab to spawn
	public string spawnType;
	
	// Update is called once per frame
	void Update () {
		while (true) {
			Invoke ("spawn", spawnInterval);
		}
	}
		
	void spawn() {
		Instantiate (Resources.Load (spawnType), transform.position, Quaternion.identity);
	}
}
