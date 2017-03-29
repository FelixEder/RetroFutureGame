using UnityEngine;
using System.Collections;

public class SmallCritter : MonoBehaviour {
	public float moveSpeed, knockForce, followDistance, followDistanceUP, followDistanceDown, wanderDistance, invulnerabilityTime;
	float activeMoveSpeed, initialFreezeTime;
	bool invulnerable, touchingSomething, hasSeenPlayer, wanderLeft;
	public int health = 2, damage = 1;
	public Material glitchMaterial;
	Rigidbody2D rb2D;
	GameObject player;
	float startPos;

	void Start() {
		player = GameObject.Find ("Char");
		startPos = transform.position.x;
		if (Random.Range (0, 2) == 0)
			wanderLeft = true;
		initialFreezeTime = GetComponent<SpawnProperties> ().initialFreezeTime;
		wanderDistance = GetComponent<SpawnProperties> ().wanderDistance;
		rb2D = GetComponent<Rigidbody2D> ();
		if (initialFreezeTime > 0)
			Invoke ("InitializeMoveSpeed", initialFreezeTime);
	}

	void FixedUpdate() {
		calculateMirror ();
		if (activeMoveSpeed > 0 && touchingSomething) {
			if (PlayerInRange () && DistanceFromPlayer() > 1) { //follow player if player is in range
				rb2D.velocity = new Vector2 (activeMoveSpeed * Mathf.Sign (player.transform.position.x - transform.position.x), rb2D.velocity.y);
			}
			else if (!PlayerInRange()) { //wander within start pos if outside followdistance.
				if (transform.position.x - startPos > wanderDistance)
					wanderLeft = true;
				else if (startPos - transform.position.x > wanderDistance)
					wanderLeft = false;
				
				if (wanderLeft)
					rb2D.velocity = new Vector2 (activeMoveSpeed * -0.7f, rb2D.velocity.y);
				else
					rb2D.velocity = new Vector2 (activeMoveSpeed * 0.7f, rb2D.velocity.y);
			}
		}
	}

	void OnBecameVisible() {
		if (initialFreezeTime == 0)
			InitializeMoveSpeed ();
	}

	void OnCollisionStay2D() {
		touchingSomething = true;
	}

	void OnCollisionExit2D() {
		touchingSomething = false;
	}

	void OnCollisionEnter2D(Collision2D col) {
		switch(col.gameObject.tag) {

		case "Char":
			if (!col.gameObject.GetComponent<CharStomp> ().groundStomping) {
				col.gameObject.GetComponent<CharHealth> ().TakeDamage (damage, gameObject, knockForce);
			}
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
			wanderLeft = !wanderLeft;
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
			break;
		}
	}

	/**
	 * Mirrors the enemy and therefor makes it change direction.
	 */
	void calculateMirror() {
		if (!invulnerable) {
			if (rb2D.velocity.x > 1) {
				transform.rotation = Quaternion.Euler (0, 180, 0);
				GetComponent<Animator> ().enabled = true;
			} else if (rb2D.velocity.x < -1) {
				transform.rotation = Quaternion.Euler (0, 0, 0);
				GetComponent<Animator> ().enabled = true;
			} else {
				GetComponent<Animator> ().enabled = false;
			}
		}
	}

	float DistanceFromPlayer() {
		return Mathf.Abs (player.transform.position.x - transform.position.x);
	}

	bool PlayerInRange() {
		Vector2 pPos = player.transform.position, pos = transform.position;
		if (Mathf.Abs (pPos.x - pos.x) < followDistance//player within follow distance in X;
			&& pos.y - pPos.y < followDistanceDown//player less than 2u below;
			&& pPos.y - pos.y < followDistanceUP)//player less than 6u above;
			return true;
		else
			return false;
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
			if (health <= 0)
				StartCoroutine(Die ());
		}
	}

	void SetVulnerable() {
		invulnerable = false;
	}

	public void Knockback(GameObject attacker, float force) {
		if (!invulnerable) {
			activeMoveSpeed = 0;
			if (transform.position.x < attacker.transform.position.x)
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (-force, 2);
			else
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (force, 2);
			Invoke ("InitializeMoveSpeed", invulnerabilityTime);
		}
	}

	IEnumerator Die() {
		GetComponent<SpriteRenderer> ().material = glitchMaterial;
		yield return new WaitForSeconds (0.2f);
		int ranNumb = Random.Range(0, 60);
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