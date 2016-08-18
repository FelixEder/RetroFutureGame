using UnityEngine;
using System.Collections;

public class Phase2Head : MonoBehaviour {
	public Sprite normalHead, blueFace, biteFace;
	bool isSpitting;
	Phase2 actualBoss;

	void Start() {
		actualBoss = transform.parent.gameObject.GetComponent<Phase2> ();
	}

	void Blued() {
		//This should only change the head-sprite, so maybe this should affect a child somehow?
		GetComponent<SpriteRenderer>().sprite = blueFace;
		actualBoss.blued = true;
		actualBoss.Stunned(5f);
		Invoke ("Unblued", 5f);
	}

	void Unblued() {
		GetComponent<SpriteRenderer>().sprite = normalHead;
		actualBoss.blued = false;
	}

	void Spit() {
		if(!actualBoss.blued) {
			GetComponent<SpriteRenderer> ().sprite = biteFace;
			//Call PrefabSpawner somehow
			Invoke ("StopSpit", 3f);
		}
	}

	void StopSpit() {
		GetComponent<SpriteRenderer> ().sprite = normalHead;
	}

	void OnCollisionEnter2D(Collision2D col) {
		switch (col.gameObject.tag) {

		case "PickupableItem":
			if (col.gameObject.GetComponent<PickUpableItem> ().GetItemType () == "Rock") {
				//Maybe play grunt
				Debug.Log ("Threw rock at boss's head!");
				if (Random.Range (0, 2) == 0)
					Instantiate (Resources.Load ("HealthDrop"), transform.position, Quaternion.identity);
				else
					Instantiate (Resources.Load ("EnergyDrop"), transform.position, Quaternion.identity);
				Destroy (col.gameObject);
				//Find a way to get KickPunching ();
			}
			break;

		case "Char":
			if (!actualBoss.stunned && !actualBoss.blued) {
				//Find a way to get KickPunching ();
				col.gameObject.GetComponent<CharHealth> ().TakeDamage (5, gameObject, 5f);
				//Should also knock the player away
				actualBoss.walksRight = false;
				actualBoss.ResetDeltaX ();
			}
			break;
		}
	}
}