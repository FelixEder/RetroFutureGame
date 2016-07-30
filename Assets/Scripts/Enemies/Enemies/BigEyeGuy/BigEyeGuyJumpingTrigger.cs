using UnityEngine;
using System.Collections;

public class BigEyeGuyJumpingTrigger : MonoBehaviour {

	void OnTriggerStay2D() {
		transform.parent.GetComponent<BigEyeGuy> ().Jump ();
	}
}
