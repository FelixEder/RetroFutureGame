using UnityEngine;
using System.Collections;

public class Upgrades : MonoBehaviour {
	//Name has to be the same 
	public string upgradeType;

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.name == "Char") {
			//Play correct music and animation depending on what switch-option is chosen
			switch(upgradeType) {

				case "Float":
					col.gameObject.GetComponent<CharFloat>().enabled = true;
					break;

				case "SecondJump":
					col.gameObject.GetComponent<CharJump>().gotSecondJump = true;
					break;

				case "WallJump":
					col.gameObject.GetComponent<WallJump>().enabled = true;
					break;

				case "Stomp":
					col.gameObject.GetComponent<CharStomp>().enabled = true;
					break;

				case "Laser":
					col.gameObject.transform.GetChild(9).gameObject.SetActive(true);
					break;

				case "Health":
					col.gameObject.GetComponent<CharHealth>().IncreaseMaxHealth();
					col.gameObject.GetComponent<CharInventory>().collectItem();
					break;

				case "Speed":
					col.gameObject.GetComponent<CharMovement>().moveSpeed++;
					col.gameObject.GetComponent<CharInventory>().collectItem();
					break;

				case "Energy":
					col.gameObject.GetComponent<CharEnergy>().IncreaseMaxEnergy();
					col.gameObject.GetComponent<CharInventory>().collectItem();
					break;

				case "MegaPunch":
					col.gameObject.transform.GetChild(5).GetComponent<CharPunch>().megaAquired = true;
					break;
			}
			if(GetComponent<AudioPlayer>().audioClips.Length > 0)
				GetComponent<AudioPlayer>().PlayDetached(0, 1, 1, 1);
			GameObject.Find("tutorialPanel").GetComponent<TutorialManager>().DisplayTutorial(upgradeType);
			Destroy(gameObject);
		}
	}
}