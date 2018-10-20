using UnityEngine;
using System.Collections;

public class Phase2 : MonoBehaviour {
	public float knockForce, movespeed = 3;
	float moveSpeed = 3;
	public int health, spawnLoc = 2;
	public Sprite normal, kickPunching;
	public bool stunned, blued, walksRight = true;
	Rigidbody2D rb2D;
	public int deltaX, damage;

	// Use this for initialization
	void Start() {
		rb2D = GetComponent<Rigidbody2D>();
		InvokeRepeating("KickPunching", 3f, 5f);
		InvokeRepeating("Charge", 5f, 20f);
		ResetDeltaX();
		transform.GetChild(0).GetComponent<Phase2Head>().enabled = true;
		moveSpeed = movespeed;
	}

	void Update() {
		if(Mathf.Abs(deltaX - (int) transform.position.x) >= 10) {
			if(Random.Range(0, 2) == 0) {
				walksRight = true;
				ResetDeltaX();
			}
			else {
				walksRight = false;
				ResetDeltaX();
			}
		}
	}

	void FixedUpdate() {
		if(walksRight) {
			rb2D.velocity = new Vector2(moveSpeed, rb2D.velocity.y);
		}
		else {
			rb2D.velocity = new Vector2(-1 * moveSpeed, rb2D.velocity.y);
		}
	}

	public void ResetDeltaX() {
		deltaX = (int) transform.position.x;
	}

	void OnCollisionEnter2D(Collision2D col) {
		switch(col.gameObject.tag) {
			case "Player":
				if(!stunned) {
					KickPunching();
					col.gameObject.GetComponent<PlayerHealth>().TakeDamage(5, gameObject.transform.position, 5f);
					//Should also knock the player away
					walksRight = false;
					ResetDeltaX();
				}
				break;

			case "PickupableItem":
				if(col.gameObject.GetComponent<PickUpableItem>().GetItemType().Equals("Rock")) {
					//Maybe play grunt
					Debug.Log("Threw rock at boss!");
					if(Random.Range(0, 2) == 0)
						Instantiate(Resources.Load("HealthDrop"), col.gameObject.transform.position, Quaternion.identity);
					else
						Instantiate(Resources.Load("EnergyDrop"), col.gameObject.transform.position, Quaternion.identity);
					Destroy(col.gameObject);
					Charge();
				}
				break;

			case "Wall":
				walksRight = true;
				ResetDeltaX();
				break;

			case "SecretTrigger":
				walksRight = false;
				ResetDeltaX();
				break;
		}
	}

	public void Stunned(float time) {
		Debug.Log("Boss is stunned!");
		//Stuns the boss a few seconds and makes it drop a few pick-Ups
		stunned = true;
		moveSpeed = 0;
		for(int i = 0; i < 3; i++) {
			DropSpawner("HealthDrop", transform.position);
			DropSpawner("EnergyDrop", transform.position);
		}
		if(!blued) {
			transform.GetChild(0).GetComponent<Phase2Head>().OpenMouth(3f);
		}
		Invoke("UnStunned", time);
	}

	void UnStunned() {
		Debug.Log("Boss is UnStunned");
		moveSpeed = 3;
		stunned = false;
	}

	public void LaserShot() {
		if(!blued && !stunned)
			//Play some kind of laughing animation
			Charge();
	}

	void Charge() {
		if(blued)
			return;
		if(!walksRight)
			walksRight = true;
		//FinalBoss is charging, play relevant things
		Debug.Log("FinalBoss is charging");
		transform.GetChild(0).GetComponent<Phase2Head>().OpenMouth(3f);
		moveSpeed = movespeed + 5;
		damage += 2;
		Invoke("StopCharge", 1f);
	}

	void StopCharge() {
		Debug.Log("FinalBoss stopped charging");
		//FinalBoss stops charging, play relevant things
		damage -= 2;
		moveSpeed = movespeed;
	}

	void KickPunching() {
		if(!stunned) {
			GetComponent<SpriteRenderer>().sprite = kickPunching;
			Debug.Log("kickpunching");
			this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
			Invoke("FinishKickPunching", 1f);
		}
		moveSpeed = 0;
	}

	void FinishKickPunching() {
		GetComponent<SpriteRenderer>().sprite = normal;
		Debug.Log("finish kickpunching");
		this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
		moveSpeed = movespeed;
	}

	public void TakeDamage(int damage) {
		health -= damage;
		if(health <= 0) {
			Defeated();
		}
	}

	void Defeated() {
		//Now phase 3 starts
		for(int i = 0; i < 5; i++) {
			DropSpawner("HealthDrop", transform.position);
			DropSpawner("EnergyDrop", transform.position);
		}
		Instantiate(Resources.Load("FBP3"), transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	void DropSpawner(string type, Vector3 pos) {
		Vector3 dropLoc = new Vector3(pos.x + Random.Range(-spawnLoc, spawnLoc), pos.y + Random.Range(-spawnLoc, spawnLoc), pos.z);
		Instantiate(Resources.Load(type), dropLoc, Quaternion.identity);
	}
}