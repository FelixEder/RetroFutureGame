using UnityEngine;
using System.Collections;

/**
 * This class spawns prefabs with regular intervals.
 */
public class PrefabSpawner : MonoBehaviour {
	/**The amount of time in seconds between each spawn*/
	public float spawnStart, spawnInterval, prefabInitialFreezeTime;
	/**The max amount of simultaneously spawned prefabs.*/
	public int maxSimultaneous;
	/**The name of the prefab to spawn*/
	public string prefab;

	public bool continuousRespawn;
	bool willRespawn;

	void Start() {
		willRespawn = true;
		InvokeRepeating ("SpawnCheck", spawnStart, spawnInterval);
	}

	/**Instantiates "type" prefab as gameobject and sets this gameobject as parent if the parent's childcount is less than maxSimultaneous.*/
	void SpawnCheck() {
		if (transform.childCount < maxSimultaneous && continuousRespawn) {
			Spawn ();
		} else if (transform.childCount < maxSimultaneous && !continuousRespawn && willRespawn) {
			Spawn ();
			if (transform.childCount >= maxSimultaneous)
				willRespawn = false;
		}
	}

	void Spawn() {
		GameObject instance = Instantiate (Resources.Load (prefab), transform.position, Quaternion.identity) as GameObject;
		instance.transform.parent = transform;
		instance.GetComponent<SpawnProperties> ().initialFreezeTime = prefabInitialFreezeTime;
		instance.GetComponent<SpriteRenderer> ().sortingOrder = (int)Time.timeSinceLevelLoad;
	}

	public void KillChildren() {
		foreach (Transform child in transform) {
			GameObject.Destroy (child.gameObject);
		}
		Debug.Log ("Prefab spawner:\nKilled all children.");
	}

	public void SetToRespawn() {
		willRespawn = true;
	}
}