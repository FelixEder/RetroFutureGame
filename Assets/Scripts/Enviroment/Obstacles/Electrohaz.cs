using UnityEngine;
using System.Collections;

public class Electrohaz : MonoBehaviour {
	/* NOT USED
	public Sprite inactive, charging, active;
	SpriteRenderer sr;
	*/
	bool isActive;
	public int damage;

	void Start() {
		/* NOT USED
		sr = GetComponent<SpriteRenderer>();
		sr.sprite = inactive;
		InvokeRepeating("EspisMetod", 0, 2);
		*/
		StartCoroutine (GustavsMetod ());
	}

	void Update() {
		transform.localPosition = new Vector2 (transform.localPosition.x + 0.1f, transform.localPosition.y);
		transform.localPosition = new Vector2 (transform.localPosition.x - 0.1f, transform.localPosition.y);
	}

	IEnumerator GustavsMetod() {
		while (true) {
			yield return new WaitForSeconds (3f);
			Debug.Log ("Start charge");
			GetComponent<Animator> ().SetTrigger ("Start");
			while (GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("lightning_charge")) {
				yield return 0;
			}
			Debug.Log ("Start active");
			isActive = true;
			while (GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("lightning_active")) {
				yield return 0;
			}
			Debug.Log ("Start idle");
			isActive = false;
		}
	}

	/* NOT USED
	void EspisMetod() {
		//Debug.Log (sr.sprite + " " + inactive + charging + active);
		if(sr.sprite == inactive) {
 			sr.sprite = charging;

 		}
		else if(sr.sprite == charging) {
 			sr.sprite = active;
 			isActive = true;
 		}
		else if(sr.sprite == active) {
			sr.sprite = inactive;
			isActive = false;
		}
	}
	*/

	void OnTriggerStay2D(Collider2D col) {
		if (isActive) {
			switch (col.gameObject.tag) {
			case "Char":
				col.gameObject.GetComponent<CharHealth> ().TakeDamage (damage);
				break;

			case "SmallCritter":
			case "JumpingCritter":
			case "HardEnemy":
			case "BigEyeGuy":
			case "CrawlerCritter":
			case "ShellMan":
			case "Wall":
			case "Door":
				break;

			case "PickupableItem":
				col.gameObject.GetComponent<PickUpableItem> ().Break ();
				break;
			}
		}
	}
}