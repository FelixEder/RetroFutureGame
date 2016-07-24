using UnityEngine;
using System.Collections;

public class ShellMan : MonoBehaviour {
	public float moveSpeed, knockForce, jumpSpeed;
	bool isMirrored = false, deShelled;
	Rigidbody2D rb2D;
	public int health = 6, damage = 3;

	void Start() {
		rb2D = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate() {
		if (isMirrored) {
			rb2D.velocity = new Vector2 (moveSpeed, rb2D.velocity.y);
		} else {
			rb2D.velocity = new Vector2 (-1 * moveSpeed, rb2D.velocity.y);
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		switch(col.gameObject.tag) {

		case "char":
			if (col.gameObject.GetComponent<CharStomp> ().groundStomping && !deShelled) {
				GetHurt (1);
			} else if (col.gameObject.GetComponent<CharStomp> ().groundStomping && deShelled) {
				GetHurt (2);
			}
			else {
				col.gameObject.GetComponent<CharHealth> ().TakeDamage (damage);
				col.gameObject.GetComponent<Knockback> ().Knock (this.gameObject, knockForce);
			}
			break;

			case "softEnemy" :
			GetMirrored();
			break;

			case "hardEnemy" :
			GetMirrored();
			break;

			case "stompEnemy" :
			GetMirrored();
			break;

			case "wall" :
			GetMirrored();
			break;

			case "door" :
			GetMirrored();
			break;

			case "rock":
			if (col.gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude >= 3.0f && deShelled) {
				GetHurt (col.gameObject.GetComponent<PickUpableItem> ().damage);
			}
			GetMirrored ();
			break;

			case "branch":
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
		deShelled = true;
		damage = 5;
		moveSpeed += 3;
		knockForce += 3;
		jumpSpeed += 3;
	}

	/**
	 * Method called when enemy is hit by the player
	 */
	public void GetHurt(int damage) {
		//Play a sound and animation.
		health -= damage;
		if (health == 3) {
			BreakShell ();
		} else if (health <= 0) {
			//Enemy is dead, play animation and sound.
			for(int i = 0; i < 2; i++) {
				Instantiate (Resources.Load ("HealthDrop"), transform.position, Quaternion.identity);
				Instantiate (Resources.Load ("EnergyDrop"), transform.position, Quaternion.identity);
			}
			Destroy (this.gameObject);
		}
		GetMirrored ();
	}

	public void Jump () {
		if (Random.Range (0, 100) < 5) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, jumpSpeed);
		}
	}
}