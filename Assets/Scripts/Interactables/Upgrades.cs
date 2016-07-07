using UnityEngine;
using System.Collections;

public class Upgrades : MonoBehaviour {
	//Name has to be the same 
	public string type;
	CharInventory charInventory;

	void Start() {
		charInventory = GetComponent<CharInventory> ();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (!col.isTrigger) {
			//In the future maybe a check so that only the player can activate the item?

			//Play correct music and animation depending on what switch-option is chosen, change player-image somewhat and display a tutorial for the item.
			switch (name) {

			case "leaf":
				charInventory.leaf = true;
				break;

			}
			Destroy (gameObject);
		}
	}
}
