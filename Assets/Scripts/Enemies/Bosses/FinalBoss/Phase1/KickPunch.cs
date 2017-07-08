using UnityEngine;
using System.Collections;

public class KickPunch : MonoBehaviour {

	void OnEnable() {
		Debug.Log("Boss is Punching");
		//Also play relevant soundFX
		this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(!col.collider.isTrigger && col.gameObject.tag.Equals("Char")) {
			col.gameObject.GetComponent<CharHealth>().TakeDamage(5, gameObject, 3f);
			//Play fitting soundFX
		}
	}
}
