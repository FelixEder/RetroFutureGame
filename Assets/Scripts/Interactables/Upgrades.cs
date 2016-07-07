using UnityEngine;
using System.Collections;

public class Upgrades : MonoBehaviour {
	//Name has to be the same 
	public string type;

	void OnTriggerEnter2D(Collider2D col) {
		if (!col.isTrigger && col.gameObject.tag.Equals("char")) {
			//Play correct music and animation depending on what switch-option is chosen, change player-image somewhat and display a tutorial for the item.
			switch (type) {

			case "leaf":
				col.gameObject.GetComponent<CharInventory>().leaf = true;
				break;
			}
			Destroy (gameObject);
		}
	}
}
