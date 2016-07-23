using UnityEngine;
using System.Collections;

public class Upgrades : MonoBehaviour {
	//Name has to be the same 
	public string type;

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name == "char") {
			//Play correct music and animation depending on what switch-option is chosen
			switch (type) {

			case "leaf":
				col.gameObject.GetComponent<CharFloat>().enabled = true;
				break;

			case "highJump":
				col.gameObject.GetComponent<CharJump> ().jumpSpeed = 15f;
				break;

			case "wallJump":
				col.gameObject.GetComponent<WallJump> ().enabled = true;
				break;

			case "stomp":
				col.gameObject.GetComponent<CharStomp> ().enabled = true;
				break;
			
			case "laserGun":
				col.gameObject.transform.GetChild (8).gameObject.SetActive (true);
				break;

			case "healthIncrease":
				col.gameObject.GetComponent<CharHealth> ().IncreaseMaxHealth ();
				break;

			case "speedIncrease":
				col.gameObject.GetComponent<CharMovement> ().moveSpeed++;
				break;
			
			case "energyIncrease":
				col.gameObject.GetComponent<CharEnergy> ().IncreaseMaxEnergy ();
				break;
			}
			Destroy (gameObject);
		}
	}
}