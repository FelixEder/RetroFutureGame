using UnityEngine;
using System.Collections;

public class Electrohaz : MonoBehaviour {
	bool isActive;
	public Sprite inactive, charging, active;
	SpriteRenderer sr;
	public int damage = 99;

	void Start() {
		sr = GetComponent<SpriteRenderer>();
		sr.sprite = inactive;
		InvokeRepeating("EspisMetod", 0, 2);
		Debug.Log (sr.sprite + " has started");
	}

	void EspisMetod() {
		Debug.Log (sr.sprite + " " + inactive + charging + active);
		if(sr.sprite == inactive) {
 			sr.sprite = charging;

 		}
		else if(sr.sprite == charging) {
 			sr.sprite = active;
 			isActive = true;
 		}
		else if(sr.sprite == active) {
			sr.sprite = inactive;
			isActive = false;
		}
	}
	void OnTriggerStay2D(Collider2D col) {
		switch(col.gameObject.tag) {

		case "Char":
			if (isActive) {
				col.gameObject.GetComponent<CharHealth> ().TakeDamage (damage);
				Debug.Log ("player is not happy");
			}
			break;

		case "SmallCritter" :
		case "JumpingCritter":
		case "HardEnemy" :
		case "BigEyeGuy" :
		case "CrawlerCritter":
		case "ShellMan":
		case "Wall" :
		case "Door" :
		case "PickupableItem":
			break;

		}
	}
}