using UnityEngine;
using System.Collections;

public class StompTrigger : MonoBehaviour {
	public float knockForce;
	GameObject player;

	void Start() {
		//Play stomp-animation and sound
	//	player = GameObject.Find("Char");
		player = gameObject.transform.parent.gameObject;
	}

	void OnTriggerEnter2D(Collider2D col) {
		switch (col.gameObject.tag) {
		
		case "SoftEnemy":
			col.gameObject.GetComponent<SmallCritter> ().TakeDamage (3);
			col.gameObject.GetComponent<EnemyKnockback> ().Knock (GameObject.Find ("Char"), knockForce);
			break;

		case "EyeEnemy":
			//Can't be hurt with stomp, play relevant things
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

		case "HardEnemy":
			col.gameObject.GetComponent<SmallCritter> ().TakeDamage (2);
			col.gameObject.GetComponent<EnemyKnockback> ().Knock (gameObject.transform.parent.gameObject, knockForce);
			break;
		}
	}
}