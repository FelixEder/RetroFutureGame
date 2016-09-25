using UnityEngine;
using System.Collections;

/**
 * This class spawns prefabs with regular intervals.
 */
public class PickupableItemSpawner : MonoBehaviour {
	/**The amount of time in seconds between each spawn*/
	public float spawnStart, spawnInterval;
	/**The max amount of simultaneously spawned prefabs.*/
	public int maxSimultaneous, spawnArea;
	//The current number of spawned prefabs.
	int current;
	/**The name of the prefab to spawn*/
	public string prefab;
	Vector3 spawnLoc;

	void Start() {
		InvokeRepeating ("Spawn", spawnStart, spawnInterval);
		spawnLoc = transform.position;
	}

	/**Instantiates "type" prefab as gameobject and sets this gameobject as parent if the parent's childcount is less than maxSimultaneous.*/
	void Spawn() {
		if (current < maxSimultaneous) {
			Vector3 babyBoom = new Vector3 (spawnLoc.x + Random.Range (-spawnArea, spawnArea), spawnLoc.y + Random.Range (-spawnArea, spawnArea), spawnLoc.z);
			GameObject instance = Instantiate (Resources.Load (prefab), babyBoom, Quaternion.identity) as GameObject;
			instance.GetComponent<PickUpableItem> ().PIS = GetComponent<PickupableItemSpawner> ();
			IncreaseCurrent ();
		}
	}

	public void IncreaseCurrent() {
		current++;
	}

	public void DecreaseCurrent() {
		current--;
	}
}