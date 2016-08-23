using UnityEngine;
using System.Collections;

public class FinalBossSpitAttack : MonoBehaviour {
	public int spitLimit;

	void OnEnable() {
		Debug.Log ("Spit-Script!");
		//Also play relevant soundFX
		Invoke ("Spit", 0.5f);
	}

	void Spit() {
		if (transform.parent.parent.childCount < spitLimit) {
			//Play spit SoundFX
			string spawnType = "";
			int critterChoice = Random.Range (0, 12);
			if (critterChoice < 5) {
				spawnType = "JumpingCritter";
			} else if (critterChoice < 9) {
				spawnType = "CrawlerCritter";
			} else if (critterChoice < 11) {
				spawnType = "HardEnemy";
			} else {
				spawnType = "BigEyeGuy";
			}
			GameObject instance = Instantiate (Resources.Load (spawnType), transform.position, Quaternion.identity) as GameObject;
			instance.transform.parent = transform.parent.parent;
		}
	}

	void OnTriggerEnter2D(Collider2D item) {
		if (item.gameObject.GetComponent<PickUpableItem> ().GetItemType () == "Rock") {
			Debug.Log ("You blued the boss!");
			transform.parent.GetComponent<Phase2Head> ().Blued ();
		}
	}
}