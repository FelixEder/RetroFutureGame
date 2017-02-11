using UnityEngine;
using System.Collections;

public class HardCritter : MonoBehaviour {
	public float moveSpeed, knockForce;
	bool isMirrored = false, rushing;
	Rigidbody2D rb2D;
	public int health = 5, damage = 3;

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
			if (col.gameObject.GetComponent<CharStatus> ().IsMegaPunching())
				GetMirrored ();
			else {
				col.gameObject.GetComponent<CharHealth> ().TakeDamage (damage, gameObject, knockForce);
				Rush ();
			}
			GetMirrored();
			break;

		case "SmallCritter":
		case "JumpingCritter":
		case "HardEnemy":
		case "BigEyeGuy":
		case "CrawlerCritter":
		case "ShellMan":
		case "Barrier":
			GetMirrored ();
			break;

		case "Wall":
		case "Door":
			GetMirrored ();
			Rush ();
			break;

		case "PickupableItem":
			if (col.gameObject.GetComponent<PickUpableItem> ().GetItemType () == "Rock") {
				if (col.gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude >= 3.0f) {
					GetHurt (col.gameObject.GetComponent<PickUpableItem> ().damage);
					break;
				}
			}
			GetMirrored ();
			Rush ();
			break;
		}
	}

	public void Rush() {
		//Enemy is rushing, play relevant things
		if (!rushing) {
			Debug.Log ("Enemy is rushing");
			damage += 2;
			moveSpeed += 5;
			rushing = true;
			Invoke ("StopRush", 1f);
		}
	}

	void StopRush() {
		Debug.Log ("Enemy stopped rushing");
		//Enemy stops rushing, play relevant things
		damage -= 2;
		moveSpeed -= 5;
		rushing = false;
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
	public void GetHurt(int damage) {
		//Play a sound and animation.
		health -= damage;
		if (health <= 0) {
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