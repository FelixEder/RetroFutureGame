using UnityEngine;
using System.Collections;

public class BigBadBird : MonoBehaviour {
	public Sprite regular, spitting, winging;
	public float moveSpeed, knockForce;
	public bool isMirrored, isSpitting;
	public int health = 6, damage;
	//Should it really use a rigidBody?
	Rigidbody2D rb2D;
	int spitChance = 100;

	void Start() {
		rb2D = GetComponent<Rigidbody2D> ();
		GetComponent<SpriteRenderer> ().sprite = regular;
		//Set the start values here
		//Also set the standard sprite.
	}

	void Update() {
		if(health <= 0)
			Defeated ();
		if (Random.Range (0, spitChance) < 2 && !isSpitting)
			SpitAttack ();
	}

	void FixedUpdate() {
		int vertDir = 0;
			//Random.Range (-20, 20);
		if (isMirrored) {
			rb2D.velocity = new Vector2 (-1 * moveSpeed, vertDir);
		} else {
			rb2D.velocity = new Vector2 (moveSpeed, vertDir);
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		switch (col.gameObject.tag) {
		case "Wall":
			GetMirrored ();
			WingAttack ();
			break;

		case "Platform":
			GetMirrored ();
			WingAttack ();
			break;

		case "Char":
			WingAttack ();
			break;

		case "PickupableItem":
			switch (col.gameObject.GetComponent<PickUpableItem> ().GetItemType ()) {

			case "Branch":
				WingAttack ();
				break;

			case "Rock":
				SpitAttack ();
				break;
			}
			break;
		}
	}
	
	//Call this method when the boss gets mad and changes some things.
	void GetAngry() {
		//PLay fitting animation and sound
		//Change color or entire sprite
		moveSpeed += 3;
		knockForce += 3;
		spitChance = 50;
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

	void WingAttack() {
		//Make some swooshing animation with feather
		//If player is hit by the hitbox, he will get hurt
		//Will probably activate some child-trigger that roughly encapsulates the wing being slapped.
		this.gameObject.transform.GetChild (1).gameObject.SetActive (true);
		GetComponent<SpriteRenderer> ().sprite = winging;
		//Is there a better way to disable a child after a certain time than a Invoke-call to an unneccecary method?
		Invoke ("FinishWingAttack", 1f);
	}

	void FinishWingAttack() {
		GetComponent<SpriteRenderer> ().sprite = regular;
		this.gameObject.transform.GetChild (1).gameObject.SetActive (false);
	}

	void SpitAttack() {
		isSpitting = true;
		this.gameObject.transform.GetChild (0).gameObject.SetActive (true);
		GetComponent<SpriteRenderer> ().sprite = spitting;
		//Is there a better way to disable a child after a certain time than a Invoke-call to an unneccecary method?
		Invoke ("FinishSpitAttack", 1f);
	}

	void FinishSpitAttack() {
		GetComponent<SpriteRenderer> ().sprite = regular;
		this.gameObject.transform.GetChild (0).gameObject.SetActive (false);
		isSpitting = false;
		//Play animation that closes mouth
	}

	public void GetHurt() {
		health--;
		if (health == 3)
			GetAngry ();
		//PLay fitting animation and sound
		//Should flash in some color in order to show that the player hurt it
	}

	void Defeated() {
		//PLay fitting animation and sound
		Destroy(this.gameObject.transform.parent.gameObject);
		for (int i = 0; i < 5; i++) {
			Instantiate (Resources.Load ("HealthDrop"), transform.position, Quaternion.identity);
			Instantiate (Resources.Load ("EnergyDrop"), transform.position, Quaternion.identity);
		}
	}
}