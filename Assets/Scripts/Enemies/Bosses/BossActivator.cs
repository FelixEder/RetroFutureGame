using UnityEngine;
using System.Collections;

public class BossActivator : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if (!col.isTrigger && col.gameObject.tag.Equals("Char")) {
			this.gameObject.transform.GetChild (0).gameObject.SetActive (true);
		}
	}
}
