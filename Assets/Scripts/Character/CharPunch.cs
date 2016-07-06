using UnityEngine;
using System.Collections;

public class CharPunch : MonoBehaviour {
	bool isPunching = false;
	
	void OnTriggerStay2D(Collider2D victim) {
		if(Input.GetKey(KeyCode.K)) {
			if(!isPunching) {
				isPunching = true;
				//Here an animation and soundFX will be played

				switch (victim.gameObject.tag) {

				case "door":
					victim.gameObject.GetComponent<Door> ().setInvisible ();
					break;

				case "softEnemy":
					victim.gameObject.GetComponent<SmallCritter>.getHurt ();
					break;
				}
				//Make it so that this boolean is set to false only when the punch animation is finished
				isPunching = false;
			}
		}
	}
}
