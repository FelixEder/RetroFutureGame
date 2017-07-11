using UnityEngine;
using System.Collections;

public class ChargeStation : MonoBehaviour {
	bool deCharged;

	void OnBecameInvisible() {
		deCharged = false;
		//Change sprite to the Charged version
	}

	void OnCollisionStay2D(Collision2D col) {
		if(col.gameObject.tag.Equals("Player") && Input.GetButton("Attack") && !deCharged) {
			//Play relevant things
			col.gameObject.GetComponent<PlayerHealth>().MaximizeHealth();
			col.gameObject.GetComponent<PlayerEnergy>().MaximizeEnergy();
			deCharged = true;
			//Change sprite to the deCharged version
		}
	}
}