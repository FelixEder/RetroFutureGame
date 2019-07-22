using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioPlayer))]
public class EnemyHealth : MonoBehaviour {
    public int health = 1;
    [Header("Can be hurt by damageType")]
    public bool punch;
	public bool mega, branch, rock;
	[Space(10)]
    public float invulnerabilityTime = 0.5f;
	public Material deathMaterial;

	bool invulnerable;
	AudioPlayer audioplay;
	PlayerEnergy playerEnergy;
	SpriteRenderer sprite;

	void Start() {
		audioplay = GetComponent<AudioPlayer>();
		sprite = GetComponent<SpriteRenderer>();
		playerEnergy = GameObject.Find("Player").GetComponent<PlayerEnergy>();
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
			if(audioplay.audioClips.Length > 0)
				audioplay.PlayClip(0, 1, 0.5f, 1.5f);
			health -= damage;
			StartCoroutine(Invulnerability());
			StartCoroutine(Flash());
			if(health <= 0)
				StartCoroutine(Die());
		}
	}

	public void Knockback(GameObject attacker, float force) {
		if(!invulnerable)
			GetComponent<Rigidbody2D>().velocity = new Vector2(force * Mathf.Sign(transform.position.x - attacker.transform.position.x), force / 2);
	}

	IEnumerator Flash() {
		Color orig = sprite.color;
		sprite.color = new Color(orig.r + 0.2f, orig.g - 0.5f, orig.b - 0.5f);
		yield return new WaitForSeconds(0.2f);
		sprite.color = orig;
	}

	IEnumerator Invulnerability() {
		invulnerable = true;
		yield return new WaitForSeconds(invulnerabilityTime);
		invulnerable = false;
	}

	IEnumerator Die() {
		if(audioplay.audioClips.Length > 0)
			audioplay.PlayDetached(1, 1, 0.5f, 1.5f);
		if(GetComponent<EnemyAttack>())
			GetComponent<EnemyAttack>().enabled = false;
		GetComponent<SpriteRenderer>().material = deathMaterial;
		yield return new WaitForSeconds(0.2f);
		int ranNumb = Random.Range(0, 60);
		if(ranNumb < 25) {
			Instantiate(Resources.Load("HealthDrop"), transform.position, Quaternion.identity);
		}
		else if(ranNumb < 40 && playerEnergy.maxEnergy > 0) {
			Instantiate(Resources.Load("EnergyDrop"), transform.position, Quaternion.identity);
		}
		Destroy(this.gameObject);
	}
}
