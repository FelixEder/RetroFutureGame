using UnityEngine;
using System.Collections;

public class BirdBossSpitAttack : MonoBehaviour {

	void Start() {
		//Here start the mouth-opening animation
		//Also play relevant soundFX
		Invoke ("Spit", 0.5f);

	}

	void Spit() {
		//Play spit SoundFX
		int critterChoice = Random.Range (0, 10);

		Instantiate (Resources.Load (spawnType), transform.position, Quaternion.identity);

	}
}

