using UnityEngine;
using System.Collections;

public class ChargeStation : MonoBehaviour {
	bool deCharged;

	void OnBecameInvisible() {
		deCharged = false;
		//Change sprite to the Charged version
	}

	void OnCollisionStay2D(Collision2D col) {
		if (col.gameObject.tag.Equals ("char") && Input.GetButton ("Attack") && !deCharged) {
			//Play relevant things
			col.gameObject.GetComponent<CharHealth> ().MaximizeHealth ();
			col.gameObject.GetComponent<CharEnergy> ().MaximizeEnergy ();
			deCharged = true;
			//Change sprite to the deCharged version
		}
	}
}