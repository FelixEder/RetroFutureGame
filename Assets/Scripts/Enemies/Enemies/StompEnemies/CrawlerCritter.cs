using UnityEngine;
using System.Collections;

public class CrawlerCritter : MonoBehaviour {
	public Sprite deCrawled;
	public float moveSpeed, knockForce;
	public bool isMirrored = false, deShelled;
	Rigidbody2D rb2D;
	public int health = 2, damage = 2;

	void Start() {
		rb2D = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate() {
		if (isMirrored) {
			rb2D.velocity = new Vector2 (-1 * moveSpeed, rb2D.velocity.y);
		} else {
			rb2D.velocity = new Vector2 (moveSpeed, rb2D.velocity.y);
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		switch(col.gameObject.tag) {
			case "Char":
			if (!col.gameObject.GetComponent<CharStomp> ().groundStomping) {
				col.gameObject.GetComponent<CharHealth> ().TakeDamage (damage);
			}
			col.gameObject.GetComponent<Knockback> ().Knock (this.gameObject, knockForce);
			break;

			case "SoftEnemy" :
			GetMirrored();
			break;

			case "HardEnemy" :
			GetMirrored();
			break;

			case "StompEnemy" :
			GetMirrored();
			break;

			case "Wall" :
			GetMirrored();
			break;

			case "Door" :
			GetMirrored();
			break;

			case "Rock":
			if (col.gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude >= 3.0f) {
				GetHurt (col.gameObject.GetComponent<PickUpableItem> ().damage);
			}
			GetMirrored ();
			break;

			case "Branch":
			if (col.gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude >= 2.0f) {
				GetHurt (col.gameObject.GetComponent<PickUpableItem> ().damage);
			}
			GetMirrored ();
			break;

			case "Barrier":
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

	void BreakShell() {
		//Change sprite into the DeShelled one and play relevant things.
		GetComponent<SpriteRenderer>().sprite = deCrawled;
		deShelled = true;
		damage = 5;
		moveSpeed += 3;
		knockForce += 3;
	}

	/**
	 * Method called when enemy is hit by the player
	 */
	public void GetHurt(int damage) {
		//Play a sound and animation.
		health -= damage;
		if (health == 1) {
			BreakShell ();
		}
		else if (health <= 0) {
			//Enemy is dead, play animation and sound.
			int ranNumb = Random.Range(0, 60);
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