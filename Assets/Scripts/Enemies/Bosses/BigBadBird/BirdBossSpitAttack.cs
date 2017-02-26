using UnityEngine;
using System.Collections;

public class BirdBossSpitAttack : MonoBehaviour {
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
			int critterChoice = Random.Range (0, 20);
			if (critterChoice < 5) {
				spawnType = "SmallCritter";
			} else if (critterChoice < 9) {
				spawnType = "JumpingCritter";
			} else if (critterChoice < 11) {
				spawnType = "CrawlerCritter";
			} else if (critterChoice < 14) {
				spawnType = "BigEyeGuy";
			} else
				return;
			GameObject instance = Instantiate (Resources.Load (spawnType), transform.position, Quaternion.identity) as GameObject;
			instance.transform.parent = transform.parent.parent;
			instance.GetComponent<SpawnProperties> ().initialFreezeTime = 1;
			instance.GetComponent<Rigidbody2D> ().velocity = new Vector2 (transform.parent.gameObject.GetComponent<Rigidbody2D> ().velocity.x * 3f, 0);
		}
	}
}