using UnityEngine;
using System.Collections;

public class SpecialDoor : MonoBehaviour {
	//The speciality of this door, is given in the editor as indicated by which item is needed to destroy it.
	//Ex. "rock" for doors that can only be destroyed by regular doors.
	public string speciality;
	public int health;
	public Sprite barrier;
	SpecialDoor specialScript;
	Door normalScript;

	void Start() {
		normalScript = gameObject.GetComponent<Door> ();
		normalScript.enabled = false;
		specialScript = gameObject.GetComponent<SpecialDoor> ();
		specialScript.enabled = true;
	}

	/**
	 * The barrier of the door has been broken, it now changes script to a 
	 */
	void opened() {
		gameObject.tag = "door";
		specialScript.enabled = false;
		normalScript.enabled = true;
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag.Equals(speciality) && col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude >= 3.0f) {
			getHurt ();
		}
	}

	public void getHurt() {
		health--;
		if (health <= 0) {
			opened ();
		}
	}

	public string getSpecial() {
		return speciality;
	}
}