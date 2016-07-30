using UnityEngine;
using System.Collections;

/**
 * This class spawns prefabs with regular intervals.
 */
public class PrefabSpawner : MonoBehaviour {
	/**The amount of time in seconds between each spawn*/
	public float spawnStart, spawnInterval;
	/**The max amount of simultaneously spawned prefabs.*/
	public int maxSimultaneous;
	/**The name of the prefab to spawn*/
	public string prefab;

	void Start() {
		InvokeRepeating ("Spawn", spawnStart, spawnInterval);
	}

	/**Instantiates "type" prefab as gameobject and sets this gameobject as parent if the parent's childcount is less than maxSimultaneous.*/
	void Spawn() {
		if (transform.childCount < maxSimultaneous) {
			GameObject instance = Instantiate (Resources.Load (prefab), transform.position, Quaternion.identity) as GameObject;
			instance.transform.parent = transform;
		}
	}
}