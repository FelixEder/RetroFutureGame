using UnityEngine;
using System.Collections;

public class CharPunch : MonoBehaviour {
	
	void OnTriggerStay2D(Collider2D victim) {
		if(Input.GetKey(KeyCode.K)) {
			//Here an animation and music will be played
			switch(victim.gameObject.tag) {

			case "door" :
				GetComponent<Door>().setInvisible (victim.gameObject);
				break;

			case "softEnemy" :
				break;
			}
		}
	}
}
