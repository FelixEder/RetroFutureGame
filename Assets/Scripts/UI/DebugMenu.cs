using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugMenu : MonoBehaviour {
	GameObject character;
	bool shown;

	void Start () {
		character = GameObject.Find ("Char");
		InvokeRepeating ("UpdateToggles", 1, 5);
	}

	public void ToggleUpgradeState(string upgradeType) {
		switch (upgradeType) {

		case "Float":
			character.GetComponent<CharFloat> ().enabled = !character.GetComponent<CharFloat> ().enabled;
			break;

		case "SecondJump":
			character.GetComponent<CharJump> ().gotSecondJump = !character.GetComponent<CharJump> ().gotSecondJump;
			break;

		case "WallJump":
			character.GetComponent<WallJump> ().enabled = !character.GetComponent<WallJump> ().enabled;
			break;

		case "Stomp":
			character.GetComponent<CharStomp> ().enabled = !character.GetComponent<CharStomp> ().enabled;
			break;

		case "Laser":
			character.transform.GetChild (8).gameObject.SetActive (!character.transform.GetChild (8).gameObject.activeInHierarchy);
			break;

		case "MegaPunch":
			character.transform.GetChild (5).GetComponent<CharMegaPunch> ().enabled = !character.transform.GetChild (5).GetComponent<CharMegaPunch> ().enabled;
			break;

		case "Health":
			character.GetComponent<CharHealth> ().currentHealth = (int)transform.GetChild (8).GetComponent<Slider> ().value;
			character.GetComponent<CharHealth> ().SetHealthSlider ();
			transform.GetChild (8).GetChild (4).GetComponent<Text> ().text = transform.GetChild (8).GetComponent<Slider> ().value.ToString();
			break;

		case "Speed":
			character.GetComponent<CharMovement>().moveSpeed = transform.GetChild (10).GetComponent<Slider> ().value;
			transform.GetChild (10).GetChild (4).GetComponent<Text> ().text = transform.GetChild (10).GetComponent<Slider> ().value.ToString();
			break;

		case "Energy":
			character.GetComponent<CharEnergy> ().currentEnergy = (int) transform.GetChild (9).GetComponent<Slider> ().value;
			character.GetComponent<CharEnergy> ().SetEnergySlider ();
			transform.GetChild (9).GetChild (4).GetComponent<Text> ().text = transform.GetChild (9).GetComponent<Slider> ().value.ToString();
			break;
		}
	}

	public void UpdateToggles() {
		//Float
		transform.GetChild(1).GetComponent<Toggle>().isOn = character.GetComponent<CharFloat>().enabled;
		//SecondJump
		transform.GetChild(2).GetComponent<Toggle>().isOn = character.GetComponent<CharJump> ().gotSecondJump;
		//WallJump
		transform.GetChild(3).GetComponent<Toggle>().isOn = character.GetComponent<WallJump> ().enabled;
		//Stomp
		transform.GetChild(4).GetComponent<Toggle>().isOn = character.GetComponent<CharStomp> ().enabled;
		//Laser
		transform.GetChild(5).GetComponent<Toggle>().isOn = character.transform.GetChild (8).gameObject.activeInHierarchy;
		//MegaPunch
		transform.GetChild(6).GetComponent<Toggle>().isOn = character.transform.GetChild (5).GetComponent<CharMegaPunch> ().enabled;
		//Health
		transform.GetChild (8).GetComponent<Slider> ().value = (float)character.GetComponent<CharHealth> ().currentHealth;
		transform.GetChild (8).GetComponent<Slider> ().maxValue = (float)character.GetComponent<CharHealth> ().maxHealth;
		transform.GetChild (8).GetChild (4).GetComponent<Text> ().text = transform.GetChild (8).GetComponent<Slider> ().value.ToString();
		//Speed
		transform.GetChild (10).GetComponent<Slider> ().value = character.GetComponent<CharMovement>().moveSpeed;
		transform.GetChild (10).GetChild (4).GetComponent<Text> ().text = transform.GetChild (10).GetComponent<Slider> ().value.ToString();
		//Energy
		transform.GetChild (9).GetComponent<Slider> ().value = (float) character.GetComponent<CharEnergy> ().currentEnergy;
		transform.GetChild (9).GetComponent<Slider> ().maxValue = (float) character.GetComponent<CharEnergy> ().maxEnergy;
		transform.GetChild (9).GetChild (4).GetComponent<Text> ().text = transform.GetChild (9).GetComponent<Slider> ().value.ToString();
	}

	public void ToggleDebugMenu() {
		if (shown)
			GetComponent<RectTransform> ().position = new Vector3 (-230, Screen.height, 0);
		else
			GetComponent<RectTransform> ().position = new Vector3 (0, Screen.height, 0);
		shown = !shown;
	}

}
