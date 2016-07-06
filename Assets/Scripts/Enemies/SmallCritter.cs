using UnityEngine;
using System.Collections;

public class SmallCritter : MonoBehaviour {
	int health = 2;
	public float moveSpeed;
	bool isMirrored = false;

	void FixedUpdate() {
		transform.Translate(new Vector3(moveSpeed * -1, 0, 0));
	}

	void OnCollisionEnter2D(Collision2D col) {

		switch(col.gameObject.tag) {

		case "char":
			//Here, the player will be hurt
			getMirrored();
			break;

		case "softEnemy":
			getMirrored();
			break;
		
		case "wall" :
			getMirrored();
			break;
		
		case "door" :
			getMirrored();
			break;

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
	public void getHurt() {
		//Play a sound and animation.
		health--;
		if(health == 0) {
			//Enemy is dead, play animation and sound.
			Destroy(this.gameObject);
		}
		getMirrored();
	}
}
