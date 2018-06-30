using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public int currentHealth, maxHealth;
	public float invulnerabilityTime;
	bool dead;

	public Slider slider;
	PlayerStatus status;
	PlayerInventory inventory;
	Rigidbody2D rb2D;
	AudioPlayer audioplay;

	void Start() {
		status = GetComponent<PlayerStatus>();
		inventory = GetComponent<PlayerInventory>();
		rb2D = GetComponent<Rigidbody2D>();

		audioplay = GetComponent<AudioPlayer>();
		SetHealthSliderSize();
		SetHealthSlider();
	}

	public void TakeDamage(int damage) {
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
			TransitionHealthSlider();
		}
	}

	public void TakeDamage(int damage, GameObject attacker, float knockbackForce) {
		Knockback(attacker, knockbackForce);
		TakeDamage(damage);
	}

	public void Knockback(GameObject attacker, float force) {
		if(!status.Invulnerable()) {
			GameObject.Find("InputManager").GetComponent<InputManager>().Disable(0.1f);
			GetComponent<PlayerMovement>().Stun(0.3f);
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
		TransitionHealthSlider();
	}

	public void IncreaseMaxHealth() {
		maxHealth++;
		currentHealth = maxHealth;
		SetHealthSliderSize();
		TransitionHealthSlider();
	}

	public void SetHealthSlider() {
		slider.value = (float) currentHealth / maxHealth;
	}

	void SetHealthSliderSize() {
		GameObject.Find("healthSlider").GetComponent<RectTransform>().sizeDelta = new Vector2(8 + 32 * (float) maxHealth, 32);
	}

	void TransitionHealthSlider() {
		StopCoroutine(TransitionSlider());
		StartCoroutine(TransitionSlider());
	}

	IEnumerator TransitionSlider() {
		while(slider.value * (float) maxHealth < currentHealth - 0.1 || slider.value * (float) maxHealth > currentHealth + 0.1) {
			slider.value = Mathf.Lerp(slider.value, (float) currentHealth / maxHealth, Time.deltaTime * 8);
			yield return new WaitForSeconds(0.01f);
		}
		SetHealthSlider();
	}

	public void MaximizeHealth() {
		currentHealth = maxHealth;
		TransitionHealthSlider();
	}

	void Die() {
		Debug.Log("YOU DIED");
		dead = true;
		
		GetComponent<Animator>().SetBool("dead", true);
		
		rb2D.constraints = RigidbodyConstraints2D.None;
		rb2D.AddForce(Vector2.up * 300);
		rb2D.angularVelocity = 90;
		audioplay.PlayDetached(Random.Range(0, 5), 1f, 1f, 1f);
		audioplay.Mute(true);
		GameObject.Find("GameOverScreen").GetComponent<GameOverScreen>().Gameover();
	}

	public void Revive() {
		Debug.Log("REVIVED");
		dead = false;
		
		//Maximize health and energy.
		MaximizeHealth();
		GetComponent<PlayerEnergy>().MaximizeEnergy();
		
		//Fix sprite and audio
		GetComponent<Animator>().SetBool("dead", false);
		status.isMirrored = false;
		if(GetComponent<SmallFry>().enabled)
			GetComponent<SmallFry>().GrowBig();
		
		//Place player at last checkpoint and set transforms.
		rb2D.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
		transform.position = GetComponent<Checkpoint>().activeCheckpoint.transform.position;
		transform.position += new Vector3(0, 1, 0);
		transform.rotation = Quaternion.Euler(0, 0, 0);
		
		//Keep immovable and muted and set invulnerable for duration.
		status.CancelInvoke("SetVulnerable");
		status.Invulnerable(2f);
		Invoke("FinishRevive", 1.5f);
	}
	
	void FinishRevive() {
		audioplay.Mute(false);
		rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
		rb2D.velocity = new Vector2(0, 0);
	}
}