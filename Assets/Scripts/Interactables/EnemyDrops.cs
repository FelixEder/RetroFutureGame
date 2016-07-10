using UnityEngine;
using System.Collections;

public class EnemyDrops : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if (!col.isTrigger && col.gameObject.tag.Equals("char")) {
			//Play correct music and animation depending on what upgrade is choosen
			switch (gameObject.tag) {
			//Add more switch-statements as more drops are implemented in the game.

			case "healthDrop":
				//Increase player health here.
				break;
			}
			Destroy (gameObject);
		}
	}
}