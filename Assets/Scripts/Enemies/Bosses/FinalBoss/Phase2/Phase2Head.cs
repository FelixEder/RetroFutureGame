using UnityEngine;
using System.Collections;

public class Phase2Head : MonoBehaviour {
	public Sprite normalHead, blueFace, biteFace;
	bool isSpitting;
	Phase2 actualBoss;

	void Start() {
		actualBoss = transform.parent.gameObject.GetComponent<Phase2> ();
		InvokeRepeating ("Spit", 5f, 10f);
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
			Debug.Log ("Boss is spitting");
			GetComponent<SpriteRenderer> ().sprite = biteFace;
			transform.GetChild (0).GetComponent<FinalBossSpitAttack> ().enabled = true;
			Invoke ("StopSpit", 3f);
		}
	}

	void StopSpit() {
		GetComponent<SpriteRenderer> ().sprite = normalHead;
		transform.GetChild (0).GetComponent<FinalBossSpitAttack> ().enabled = false;
	}
		
	public void OpenMouth(float time) {
		GetComponent<SpriteRenderer> ().sprite = biteFace;
		Invoke ("CloseMouth", time);
	}

	void CloseMouth() {
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