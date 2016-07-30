using UnityEngine;
using System.Collections;

public class Barriers : MonoBehaviour {
	//The speciality of this door, is given in the editor as indicated by which item is needed to destroy it.
	//Ex. "rock" for doors that can only be destroyed by regular doors.
	public string barrierType;
	public int health;
	public Sprite sprite;

	/**
	 * The barrier of the door has been broken, it is now destroyed.
	 */
	void Broken() {
		//Play animation and 
		Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag.Equals(barrierType) && col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude >= 2.0f) {
			TakeDamage ();
		}
	}

	public void TakeDamage() {
		health--;
		if (health <= 0) {
			Broken();
		}
	}

	/**Returns the barrier type.*/
	public string GetBarrierType() {
		return barrierType;
	}
}