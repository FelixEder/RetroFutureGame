using UnityEngine;
using System.Collections;

public class Electrohaz : MonoBehaviour {
	/* NOT USED
	public Sprite inactive, charging, active;
	SpriteRenderer sr;
	*/
	bool isActive;
	public int damage;
	AudioPlayer audioplay;

	void Start() {
		/* NOT USED
		sr = GetComponent<SpriteRenderer>();
		sr.sprite = inactive;
		InvokeRepeating("EspisMetod", 0, 2);
		*/
		audioplay = GetComponent<AudioPlayer>();
		StartCoroutine(GustavsMetod());
	}

	void Update() {
		transform.localPosition = new Vector2(transform.localPosition.x + 0.1f, transform.localPosition.y);
		transform.localPosition = new Vector2(transform.localPosition.x - 0.1f, transform.localPosition.y);
	}

	IEnumerator GustavsMetod() {
		while(true) {
			//Idle
			yield return new WaitForSeconds(5f);

			GetComponent<Animator>().SetTrigger("Start");
			while(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idle")) {
				yield return 0;
			}
			//Charging
			audioplay.PlayClip(2, 1);

			while(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("lightning_charge")) {
				yield return 0;
			}
			//Active
			isActive = true;
			audioplay.StopPlaying();
			audioplay.PlayClip(1, 1);
			audioplay.PlayClip(0, 1);

			while(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("lightning_active")) {
				yield return 0;
			}
			//Idle
			isActive = false;
			audioplay.StopPlaying();
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
		if(isActive) {
			switch(col.gameObject.tag) {
				case "Player":
					col.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage, gameObject.transform.position, 5f);
					break;

				case "SmallCritter":
				case "JumpingCritter":
				case "HardCritter":
				case "BigEyeGuy":
				case "CrawlerCritter":
				case "ShellMan":
					col.gameObject.GetComponent<EnemyHealth>().TakeDamage(99);
					break;

				case "PickupableItem":
					col.gameObject.GetComponent<PickUpableItem>().Break();
					break;
				case "Wall":
				case "Door":
					break;
			}
		}
	}
}