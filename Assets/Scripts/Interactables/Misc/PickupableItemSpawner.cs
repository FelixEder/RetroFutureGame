using UnityEngine;
using System.Collections;

/**
 * This class spawns prefabs with regular intervals.
 */
public class PickupableItemSpawner : MonoBehaviour {
	/**The amount of time in seconds between each spawn*/
	public float spawnStart, spawnInterval;
	/**The max amount of simultaneously spawned prefabs.*/
	public int maxSimultaneous, spawnAreaX, spawnAreaY;
	//The current number of spawned prefabs.
	/**The name of the prefab to spawn*/
	public string prefab;
	Vector3 spawnLoc;

	void Start() {
		InvokeRepeating("Spawn", spawnStart, spawnInterval);
		spawnLoc = transform.position;
	}

	/**Instantiates "type" prefab as gameobject and sets this gameobject as parent if the parent's childcount is less than maxSimultaneous.*/
	void Spawn() {
		if(transform.childCount < maxSimultaneous) {
			Vector3 babyBoom = new Vector3(spawnLoc.x + Random.Range(-spawnAreaX, spawnAreaX), spawnLoc.y + Random.Range(-spawnAreaY, spawnAreaY), spawnLoc.z);
			GameObject instance = Instantiate(Resources.Load(prefab), babyBoom, Quaternion.identity) as GameObject;
			instance.transform.parent = transform;
		}
	}

	public void KillChildren() {
		foreach(Transform child in transform) {
			GameObject.Destroy(child.gameObject);
		}
		Debug.Log("Prefab spawner:\nKilled all children.");
	}
}