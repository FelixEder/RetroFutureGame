using UnityEngine;
using System.Collections;

public class SmallCritter : MonoBehaviour {
	public float moveSpeed, knockForce;
	bool isMirrored = false;
	Rigidbody2D rb2D;
	int health = 2;

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

		case "char" :
			//Here, the player will be hurt
			col.gameObject.GetComponent<Knockback>().Knock(this.gameObject, knockForce);
			break;

		case "softEnemy" :
			getMirrored();
			break;
		
		case "wall" :
			getMirrored();
			break;
		
		case "door" :
			getMirrored();
			break;

		case "rock":
			if (col.gameObject.GetComponent<Rigidbody2D>.velocity.magnitude > 3.0f) {
				getHurt (2);
			}
			getMirrored ();
		}
	}

	/**
	 * Mirrors the enemy and therefor makes it change direction.
	 */
	void getMirrored() {
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
	public void getHurt(int damage) {
		//Play a sound and animation.
		health =- damage;
		if(health <= 0) {
			//Enemy is dead, play animation and sound.
			Destroy(this.gameObject);
		}
		getMirrored();
	}
}
