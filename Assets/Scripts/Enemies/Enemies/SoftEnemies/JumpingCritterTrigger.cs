using UnityEngine;
using System.Collections;

public class JumpingCritterTrigger : MonoBehaviour {

	void OnTriggerStay2D() {
		transform.parent.GetComponent<JumpingCritter> ().Jump ();
	}
}
