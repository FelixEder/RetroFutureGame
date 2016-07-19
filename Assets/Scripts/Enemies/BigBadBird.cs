using UnityEngine;
using System.Collections;

public class BigBadBird : MonoBehaviour {
	bool isMirrored;
	public int health = 6;

	void Start() {
		//Set the start values here
		//Also set the standard sprite.
	}

	void Update() {
		if (health == 3)
			GetAngry ();
		else if(health <= 0)
			Defeated ();
	}

	void FixedUpdate() {
	}

	void OnCollisionEnter2D(Collision2D col) {
		switch (col.gameObject.tag) {
			case "wall":
				GetMirrored ();
				break;

			case "char":
				WingAttack ();
				break;
		}
	}

	//Could be used for when the player shoots the mouth, but that could also be solved 
	//by using the laser-Hit on the Player-lasergun
	void OnTriggerEnter2D(Collider2D col) {
	}
	
	//Call this method when the boss get's mad and changes some things.
	void GetAngry() {
		//PLay fitting animation and sound
		//Change color or entire sprite
	}

	void GetMirrored() {
	}

	void WingAttack() {
		//Make some swooshing animation with feather
		//If player is hit by the hitbox, he will get hurt
	}

	void SpitAttack() {
		//Activate a child-trigger with an attached Prefab-Spawner
	}

	public void GetHurt() {
		//PLay fitting animation and sound
		//Should flash in some color in order to show that the player hurt it
	}

	void Defeated() {
		//PLay fitting animation and sound
		Destroy (gameObject);
	}
}