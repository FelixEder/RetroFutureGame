using UnityEngine;
using System.Collections;

public class StompTrigger : MonoBehaviour {
	public float knockForce;

	void Start() {
		//Play stomp-animation and sound
	}

	void OnTriggerEnter2D(Collider2D col) {
		switch(col.gameObject.tag) {

			case "SmallCritter":
			case "JumpingCritter":
				col.gameObject.GetComponent<EnemyHealth>().TakeDamage(3, gameObject, knockForce);
				break;

			case "CrawlerCritter":
				Debug.Log("Hit crawler!");
				//TODO: Really bad code, should be re-written
				CrawlerCritter crawlerCritter = col.gameObject.GetComponent<CrawlerCritter>();
				if(!crawlerCritter.deShelled) {
					crawlerCritter.TakeDamage(1);
				}
				else if(crawlerCritter.deShelled) {
					crawlerCritter.TakeDamage(2);
				}
				break;

			case "ShellMan":
				ShellMan shellMan = col.gameObject.GetComponent<ShellMan>();
				if(!shellMan.deShelled) {
					shellMan.TakeDamage(1);
				}
				else if(shellMan.deShelled) {
					shellMan.TakeDamage(2);
				}
				break;

			case "FinalBossLastForm":
				col.gameObject.GetComponent<Phase3Head>().TakeDamage();
				break;
		}
		Debug.Log("STOMPED: " + col.gameObject.name + " with tag: " + col.gameObject.tag);
	}
}