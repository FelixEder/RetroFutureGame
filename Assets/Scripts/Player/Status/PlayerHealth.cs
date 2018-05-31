using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public int currentHealth, maxHealth;
	public float invulnerabilityTime;
	bool dead;

	Slider slider;
	PlayerStatus status;
	PlayerInventory inventory;
	Rigidbody2D rb2D;
	AudioPlayer audioplay;

	void Start() {
		status = GetComponent<PlayerStatus>();
		inventory = GetComponent<PlayerInventory>();
		rb2D = GetComponent<Rigidbody2D>();

		audioplay = GetComponent<AudioPlayer>();
		slider = GameObject.Find("healthSlider").GetComponent<Slider>();
		SetHealthSliderSize();
		SetHealthSlider();
	}

	public void TakeDamage(int damage) {
		//Maybe give a few seconds invincibility and make sprite blink or so?
		if(!status.Invulnerable()) {
			audioplay.PlayClip(Random.Range(0, 5), 0.5f);

			if(currentHealth - damage <= 0) {
				currentHealth = 0;
				if(!dead)
					Die();
			}
			else
				currentHealth -= damage;
			status.Invulnerable(invulnerabilityTime);
			StartCoroutine(TransitionHealthSlider());
		}
	}

	public void TakeDamage(int damage, GameObject attacker, float knockbackForce) {
		Knockback(attacker, knockbackForce);
		TakeDamage(damage);
	}

	public void Knockback(GameObject attacker, float force) {
		if(!GetComponent<PlayerStatus>().Invulnerable()) {
			GameObject.Find("InputManager").GetComponent<InputManager>().Disable(0.2f);
			if(transform.position.x < attacker.transform.position.x)
				GetComponent<Rigidbody2D>().velocity = new Vector2(-force, 2);
			else
				GetComponent<Rigidbody2D>().velocity = new Vector2(force, 2);
			//Drops the item the player is holding.
			if(inventory.IsHoldingItem()) {
				inventory.GetHoldingItem().GetComponent<PickUpableItem>().Drop(false);
				inventory.SetHoldingItem(null);
			}
		}
	}

	public void IncreaseCurrentHealth(int amount) {
		currentHealth += amount;
		if(currentHealth > maxHealth)
			currentHealth = maxHealth;
		StartCoroutine(TransitionHealthSlider());
	}

	public void IncreaseMaxHealth() {
		maxHealth++;
		currentHealth = maxHealth;
		SetHealthSliderSize();
		StartCoroutine(TransitionHealthSlider());
	}

	public void SetHealthSlider() {
		slider.value = (float) currentHealth / maxHealth;
	}

	void SetHealthSliderSize() {
		GameObject.Find("healthSlider").GetComponent<RectTransform>().sizeDelta = new Vector2(8 + 32 * (float) maxHealth, 32);
	}

	IEnumerator TransitionHealthSlider() {
		float transitionSpeed = (maxHealth - Mathf.Abs(slider.value * maxHealth - currentHealth)) / 2;
		while(slider.value * (float) maxHealth < currentHealth - 0.1 || slider.value * (float) maxHealth > currentHealth + 0.1) {
			slider.value = Mathf.Lerp(slider.value, (float) currentHealth / maxHealth, Time.deltaTime * transitionSpeed);
			yield return new WaitForSeconds(0.01f);
		}
		SetHealthSlider();
	}

	public void MaximizeHealth() {
		SetHealthSlider();
		currentHealth = maxHealth;
		StartCoroutine(TransitionHealthSlider());
	}

	void Die() {
		Debug.Log("YOU DIED");
		dead = true;
		audioplay.Mute(true);
		rb2D.constraints = RigidbodyConstraints2D.None;
		rb2D.AddForce(Vector2.up * 300);
		rb2D.angularVelocity = 90;
		SetHealthSlider();
		audioplay.PlayClip(Random.Range(0, 5), 1f);
		transform.GetChild(0).GetComponent<Animator>().SetBool("dead", true);
		GameObject.Find("GameOverScreen").GetComponent<GameOverScreen>().Gameover();
	}

	public void Revive() {
		Debug.Log("The angels have granted your wish");
		dead = false;
		audioplay.Mute(false);
		rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
		rb2D.velocity = new Vector2(0, 0);
		transform.rotation = Quaternion.Euler(0, 0, 0);
		currentHealth = maxHealth;
		SetHealthSlider();
		transform.GetChild(0).GetComponent<Animator>().SetBool("dead", false);
		GetComponent<PlayerEnergy>().MaximizeEnergy();
		status.isMirrored = false;
	}
}