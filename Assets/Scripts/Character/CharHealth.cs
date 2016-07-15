using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharHealth : MonoBehaviour {
	public int currentHealth, maxHealth;

	void SetHealthSlider() {
		GameObject.Find ("healthSlider").Slider.Set (currentHealth / maxHealth);
	}
}