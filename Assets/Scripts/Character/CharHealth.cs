using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharHealth : MonoBehaviour {
	public float currentHealth, maxHealth;

	void Update() {
		SetHealthSlider ();
	}

	void SetHealthSlider() {
		GameObject.Find ("healthSlider").GetComponent<Slider>().value = currentHealth / maxHealth;
		GameObject.Find ("healthSlider").GetComponent<RectTransform> ().sizeDelta = new Vector2 (8 + 32 * maxHealth, 32);
	}
}