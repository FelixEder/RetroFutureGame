using UnityEngine;
using System.Collections;

public class EnemyDrops : MonoBehaviour {
	int livedTime;

	void Start() {
		livedTime = (int) Time.time;
	}

	void Update() {
		int controlTime = (int) Time.time - livedTime;
		switch(controlTime) {
			case 15:
				//Börja blinka rött lite lätt
				break;

			case 25:
				//Blinka väldigt snabbt rött, upprepa förevigt
				break;

			case 30:
				Destroy (gameObject);
				break;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (!col.isTrigger && col.gameObject.tag.Equals("char")) {
			//Play correct music and animation depending on what upgrade is choosen
			switch (gameObject.tag) {
			//Add more switch-statements as more drops are implemented in the game.
				case "healthDrop":
					col.gameObject.GetComponent<CharHealth> ().IncreaseCurrentHealth (1);
					break;
			}
			Destroy (gameObject);
		}
	}
}