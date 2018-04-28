using UnityEngine;
using System.Collections;


public class PrefabSpawner : MonoBehaviour {
	[Header("Spawner")]
	public GameObject prefab;
	public float spawnStart;
	[Range(0.1f, 1000f)]
	public float spawnInterval;
	public int maxSimultaneous;
	[Tooltip("Should a new prefab spawn when another is killed and current amount is less than max simultaneous?"), Space(5)]
	public bool continuousRespawn;

	[Header("Movement")]
	public bool setMovement;
	[Space(5), Tooltip("If set to 0, prefab will move OnBecameVisible")]
	public float timeBeforeWander;
	public float wanderSpeed, wanderDistance;
	[Space(5)]
	public float followSpeed = 3;
	public FollowRange followRange;

	[Header("Jump")]
	public bool setJump;
	[Space(5)]
	public float jumpForce = 7;
	[Range(1, 100)]
	public int jumpChance = 1;

	[Header("Health")]
	public bool setHealth;
	[Space(5)]
	public int health = 1;
	public float invulnerabilityTime = 0.5f;

	[Header("Attack")]
	public bool setAttack;
	[Space(5)]
	public int damage = 1;
	public float knockbackForce = 5;

	[Header("Color")]
	public bool setColor;
	public Color color = Color.white;

	bool willRespawn = true;

	void Start() {
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
		if(setMovement) {
			var movement = instance.GetComponent<EnemyMovement>();
			movement.followSpeed = followSpeed;
			movement.timeBeforeWander = timeBeforeWander;
			movement.wanderSpeed = wanderSpeed;
			movement.wanderDist = wanderDistance;
			movement.followRange = followRange;
}
		if(setJump) {
			var jump = instance.GetComponent<EnemyJump>();
			jump.jumpForce = jumpForce;
			jump.jumpChance = jumpChance;
		}
		if(setHealth) {
			var _health = instance.GetComponent<EnemyHealth>();
			_health.health = health;
			_health.invulnerabilityTime = invulnerabilityTime;
		}
		if(setAttack) {
			var attack = instance.GetComponent<EnemyAttack>();
			attack.damage = damage;
			attack.knockbackForce = knockbackForce;
		}
		if(setColor) {
			instance.GetComponent<SpriteRenderer>().color = color;
		}
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