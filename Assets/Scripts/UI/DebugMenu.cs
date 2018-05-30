using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugMenu : MonoBehaviour {
	public GameObject player, droid, HUD;

	void Start() {
		InvokeRepeating("UpdateToggles", 1, 5f);
	}

	public void ToggleUpgradeState(string upgradeType) {
		switch(upgradeType) {

			case "Float":
				player.GetComponent<Float>().enabled = !player.GetComponent<Float>().enabled;
				break;

			case "SecondJump":
				player.GetComponent<PlayerJump>().gotSecondJump = !player.GetComponent<PlayerJump>().gotSecondJump;
				break;

			case "WallJump":
				player.GetComponent<WallJump>().enabled = !player.GetComponent<WallJump>().enabled;
				break;

			case "Stomp":
				player.GetComponent<Stomp>().enabled = !player.GetComponent<Stomp>().enabled;
				break;

			case "Laser":
				droid.SetActive(!droid.activeInHierarchy);
				break;

			case "MegaPunch":
				player.GetComponent<PlayerPunch>().megaAquired = !player.GetComponent<PlayerPunch>().megaAquired;
				break;

			case "Small":
				player.transform.GetChild(5).gameObject.SetActive(!player.transform.GetChild(5).gameObject.activeInHierarchy);
				break;

			case "Health":
				HUD.SetActive(true);
				if(transform.GetChild(7).GetComponent<Slider>().value > player.GetComponent<PlayerHealth>().maxHealth)
					player.GetComponent<PlayerHealth>().IncreaseMaxHealth();
				player.GetComponent<PlayerHealth>().currentHealth = (int) transform.GetChild(7).GetComponent<Slider>().value;
				player.GetComponent<PlayerHealth>().SetHealthSlider();
				transform.GetChild(7).GetChild(4).GetComponent<Text>().text = transform.GetChild(7).GetComponent<Slider>().value.ToString();
				break;

			case "Speed":
				player.GetComponent<PlayerMovement>().moveSpeed = transform.GetChild(9).GetComponent<Slider>().value;
				transform.GetChild(9).GetChild(4).GetComponent<Text>().text = transform.GetChild(9).GetComponent<Slider>().value.ToString();
				break;

			case "Energy":
				HUD.SetActive(true);
				if(transform.GetChild(8).GetComponent<Slider>().value > player.GetComponent<PlayerEnergy>().maxEnergy)
					player.GetComponent<PlayerEnergy>().IncreaseMaxEnergy();
				player.GetComponent<PlayerEnergy>().currentEnergy = (int) transform.GetChild(8).GetComponent<Slider>().value;
				player.GetComponent<PlayerEnergy>().SetEnergySlider();
				transform.GetChild(8).GetChild(4).GetComponent<Text>().text = transform.GetChild(8).GetComponent<Slider>().value.ToString();
				break;
		}
		UpdateToggles();
	}

	public void UpdateToggles() {
		//Float
		transform.GetChild(0).GetComponent<Toggle>().isOn = player.GetComponent<Float>().enabled;
		//SecondJump
		transform.GetChild(1).GetComponent<Toggle>().isOn = player.GetComponent<PlayerJump>().gotSecondJump;
		//WallJump
		transform.GetChild(2).GetComponent<Toggle>().isOn = player.GetComponent<WallJump>().enabled;
		//Stomp
		transform.GetChild(3).GetComponent<Toggle>().isOn = player.GetComponent<Stomp>().enabled;
		//Laser
		transform.GetChild(4).GetComponent<Toggle>().isOn = droid.activeInHierarchy;
		//MegaPunch
		transform.GetChild(5).GetComponent<Toggle>().isOn = player.GetComponent<PlayerPunch>().megaAquired;
		//Small
		transform.GetChild(6).GetComponent<Toggle>().isOn = player.transform.GetChild(5).gameObject.activeInHierarchy;
		//Health
		transform.GetChild(7).GetComponent<Slider>().value = (float) player.GetComponent<PlayerHealth>().currentHealth;
		transform.GetChild(7).GetComponent<Slider>().maxValue = (float) player.GetComponent<PlayerHealth>().maxHealth + 1;
		transform.GetChild(7).GetChild(4).GetComponent<Text>().text = transform.GetChild(7).GetComponent<Slider>().value.ToString() + "/" + player.GetComponent<PlayerHealth>().maxHealth.ToString();
		//Speed
		transform.GetChild(9).GetComponent<Slider>().value = player.GetComponent<PlayerMovement>().moveSpeed;
		transform.GetChild(9).GetChild(4).GetComponent<Text>().text = transform.GetChild(9).GetComponent<Slider>().value.ToString();
		//Energy
		transform.GetChild(8).GetComponent<Slider>().value = (float) player.GetComponent<PlayerEnergy>().currentEnergy;
		transform.GetChild(8).GetComponent<Slider>().maxValue = (float) player.GetComponent<PlayerEnergy>().maxEnergy + 1;
		transform.GetChild(8).GetChild(4).GetComponent<Text>().text = transform.GetChild(8).GetComponent<Slider>().value.ToString() + "/" + player.GetComponent<PlayerEnergy>().maxEnergy.ToString();
	}
}
