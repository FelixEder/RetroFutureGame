using UnityEngine;
using System.Collections;

public class Barriers : MonoBehaviour {
	//The speciality of this door, is given in the editor as indicated by which item is needed to destroy it.
	//Ex. "rock" for doors that can only be destroyed by regular doors.
	public string speciality;
	public int health;
	public Sprite barrier;

	/**
	 * The barrier of the door has been broken, it is now destroyed.
	 */
	void Broken() {
		//Play animation and 
		Destroy(gameObject);
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag.Equals(speciality) && col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude >= 2.0f) {
			getHurt ();
		}
	}

	public void getHurt() {
		health--;
		if (health <= 0) {
			Broken();
		}
	}

	public string getSpecial() {
		return speciality;
	}
}