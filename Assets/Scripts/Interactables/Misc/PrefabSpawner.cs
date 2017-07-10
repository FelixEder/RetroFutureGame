using UnityEngine;
using System.Collections;

/**
 * This class spawns prefabs with regular intervals.
 */
public class PrefabSpawner : MonoBehaviour {
	[Header("Spawner")]
	public GameObject prefab;
	public float spawnStart, spawnInterval;
	public int maxSimultaneous;
	[Tooltip("Should a new prefab spawn when another is killed and current amount is less than max simultaneous?"), Space(10)]
	public bool continuousRespawn;
	[Header("Movement")]
	public float moveSpeed;
	public float timeBeforeWander, wanderDistance;
	[Header("Jump")]
	public float jumpForce;
	[Range(1, 100)]
	public float jumpchance = 1;
	[Header("Health")]

	[Header("Attack")]


	bool willRespawn;

	void Start() {
		willRespawn = true;
		InvokeRepeating("SpawnCheck", spawnStart, spawnInterval);
	}

	/**Instantiates "type" prefab as gameobject and sets this gameobject as parent if the parent's childcount is less than maxSimultaneous.*/
	void SpawnCheck() {
		if(transform.childCount < maxSimultaneous && continuousRespawn) {
			Spawn();
		}
		else if(transform.childCount < maxSimultaneous && !continuousRespawn && willRespawn) {
			Spawn();
			if(transform.childCount >= maxSimultaneous)
				willRespawn = false;
		}
	}

	void Spawn() {
		GameObject instance = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
		instance.transform.parent = transform;

	//Set all variables here!

		instance.GetComponent<SpriteRenderer>().sortingOrder = (int) Time.timeSinceLevelLoad;
	}

	public void KillChildren() {
		foreach(Transform child in transform) {
			GameObject.Destroy(child.gameObject);
		}
		Debug.Log("Prefab spawner:\nKilled all children.");
	}

	public void SetToRespawn() {
		willRespawn = true;
	}
}