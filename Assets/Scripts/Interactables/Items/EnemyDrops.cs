using UnityEngine;
using System.Collections;

public class EnemyDrops : MonoBehaviour {
	public string dropType;
	float livedTime;

	void Update() {
		livedTime += Time.deltaTime;
		switch((int) livedTime) {
			case 15:
				//Börja blinka rött lite lätt
				break;

			case 20:
				Destroy (gameObject);
				break;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (!col.isTrigger && col.gameObject.tag.Equals("Char")) {
			//Play correct music and animation depending on what upgrade is choosen
			switch (dropType) {
			//Add more switch-statements as more drops are implemented in the game.
				case "Health":
					col.gameObject.GetComponent<CharHealth> ().IncreaseCurrentHealth (5);
					break;

				case "Energy":
					col.gameObject.GetComponent<CharEnergy> ().IncreaseCurrentEnergy (1);
					break;
			}
			Destroy (gameObject);
		}
	}
}