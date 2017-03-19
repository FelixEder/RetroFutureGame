using UnityEngine;
using System.Collections;

public class SmallCritter : MonoBehaviour {
	public float moveSpeed, knockForce, followDistance;
	float activeMoveSpeed,initialFreezeTime;
	bool isMirrored = false, invulnerable;
	public int health = 2, damage = 1, invulnerabilityTime;
	public Material glitchMaterial;
	Rigidbody2D rb2D;
	GameObject player;
	float startPos;

	void Start() {
		player = GameObject.Find ("Char");
		startPos = transform.position.x;

		initialFreezeTime = gameObject.GetComponent<SpawnProperties> ().initialFreezeTime;
		rb2D = GetComponent<Rigidbody2D> ();
		if (initialFreezeTime > 0)
			Invoke ("InitializeMoveSpeed", initialFreezeTime);
	}

	void FixedUpdate() {
		if (activeMoveSpeed > 0 && Mathf.Abs (player.transform.position.x - transform.position.x) < followDistance && Mathf.Abs (player.transform.position.x - transform.position.x) > 1) {
			rb2D.velocity = new Vector2 (activeMoveSpeed * Mathf.Sign (player.transform.position.x - transform.position.x), rb2D.velocity.y);
			if (isMirrored) {
//				rb2D.velocity = new Vector2 (activeMoveSpeed, rb2D.velocity.y);
			} else {
//				rb2D.velocity = new Vector2 (-1 * activeMoveSpeed, rb2D.velocity.y);
			}
		}
		else if (Mathf.Abs (startPos - transform.position.x) > 1 && Mathf.Abs (player.transform.position.x - transform.position.x) > 1)
			rb2D.velocity = new Vector2 (activeMoveSpeed * Mathf.Sign (startPos - transform.position.x), rb2D.velocity.y);
	}

	void OnBecameVisible() {
		if (initialFreezeTime == 0)
			InitializeMoveSpeed ();
	}

	void OnCollisionEnter2D(Collision2D col) {
		switch(col.gameObject.tag) {

		case "Char":
			if (!col.gameObject.GetComponent<CharStomp> ().groundStomping) {
				col.gameObject.GetComponent<CharHealth> ().TakeDamage (damage, gameObject, knockForce);
			}
			GetMirrored();
			break;

		case "SmallCritter":
		case "JumpingCritter":
		case "HardEnemy":
		case "BigEyeGuy":
		case "CrawlerCritter":
		case "ShellMan":
		case "Wall":
		case "Door":
		case "Barrier":
			GetMirrored ();
			break;

		case "PickupableItem":
			switch (col.gameObject.GetComponent<PickUpableItem> ().GetItemType ()) {
			case "Rock":
				if (col.gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude >= 3.0f) {
					TakeDamage (col.gameObject.GetComponent<PickUpableItem> ().damage);
				}
				break;

			case "Branch":
				if (col.gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude >= 2.0f) {
					TakeDamage (col.gameObject.GetComponent<PickUpableItem> ().damage);
					col.gameObject.GetComponent<PickUpableItem> ().Break ();
				}
				break;
			}
			GetMirrored ();
			break;
		}
	}

	/**
	 * Mirrors the enemy and therefor makes it change direction.
	 */
	void GetMirrored() {
		if(!isMirrored) {
			transform.rotation = Quaternion.Euler(0, 180, 0);
			isMirrored = true;
		}
		else {
			transform.rotation = Quaternion.Euler(0, 0, 0);
			isMirrored = false;
		}
	}

	/**
	 * Method called when enemy is hit by the player
	 */
	public void TakeDamage(int damage) {
		if (!invulnerable) {
			//Play a sound and animation.
			health -= damage;
			invulnerable = true;
			Invoke ("SetVulnerable", invulnerabilityTime);
			if (health <= 0) {
				//Enemy is dead, play animation and sound.
				int ranNumb = Random.Range (0, 60);
				if (ranNumb < 20) {
					Instantiate (Resources.Load ("HealthDrop"), transform.position, Quaternion.identity);
				} else if (ranNumb < 40) {
					Instantiate (Resources.Load ("EnergyDrop"), transform.position, Quaternion.identity);
				}
				Destroy (this.gameObject);
			}
			GetMirrored ();
		}
	}

	void SetVulnerable() {
		invulnerable = false;
	}

	void Die() {
		int ranNumb = Random.Range(0, 80);
		if (ranNumb < 20) {
			Instantiate (Resources.Load ("HealthDrop"), transform.position, Quaternion.identity);
		} else if (ranNumb < 40) {
			Instantiate (Resources.Load ("EnergyDrop"), transform.position, Quaternion.identity);
		}
		Destroy (this.gameObject);
	}

	void InitializeMoveSpeed() {
		activeMoveSpeed = moveSpeed;
	}
}