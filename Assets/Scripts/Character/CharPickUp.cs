using UnityEngine;
using System.Collections;

public class CharPickUp : MonoBehaviour {
	CharInventory charInventory;

	void Start() {
		charInventory = GetComponent<CharInventory> ();
	}

	void FixedUpdate() {
		if (Input.GetKey (KeyCode.L) && charInventory.isHoldingItem ()) {
			transform.parent = null;
			charInventory.setHoldingItem (null);
		}
	}


	void OnCollisionStay2D(Collision2D col) {
		switch(col.gameObject.tag) {

		case "rock" :
			if(Input.GetKey(KeyCode.L) && !charInventory.isHoldingItem()) {
				charInventory.setHoldingItem(col.gameObject); 
				col.gameObject.transform.parent = transform;
			}
			break;
		}
	}
}
