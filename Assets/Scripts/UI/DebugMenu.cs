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
			case "Health":
				HUD.SetActive(true);
				var health = player.GetComponent<PlayerHealth>();
				if(transform.GetChild(7).GetComponent<Slider>().value > health.maxHealth)
					health.IncreaseMaxHealth();
				health.currentHealth = (int) transform.GetChild(7).GetComponent<Slider>().value;
				health.SetHealthSlider();
				transform.GetChild(7).GetChild(4).GetComponent<Text>().text = transform.GetChild(7).GetComponent<Slider>().value.ToString();
				break;

			case "Speed":
				player.GetComponent<PlayerMovement>().moveSpeed = transform.GetChild(9).GetComponent<Slider>().value;
				transform.GetChild(9).GetChild(4).GetComponent<Text>().text = transform.GetChild(9).GetComponent<Slider>().value.ToString();
				break;

			case "Energy":
				HUD.SetActive(true);
				var energy = player.GetComponent<PlayerEnergy>();
				if(transform.GetChild(8).GetComponent<Slider>().value > energy.maxEnergy)
					energy.IncreaseMaxEnergy();
				energy.currentEnergy = (int) transform.GetChild(8).GetComponent<Slider>().value;
				energy.SetEnergySlider(energy.slider);
				transform.GetChild(8).GetChild(4).GetComponent<Text>().text = transform.GetChild(8).GetComponent<Slider>().value.ToString();
				break;
				
			default:
				player.GetComponent<PlayerInventory>().AddUpgrade(upgradeType);
			break;
		}
		UpdateToggles();
	}

	public void UpdateToggles() {
		var inventory = player.GetComponent<PlayerInventory>();
		//Float
		transform.GetChild(0).GetComponent<Toggle>().isOn = inventory.HasAcquired("float");
		//SecondJump
		transform.GetChild(1).GetComponent<Toggle>().isOn = inventory.HasAcquired("secondJump");
		//WallJump
		transform.GetChild(2).GetComponent<Toggle>().isOn = inventory.HasAcquired("walljump");
		//Stomp
		transform.GetChild(3).GetComponent<Toggle>().isOn = inventory.HasAcquired("stomp");
		//Laser
		transform.GetChild(4).GetComponent<Toggle>().isOn = inventory.HasAcquired("laser");
		//MegaPunch
		transform.GetChild(5).GetComponent<Toggle>().isOn = inventory.HasAcquired("mega");
		//Small
		transform.GetChild(6).GetComponent<Toggle>().isOn = inventory.HasAcquired("small");
		//Health
		var health = player.GetComponent<PlayerHealth>();
		transform.GetChild(7).GetComponent<Slider>().value = (float) health.currentHealth;
		transform.GetChild(7).GetComponent<Slider>().maxValue = (float) health.maxHealth + 1;
		transform.GetChild(7).GetChild(4).GetComponent<Text>().text = transform.GetChild(7).GetComponent<Slider>().value.ToString() + "/" + health.maxHealth.ToString();
		//Speed
		transform.GetChild(9).GetComponent<Slider>().value = player.GetComponent<PlayerMovement>().moveSpeed;
		transform.GetChild(9).GetChild(4).GetComponent<Text>().text = transform.GetChild(9).GetComponent<Slider>().value.ToString();
		//Energy
		var energy = player.GetComponent<PlayerEnergy>();
		transform.GetChild(8).GetComponent<Slider>().value = (float) energy.currentEnergy;
		transform.GetChild(8).GetComponent<Slider>().maxValue = (float) energy.maxEnergy + 1;
		transform.GetChild(8).GetChild(4).GetComponent<Text>().text = transform.GetChild(8).GetComponent<Slider>().value.ToString() + "/" + energy.maxEnergy.ToString();
	}
}
