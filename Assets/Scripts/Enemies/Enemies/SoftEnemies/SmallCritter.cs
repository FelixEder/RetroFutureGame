using UnityEngine;
using System.Collections;

public class SmallCritter : MonoBehaviour {
	public float moveSpeed, knockForce;
	float activeMoveSpeed = 0;
	public bool canMoveInitially;
	bool isMirrored = false;
	public int health = 2, damage = 1;
	public Material glitchMaterial;
	Rigidbody2D rb2D;

	void Start() {
		rb2D = GetComponent<Rigidbody2D> ();
		if(canMoveInitially)
			activeMoveSpeed = moveSpeed;
	}

	void FixedUpdate() {
		if (isMirrored) {
			rb2D.velocity = new Vector2 (activeMoveSpeed, rb2D.velocity.y);
		} else {
			rb2D.velocity = new Vector2 (-1 * activeMoveSpeed, rb2D.velocity.y);
		}
	}

	void OnBecameVisible() {
		activeMoveSpeed = moveSpeed;
	}

	void OnCollisionEnter2D(Collision2D col) {

		switch(col.gameObject.tag) {

			case "Char":
				if (!col.gameObject.GetComponent<CharStomp> ().groundStomping) {
					col.gameObject.GetComponent<CharHealth> ().TakeDamage (damage);
					col.gameObject.GetComponent<Knockback> ().Knock (this.gameObject, knockForce);
				}
				break;

			case "SoftEnemy" :
				GetMirrored();
				break;

			case "HardEnemy" :
				GetMirrored();
				break;

			case "Wall" :
				GetMirrored();
				break;
			
			case "Door" :
				GetMirrored();
				break;

			case "Rock" :
				if (col.gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude >= 2.0f) {
					TakeDamage (col.gameObject.GetComponent<PickUpableItem> ().damage);
				}
				GetMirrored ();
				break;

			case "Branch" :
				if (col.gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude >= 2.0f) {
					TakeDamage (col.gameObject.GetComponent<PickUpableItem> ().damage);
				}
				GetMirrored ();
				break;

			case "Barrier" :
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
		//Play a sound and animation.
		health -= damage;
		if (health <= 0) {
			//Enemy is dead, play animation and sound.
			gameObject.GetComponent<SpriteRenderer>().material = glitchMaterial;
			rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
			gameObject.GetComponent<Collider2D> ().enabled = false;
			Invoke ("Die", 0.5f);
		}
		GetMirrored ();
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
}