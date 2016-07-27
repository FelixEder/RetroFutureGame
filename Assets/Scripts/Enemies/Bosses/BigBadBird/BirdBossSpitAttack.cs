using UnityEngine;
using System.Collections;

public class BirdBossSpitAttack : MonoBehaviour {
	string spawnType;

	void Start() {
		//Here start the mouth-opening animation
		//Also play relevant soundFX
		Invoke ("Spit", 0.5f);
	}

	void Spit() {
		//Play spit SoundFX
		int critterChoice = Random.Range (0, 12);
		if (critterChoice < 5) {
			spawnType = "SmallCritter";
		} else if (critterChoice < 9) {
			spawnType = "JumpingCritter";
		} else if (critterChoice < 11) {
			spawnType = "PrettyStrongCritter";
		} else {
			spawnType = "BigBadCritter";
		}
		Instantiate (Resources.Load (spawnType), transform.position, Quaternion.identity);
	}
}