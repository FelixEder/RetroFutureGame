using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioPlayer))]
public class EnemyHealth : MonoBehaviour {
	public int health = 1;
	public float invulnerabilityTime = 0.5f;
	public Material deathMaterial;
	[Space(10)]
	public bool hasAudio;

	bool invulnerable;
	AudioPlayer audioplay;

	void Start() {
		if(hasAudio)
			audioplay = GetComponent<AudioPlayer>();
	}

	void OnTriggerStay2D(Collider2D col) {
		if(col.gameObject.tag.Equals("Water")) 
			TakeDamage(1);
	}

	public void TakeDamage(int damage, GameObject attacker, float knockbackForce) {
		Knockback(attacker, knockbackForce);
		TakeDamage(damage);
	}

	public void TakeDamage(int damage) {
		if(!invulnerable) {
			//Play a sound and animation.
			if(hasAudio)
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
		if(hasAudio)
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
