using UnityEngine;
using System.Collections;

public class Electrohaz : MonoBehaviour {
	bool isActive;
	public Sprite inactive, charging, active;
	SpriteRenderer sr;
	public int damage = 99;

	void start() {
		sr = GetComponent<SpriteRenderer>();
		sr = inactive;
		InvokeRepeating("EspisMetod", 0, 2);
	}

	void EspisMetod() {

		SpriteRenderer  sr;
 		if(sr.sprite.equals(inactive) {
 			sr.sprite = charging;

 			}
 		else if(sr.sprite.equals(charging) {
 			sr.sprite = active;
 			isActive = true;
 			}
		else if(sr.sprite.equals(active) {
			sr.sprite = inactive;
			isActive = false;
			}
	}


	void OnTriggerEnter2D(Collision2D col) {
		switch(col.gameObject.tag) {

		case "Char":
				if(isActive) 
					col.gameObject.GetComponent<CharHealth> ().TakeDamage (damage);
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