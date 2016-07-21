using UnityEngine;
using System.Collections;

/**
 * This class spawns prefabs with regular intervals.
 */
public class PrefabSpawner : MonoBehaviour {
	//The amount of time in seconds between each spawn
	public float spawnStart, spawnInterval;
	public int maxSimultaneous;
	//The name of the prefab to spawn
	public string spawnType;

	void Start() {
		InvokeRepeating ("Spawn", spawnStart, spawnInterval);
	}

	void Spawn() {
		if (transform.childCount < maxSimultaneous) {
			GameObject instance = Instantiate (Resources.Load (spawnType), transform.position, Quaternion.identity) as GameObject;
			instance.transform.parent = transform;
		}
	}
}