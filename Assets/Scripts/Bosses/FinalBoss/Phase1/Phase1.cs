﻿using UnityEngine;
using System.Collections;

public class Phase1 : MonoBehaviour {
	public float knockForce;
	public int health = 3, spawnLoc = 2;
	public Sprite normal, kickPunching, fallen;
	bool stunned;
	Rigidbody2D rb2D;

	// Use this for initialization
	void Start() {
		rb2D = GetComponent<Rigidbody2D>();
		InvokeRepeating("KickPunching", 1f, 5f);
	}

	void OnCollisionEnter2D(Collision2D col) {
		switch(col.gameObject.tag) {
			case "Player":
				if(!stunned) {
					KickPunching();
					col.gameObject.GetComponent<PlayerHealth>().TakeDamage(5, gameObject.transform.position, 5f);
				}
				else if(rb2D.velocity.y > 5f)
					col.gameObject.GetComponent<PlayerHealth>().TakeDamage(5, gameObject.transform.position, 5f);
				break;

			case "PickupableItem":
				//Maybe play grunt
				Debug.Log("Threw rock at boss!");
				Vector3 rockLoc = col.gameObject.transform.position;
				if(Random.Range(0, 2) == 0)
					Instantiate(Resources.Load("HealthDrop"), rockLoc, Quaternion.identity);
				else
					Instantiate(Resources.Load("EnergyDrop"), rockLoc, Quaternion.identity);
				Destroy(col.gameObject);
				KickPunching();
				break;
		}
	}

	public void Stunned(float time) {
		Debug.Log("Boss is stunned!");
		//Stuns the boss a few seconds and makes it drop a few pick-Ups
		stunned = true;
		for(int i = 0; i < 3; i++) {
			DropSpawner("HealthDrop", transform.position);
			DropSpawner("EnergyDrop", transform.position);
		}
		Invoke("UnStunned", time);
	}

	void UnStunned() {
		Debug.Log("Boss is UnStunned");
		stunned = false;
	}

	public void Fall() {
		//Makes the boss fall down
		GetComponent<PolygonCollider2D>().tag = "FinalBossWeakSpot";
		transform.GetChild(0).gameObject.GetComponent<EdgeCollider2D>().enabled = false;
		GetComponent<SpriteRenderer>().sprite = fallen;
		transform.position = new Vector2(92.9f, -88.2f);
		transform.rotation = Quaternion.Euler(0, 0, -96.89f);
		Stunned(7f);
		Invoke("Rise", 7f);
	}

	void Rise() {
		GetComponent<SpriteRenderer>().sprite = normal;
		transform.position = new Vector2(89.3f, -90.67f);
		transform.rotation = Quaternion.Euler(0, 0, 0);
		GetComponent<PolygonCollider2D>().tag = "FinalBossArmor";
		transform.GetChild(0).gameObject.GetComponent<EdgeCollider2D>().enabled = true;
		KickPunching();
	}

	void KickPunching() {
		if(!stunned) {
			GetComponent<SpriteRenderer>().sprite = kickPunching;
			this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
			Invoke("FinishKickPunching", 1f);
		}
	}

	void FinishKickPunching() {
		GetComponent<SpriteRenderer>().sprite = normal;
		this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
	}

	public void TakeDamage(int damage) {
		health -= damage;
		//Would be nice if the armour could look a bit more broken after each punch.
		if(health <= 0) {
			Defeated();
		}
	}

	void Defeated() {
		//Now phase 2 starts
		for(int i = 0; i < 5; i++) {
			DropSpawner("HealthDrop", transform.position);
			DropSpawner("EnergyDrop", transform.position);
		}
		Instantiate(Resources.Load("FBP2"), new Vector3(87.76018f, -91.5612f, 0f), Quaternion.Euler(0, 0, 0));
		Destroy(gameObject);
	}

	void DropSpawner(string type, Vector3 pos) {
		Vector3 dropLoc = new Vector3(pos.x + Random.Range(-spawnLoc, spawnLoc), pos.y + Random.Range(-spawnLoc, spawnLoc), pos.z);
		Instantiate(Resources.Load(type), dropLoc, Quaternion.identity);
	}
}