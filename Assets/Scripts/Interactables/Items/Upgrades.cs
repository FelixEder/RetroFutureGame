using UnityEngine;
using System.Collections;

public class Upgrades : MonoBehaviour {
	//Name has to be the same 
	public string upgradeType;
	public GameObject upgradeLocation;
	[Header("Tutorial")]
	public GameObject manager;
	public float timeToDisplay;
	public bool stringIsHideKey;
	public string tutorial;
	[TextArea]
	public string tutorialText;

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.name == "Player") {
			PlayerInventory playInv = col.gameObject.GetComponent<PlayerInventory>();
			//Play correct music and animation depending on what switch-option is chosen
			switch(upgradeType) {

				case "Float":
					col.gameObject.GetComponent<Float>().enabled = true;
					playInv.AddUpgrade("float");
					break;

				case "SecondJump":
					col.gameObject.GetComponent<PlayerJump>().secondJumpAcquired = true;
					playInv.AddUpgrade("secondJump");
					break;

				case "WallJump":
					col.gameObject.GetComponent<WallJump>().enabled = true;
					playInv.AddUpgrade("wallJump");
					break;

				case "Stomp":
					col.gameObject.GetComponent<Stomp>().enabled = true;
					playInv.AddUpgrade("stomp");
					break;

				case "Laser":
					upgradeLocation.SetActive(true);
					playInv.AddUpgrade("laser");
					break;

				case "Health":
					col.gameObject.GetComponent<PlayerHealth>().IncreaseMaxHealth();
					col.gameObject.GetComponent<PlayerInventory>().CollectItem();
					break;

				case "Speed":
					col.gameObject.GetComponent<PlayerMovement>().moveSpeed++;
					col.gameObject.GetComponent<PlayerInventory>().CollectItem();
					break;

				case "Energy":
					col.gameObject.GetComponent<PlayerEnergy>().IncreaseMaxEnergy();
					col.gameObject.GetComponent<PlayerInventory>().CollectItem();
					break;

				case "MegaPunch":
					upgradeLocation.GetComponent<PlayerPunch>().megaAcquired = true;
					playInv.AddUpgrade("mega");
					break;

                case "Small":
                    upgradeLocation.SetActive(true);
					playInv.AddUpgrade("small");
					break;
			}
			/*
			if(GetComponent<AudioPlayer>().audioClips.Length > 0)
				GetComponent<AudioPlayer>().PlayDetached(0, 1, 1, 1);
			*/
			manager.GetComponent<TutorialManager>().DisplayTutorial(tutorial, tutorialText, stringIsHideKey, timeToDisplay);
			Destroy(gameObject);
		}
	}
}