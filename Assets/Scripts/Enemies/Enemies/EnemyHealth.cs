using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
	public int health;
	public float invulnerabilityTime;
	public Material deathMaterial;

	bool invulnerable;
	AudioPlayer audioplay;

	void Start() {
		audioplay = GetComponent<AudioPlayer>();
	}

	void OnTriggerStay2D(Collider2D col) {
		if(col.gameObject.tag.Equals("Water"))
			TakeDamage(Random.Range(0, 2));
	}

	public void TakeDamage(int damage, GameObject attacker, float knockbackForce) {
		Knockback(attacker, knockbackForce);
		TakeDamage(damage);
	}

	public void TakeDamage(int damage) {
		if(!invulnerable) {
			//Play a sound and animation.
			audioplay.PlayClip(0, 1, 0.5f, 1.5f);
			health -= damage;
			StartCoroutine(Invulnerability());
			if(health <= 0)
				StartCoroutine(Die());
		}
	}

	public void Knockback(GameObject attacker, float force) {
		if(!invulnerable)
			GetComponent<Rigidbody2D>().velocity = new Vector2(force * Mathf.Sign(transform.position.x - attacker.transform.position.x), force / 2);
	}

	IEnumerator Invulnerability() {
		invulnerable = true;
		yield return new WaitForSeconds(invulnerabilityTime);
		invulnerable = false;
	}

	IEnumerator Die() {
		audioplay.PlayDetached(1, 1, 0.5f, 1.5f);
		GetComponent<SpriteRenderer>().material = deathMaterial;
		yield return new WaitForSeconds(0.2f);
		int ranNumb = Random.Range(0, 60);
		if(ranNumb < 20) {
			Instantiate(Resources.Load("HealthDrop"), transform.position, Quaternion.identity);
		}
		else if(ranNumb < 40) {
			Instantiate(Resources.Load("EnergyDrop"), transform.position, Quaternion.identity);
		}
		Destroy(this.gameObject);
	}
}
