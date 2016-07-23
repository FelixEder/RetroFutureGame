using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharHealth : MonoBehaviour {
	public int currentHealth, maxHealth;
	bool dead;
	Slider slider;

	void Start() {
		slider = GameObject.Find ("healthSlider").GetComponent<Slider> ();
		SetHealthSliderSize ();
		SetHealthSlider ();
	}

	void Update() {
		if (currentHealth <= 0 && !dead)
			Die ();
	}

	public void TakeDamage(int damage) {
		currentHealth -= damage;
		StartCoroutine(TransitionHealthSlider ());
	}
		
	public void IncreaseCurrentHealth(int amount) {
		currentHealth += amount;
		if (currentHealth > maxHealth)
			currentHealth = maxHealth;
		StartCoroutine(TransitionHealthSlider ());
	}

	public void IncreaseMaxHealth() {
		maxHealth ++;
		currentHealth = maxHealth;
		SetHealthSliderSize ();
		StartCoroutine(TransitionHealthSlider ());
	}

	void SetHealthSlider() {
		slider.value = (float) currentHealth / maxHealth;
	}

	void SetHealthSliderSize() {
		GameObject.Find ("healthSlider").GetComponent<RectTransform> ().sizeDelta = new Vector2 (8 + 32 * (float)maxHealth, 32);
	}

	IEnumerator TransitionHealthSlider() {
		float transitionSpeed = (maxHealth - Mathf.Abs (slider.value * maxHealth - currentHealth)) / 2;
		while (slider.value * (float) maxHealth < currentHealth - 0.1 || slider.value * (float) maxHealth > currentHealth + 0.1) {
			slider.value = Mathf.Lerp (slider.value, (float) currentHealth / maxHealth, Time.deltaTime * transitionSpeed);
			yield return new WaitForSeconds (0.01f);
		}
		SetHealthSlider ();
	}

	void Die() {
		dead = true;
		Debug.Log ("YOU DIED\n------------");
	}
}