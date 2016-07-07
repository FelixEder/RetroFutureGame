using UnityEngine;
using System.Collections;

public class SpecialDoor : MonoBehaviour {
	//The speciality of this door, is given in the editor as indicated by which item is needed to destroy it.
	//Ex. "rock" for doors that can only be destroyed by regular doors.
	public string speciality;
	public int health;
	public Sprite barrier;
	GameObject specialScript, normalScript;

	void Start() {
		normalScript = gameObject.GetComponent<Door> ();
		normalScript.enabled = false;
		specialScript = gameObject.GetComponent<SpecialDoor> ();
		specialScript.enabled = true;
	}
	
	void opened() {
		specialScript.enabled = false;
		normalScript.enabled = true;
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag.equals (speciality) && col.gameObject.GetComponent<Rigidbody2D>.velocity.magnitude >= 3.0f) {
			health--;
			if (health <= 0) {
				opened ();
			}
		}
	}
}