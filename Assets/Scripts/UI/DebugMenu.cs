using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugMenu : MonoBehaviour {
	GameObject character;
	bool shown;

	void Start () {
		character = GameObject.Find ("Char");
		GetComponent<RectTransform> ().position = new Vector3 (-Screen.width, Screen.height, 0);
		transform.GetChild(11).gameObject.GetComponent<RectTransform> ().position = new Vector3 (0, Screen.height, 0);
		InvokeRepeating ("UpdateToggles", 1, 5f);
	}

	void Update() {
		transform.GetChild (11).gameObject.GetComponent<RectTransform> ().position = new Vector3 (0, Screen.height, 0);
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
			character.transform.GetChild (9).gameObject.SetActive (!character.transform.GetChild (9).gameObject.activeInHierarchy);
			break;

		case "MegaPunch":
			character.transform.GetChild (6).GetComponent<CharPunch> ().megaAquired = !character.transform.GetChild (6).GetComponent<CharPunch> ().megaAquired;
			break;

		case "Health":
			if (transform.GetChild (8).GetComponent<Slider> ().value > character.GetComponent<CharHealth> ().maxHealth)
				character.GetComponent<CharHealth> ().IncreaseMaxHealth();
			character.GetComponent<CharHealth> ().currentHealth = (int)transform.GetChild (8).GetComponent<Slider> ().value;
			character.GetComponent<CharHealth> ().SetHealthSlider ();
			transform.GetChild (8).GetChild (4).GetComponent<Text> ().text = transform.GetChild (8).GetComponent<Slider> ().value.ToString();
			break;

		case "Speed":
			character.GetComponent<CharMovement>().moveSpeed = transform.GetChild (10).GetComponent<Slider> ().value;
			transform.GetChild (10).GetChild (4).GetComponent<Text> ().text = transform.GetChild (10).GetComponent<Slider> ().value.ToString();
			break;

		case "Energy":
			if (transform.GetChild (9).GetComponent<Slider> ().value > character.GetComponent<CharEnergy> ().maxEnergy)
				character.GetComponent<CharEnergy> ().IncreaseMaxEnergy();
			character.GetComponent<CharEnergy> ().currentEnergy = (int) transform.GetChild (9).GetComponent<Slider> ().value;
			character.GetComponent<CharEnergy> ().SetEnergySlider ();
			transform.GetChild (9).GetChild (4).GetComponent<Text> ().text = transform.GetChild (9).GetComponent<Slider> ().value.ToString();
			break;
		}
		UpdateToggles ();
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
		transform.GetChild(5).GetComponent<Toggle>().isOn = character.transform.GetChild (9).gameObject.activeInHierarchy;
		//MegaPunch
		transform.GetChild(6).GetComponent<Toggle>().isOn = character.transform.GetChild (6).GetComponent<CharPunch> ().megaAquired;
		//Health
		transform.GetChild (8).GetComponent<Slider> ().value = (float)character.GetComponent<CharHealth> ().currentHealth;
		transform.GetChild (8).GetComponent<Slider> ().maxValue = (float)character.GetComponent<CharHealth> ().maxHealth + 1;
		transform.GetChild (8).GetChild (4).GetComponent<Text> ().text = transform.GetChild (8).GetComponent<Slider> ().value.ToString() + "/" + character.GetComponent<CharHealth> ().maxHealth.ToString();
		//Speed
		transform.GetChild (10).GetComponent<Slider> ().value = character.GetComponent<CharMovement>().moveSpeed;
		transform.GetChild (10).GetChild (4).GetComponent<Text> ().text = transform.GetChild (10).GetComponent<Slider> ().value.ToString();
		//Energy
		transform.GetChild (9).GetComponent<Slider> ().value = (float) character.GetComponent<CharEnergy> ().currentEnergy;
		transform.GetChild (9).GetComponent<Slider> ().maxValue = (float) character.GetComponent<CharEnergy> ().maxEnergy + 1;
		transform.GetChild (9).GetChild (4).GetComponent<Text> ().text = transform.GetChild (9).GetComponent<Slider> ().value.ToString() + "/" + character.GetComponent<CharEnergy> ().maxEnergy.ToString();
	}

	public void ToggleDebugMenu() {
		if (shown)
			GetComponent<RectTransform> ().position = new Vector3 (-Screen.width, Screen.height, 0);
		else
			GetComponent<RectTransform> ().position = new Vector3 (0, Screen.height, 0);
		shown = !shown;
	}

}
