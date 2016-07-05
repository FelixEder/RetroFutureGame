using UnityEngine;
using System.Collections;

public class CharPunch : MonoBehaviour {
	
	void OnTriggerStay2D(Collider2D victim) {
		if(Input.GetKey(KeyCode.K)) {
			switch(victim) {

			case GameObject.tag("door") :
				break;

			case GameObject.tag("softEnemy") :
				break;
			}
		}
	}
}
