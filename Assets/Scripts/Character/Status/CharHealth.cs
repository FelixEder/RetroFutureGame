using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharHealth : MonoBehaviour {
	public int currentHealth, maxHealth;
	public float invulnerabilityTime;
	Slider slider;
	CharStatus status;

	void Start() {
		slider = GameObject.Find ("healthSlider").GetComponent<Slider> ();
		status = GameObject.Find ("Char").GetComponent<CharStatus> ();
		SetHealthSliderSize ();
		SetHealthSlider ();
	}

	public void TakeDamage(int damage) {
		//Maybe give a few seconds invincibility and make sprite blink or so?
		if (!status.Invulnerable ()) {
			if (currentHealth - damage <= 0) {
				currentHealth = 0;
				Die();
			}
			else
				currentHealth -= damage;
			status.Invulnerable (invulnerabilityTime);
			StartCoroutine (TransitionHealthSlider ());
		}
	}

	public void TakeDamage(int damage, GameObject attacker, float knockbackForce) {
		GetComponent<CharKnockback> ().Knockback (attacker, knockbackForce);
		TakeDamage (damage);
	}

	public void Knockback(GameObject attacker, float knockbackForce) {

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

	public void SetHealthSlider() {
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

	public void MaximizeHealth() {
		currentHealth += (maxHealth - currentHealth);
		StartCoroutine(TransitionHealthSlider ());
	}

	void Die() {
		Debug.Log ("YOU DIED");
		gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		GameObject.Find ("GameOverScreen").GetComponent<GameOverScreen>().ShowGameover();
	}
}