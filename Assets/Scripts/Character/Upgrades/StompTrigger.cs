using UnityEngine;
using System.Collections;

public class StompTrigger : MonoBehaviour {
	public float knockForce;

	void Start() {
		//Play stomp-animation and sound
	}

	void OnTriggerEnter2D(Collider2D col) {
		switch (col.gameObject.tag) {
		
		case "SmallCritter":
			col.gameObject.GetComponent<SmallCritter> ().TakeDamage (3);
			col.gameObject.GetComponent<EnemyKnockback> ().Knock (GameObject.Find ("Char"), knockForce);
			break;

		case "JumpingCritter":
			col.gameObject.GetComponent<JumpingCritter> ().TakeDamage (3);
			col.gameObject.GetComponent<EnemyKnockback> ().Knock (GameObject.Find ("Char"), knockForce);
			break;

		case "CrawlerCritter":
			Debug.Log ("Hit crawler!");
			//Really bad code, should be re-written
			CrawlerCritter crawlerCritter = col.gameObject.GetComponent<CrawlerCritter> ();
			if (!crawlerCritter.deShelled) {
				crawlerCritter.GetHurt (1);
			} else if (crawlerCritter.deShelled) {
				crawlerCritter.GetHurt (2);
			} 
			break;

		case "ShellMan":
			ShellMan shellMan = col.gameObject.GetComponent<ShellMan> ();
			if (!shellMan.deShelled) {
				shellMan.GetHurt (1);
			} else if (shellMan.deShelled) {
				shellMan.GetHurt (2);
			}
			break;
		}
	}
}