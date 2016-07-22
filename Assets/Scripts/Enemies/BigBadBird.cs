using UnityEngine;
using System.Collections;

public class BigBadBird : MonoBehaviour {
	public float moveSpeed, knockForce;
	bool isMirrored, isSpitting;
	public int health = 6, damage;
	//Should it really use a rigidBody?
	Rigidbody2D rb2D;
	int spitChance = 10;

	void Start() {
		rb2D = GetComponent<Rigidbody2D> ();
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
		int vertDir = Random.Range (-5, 6);
		if (isMirrored) {
			rb2D.velocity = new Vector2 (moveSpeed, vertDir);
		} else {
			rb2D.velocity = new Vector2 (-1 * moveSpeed, vertDir);
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		switch (col.gameObject.tag) {
			case "wall":
				GetMirrored ();
				WingAttack ();
				break;

			case "char":
				WingAttack ();
				break;

			case "branch":
				WingAttack ();
				break;

			case "rock":
				SpitAttack ();
				break;
		}
	}
	
	//Call this method when the boss gets mad and changes some things.
	void GetAngry() {
		//PLay fitting animation and sound
		//Change color or entire sprite
		moveSpeed += 3;
		knockForce += 3;
		spitChance = 5;
	}

	void GetMirrored() {
		if (!isMirrored) {
			transform.rotation = Quaternion.Euler (0, 180, 0);
			status.isMirrored = true;
		} else {
			transform.rotation = Quaternion.Euler (0, 0, 0);
			status.isMirrored = false;
		}
	}

	void WingAttack() {
		//Make some swooshing animation with feather
		//If player is hit by the hitbox, he will get hurt
		//Will probably activate some child-trigger that roughly encapsulates the wing being slapped.
		this.gameObject.transform.GetChild (1).gameObject.SetActive (true);
		//Is there a better way to disable a child after a certain time than a Invoke-call to an unneccecary method?
		Invoke ("FinishWingAttack", 1f);
	}

	void FinishWingAttack() {
		this.gameObject.transform.GetChild (1).gameObject.SetActive (false);
	}

	void SpitAttack() {
		isSpitting = true;
		this.gameObject.transform.GetChild (0).gameObject.SetActive (true);
		//Is there a better way to disable a child after a certain time than a Invoke-call to an unneccecary method?
		Invoke ("FinishSpitAttack", 1f);
	}

	void FinishSpitAttack() {
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
		for(int i = 0; i < 5; i++) {
			Instantiate (Resources.Load ("HealthDrop"), transform.position, Quaternion.identity);
			Instantiate (Resources.Load ("EnergyDrop"), transform.position, Quaternion.identity);
	}
}