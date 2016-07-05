using UnityEngine;
using System.Collections;

public class CharPunch : MonoBehaviour {
	
	void OnTriggerStay2D(Collider2D victim) {
		if(Input.GetKey(KeyCode.K)) {
			//Here an animation and music will be played
			switch(victim) {

			case GameObject.tag("door"):
				GetComponent<Door>().setInvisible ();
				break;

			case GameObject.tag("softEnemy") :
				break;
			}
		}
	}
}
