using UnityEngine;
using System.Collections;

public class PlatformLogic : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name == "char") {
			Physics2D.IgnoreLayerCollision (8, 9, false);
			Debug.Log ("enter trigger");
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.name == "char") {
			Physics2D.IgnoreLayerCollision (8, 9, true);
			Debug.Log ("exit trigger");
		}
	}
	/*
	function OnTriggerEnter (jumper: Collider) {
    //make the parent platform ignore the jumper
    var platform = transform.parent;
    Physics.IgnoreCollision(jumper.GetComponent(CharacterController), platform.GetComponent(BoxCollider));
}
 
function OnTriggerExit (jumper: Collider) {
    //reset jumper's layer to something that the platform collides with
    //just in case we wanted to jump throgh this one
    jumper.gameObject.layer = 0;
   
    //re-enable collision between jumper and parent platform, so we can stand on top again
    var platform = transform.parent;
    Physics.IgnoreCollision(jumper.GetComponent(CharacterController), platform.GetComponent(BoxCollider), false);
}
*/
}

