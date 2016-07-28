using UnityEngine;
using System.Collections;

public class ShellManJumpingTrigger : MonoBehaviour {

	void OnTriggerStay2D() {
		transform.parent.GetComponent<ShellMan> ().Jump ();
	}
}