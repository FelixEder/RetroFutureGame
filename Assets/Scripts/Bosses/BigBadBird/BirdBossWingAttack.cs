using UnityEngine;
using System.Collections;

public class BirdBossWingAttack : MonoBehaviour {

	void Start() {
		//Here start the wing-slapping animation
		//Also play relevant soundFX
	}

	void OnTriggerEnter2D(Collider2D col) {
		Debug.Log("banan");
		if(!col.isTrigger && col.gameObject.tag.Equals("Player")) {
			col.gameObject.GetComponent<PlayerHealth>().TakeDamage(2, col.transform.position + Vector3.right, 10f);
			//Play fitting soundFX
		}
	}
}