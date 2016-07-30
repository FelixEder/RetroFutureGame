using UnityEngine;
using System.Collections;

public class SmallFry : MonoBehaviour {
	CharStatus charStatus;
	CharInventory charInventory;
//	public Sprite normal, smallFry;

	void Start() {
		charStatus = transform.parent.GetComponent<CharStatus> ();
		charInventory = transform.parent.GetComponent<CharInventory> ();
	}
	
	void Update() {
		if (Input.GetButtonDown ("SmallButton")) {
			if (charStatus.isSmall) {
				GrowBig ();
			} else {
				GrowSmall ();
			}
		}
	}

	void GrowSmall() {
		transform.parent.GetComponent<PolygonCollider2D> ().enabled = false;
		transform.parent.GetComponent<CircleCollider2D> ().enabled = true;
		charStatus.isSmall = true; 
		if(charInventory.isHoldingItem())
			charInventory.setHoldingItem (null);
	}

	void GrowBig() {
		transform.parent.GetComponent<PolygonCollider2D> ().enabled = true;
		transform.parent.GetComponent<CircleCollider2D> ().enabled = false;
		charStatus.isSmall = false;
	}
}