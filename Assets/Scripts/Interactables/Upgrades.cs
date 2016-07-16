using UnityEngine;
using System.Collections;

public class Upgrades : MonoBehaviour {
	//Name has to be the same 
	public string type;

	void OnTriggerEnter2D(Collider2D col) {
		if (!col.isTrigger && col.gameObject.tag.Equals("char")) {
			//Play correct music and animation depending on what switch-option is chosen
			switch (type) {

			case "leaf":
				col.gameObject.GetComponent<CharFloat>().enabled = true;
				break;

			case "highJump":
				col.gameObject.GetComponent<CharJump> ().jumpSpeed = 15f;
				break;

			case "healthIncrease":
				col.gameObject.GetComponent<CharHealth> ().IncreaseMaxHealth ();
				break;

			case "speedIncrease":
				col.gameObject.GetComponent<CharMovement> ().moveSpeed++;
				break;
			
			case "energyIncrease":
				col.gameObject.GetComponent<CharEnergy> ().IncreaseMaxEnergy ();
			
			}
			Destroy (gameObject);
		}
	}
}
