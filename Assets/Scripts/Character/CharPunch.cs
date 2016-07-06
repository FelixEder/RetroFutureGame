using UnityEngine;
using System.Collections;

public class CharPunch : MonoBehaviour {
	
	void OnTriggerStay2D(Collider2D victim) {
		if(Input.GetKey(KeyCode.K)) {
			//Here an animation and soundFX will be played

			switch(victim.gameObject.tag) {

			case "door" :
				victim.gameObject.GetComponent<Door>().setInvisible ();
				break;

			case "softEnemy":
				victim.gameObject.GetComponent<SmallCritter>.getHurt ();
				break;
			}
		}
	}
}
