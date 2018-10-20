using UnityEngine;
using System.Collections;
using Cinemachine;

public class BigBadBird : MonoBehaviour {
	public Sprite regular, spitting, winging;
	public float moveSpeed, knockForce, vertDir;
	public bool isMirrored, isSpitting, mirrorCooldown, forceY;
	public int health = 6, damage;
	Vector3 followPos;
	SpriteRenderer spriteRend;
	//Should it really use a rigidBody?
	Rigidbody2D rb2D;
	GameObject player;
	float attackTimer;
	bool attacking;
	int spitChance = 100;

	void Start() {
		rb2D = GetComponent<Rigidbody2D>();
		player = GameObject.Find("Player");
		spriteRend = transform.GetChild(0).GetComponent<SpriteRenderer>();
		spriteRend.sprite = regular;
		//Set the start values here
		//Also set the standard sprite.

		GameObject.Find("BirdBoss target").GetComponent<CinemachineTargetGroup>().m_Targets[0].target = transform.GetChild(2);
	}

	void Update() {
		if(health <= 0)
			Defeated();
		if(Random.Range(0, spitChance) < 1 && !isSpitting)
			SpitAttack();
			

		
		if(Vector2.Distance(transform.position, followPos) < 5) {
			attackTimer += Time.deltaTime;
		}
		else if (attackTimer != 0)
			attackTimer = 0;
			
		if(attackTimer > 1 && !attacking)
			WingAttack();
	}

	void FixedUpdate() {
		Vector3 B = new Vector3(
			Mathf.Lerp(transform.position.x, player.transform.position.x, 0.5f), 
				Mathf.Lerp(transform.position.y, player.transform.position.y, 0.5f) + Mathf.Abs(transform.position.x - player.transform.position.x),
					15);

		Vector2 AtoB = Vector2.Lerp(transform.position, B, 0.5f);
		Vector2 BtoC = Vector2.Lerp(B, player.transform.position, 0.5f);
		Vector3 final = Vector2.Lerp(AtoB, BtoC, 0.5f);

		final += Vector3.forward * 15;
		followPos = new Vector3(player.transform.GetChild(7).position.x, player.transform.GetChild(7).position.y, 15);
		transform.position = Vector3.Lerp(transform.position, followPos, moveSpeed);
	}

	void OnCollisionEnter2D(Collision2D col) {
		switch(col.gameObject.tag) {
			case "Wall":
				GetMirrored();
				WingAttack();
				break;

			case "Platform":
				GetMirrored();
				WingAttack();
				break;

			case "Player":
				col.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage, gameObject.transform.position, knockForce);
				GetMirrored();
				WingAttack();
				break;

			case "PickupableItem":
				switch(col.gameObject.GetComponent<PickUpableItem>().GetItemType()) {

					case "Branch":
						WingAttack();
						break;

					case "Rock":
						SpitAttack();
						break;
				}
				break;

			case "BirdBossUp":
				StopCoroutine("OverrideY");
				StartCoroutine(OverrideY(10f));
				break;

			case "BirdBossDown":
				StopCoroutine("OverrideY");
				StartCoroutine(OverrideY(-10f));
				break;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.CompareTag("BirdBossUp")) {
			StopCoroutine("OverrideY");
			StartCoroutine(OverrideY(10f));
		}
		else if(col.gameObject.CompareTag("BirdBossDown")) {
			StopCoroutine("OverrideY");
			StartCoroutine(OverrideY(-10f));
		}
	}

	//Call this method when the boss gets mad and changes some things.
	void GetAngry() {
		//PLay fitting animation and sound
		//Change color or entire sprite
		moveSpeed += 3;
		knockForce += 3;
		spitChance = 50;
	}

	/**
	 * Mirrors the enemy and therefor makes it change direction.
	 */
	void GetMirrored() {
		if(mirrorCooldown)
			return;
		mirrorCooldown = true;
		if(!isMirrored) {
			isMirrored = true;
			StartCoroutine(Rotate(180));
		}
		else {
			isMirrored = false;
			StartCoroutine(Rotate(0));
		}
	}

	IEnumerator Rotate(int rotation) {
		moveSpeed = 1;
		yield return new WaitForSeconds(1f);
		transform.rotation = Quaternion.Euler(0, rotation, 0);
		moveSpeed = 5;
		yield return new WaitForSeconds(1f);
		mirrorCooldown = false;
	}

	void WingAttack() {
		CancelInvoke("FinishWingAttack");

		attackTimer = 0;
		attacking = true;
		GetComponent<Animator>().SetTrigger("attack");
		//Make some swooshing animation with feather

		//If player is hit by the hitbox, he will get hurt
		this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
		spriteRend.sprite = winging;
		Invoke("FinishWingAttack", 0.6f);
	}

	void FinishWingAttack() {
		attacking = false;
		GetComponent<Animator>().SetTrigger("fly");

		this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
		spriteRend.sprite = regular;
	}

	void SpitAttack() {
		isSpitting = true;
		CancelInvoke("FinishSpitAttack");
		this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
		//		GetComponent<SpriteRenderer> ().sprite = spitting;
		Invoke("FinishSpitAttack", 1f);
	}

	void FinishSpitAttack() {
		//		GetComponent<SpriteRenderer> ().sprite = regular;
		this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
		isSpitting = false;
		//Play animation that closes mouth
	}

	public void TakeDamage() {
		health--;
		if(health == 3)
			GetAngry();
		//PLay fitting animation and sound
		//Should flash in some color in order to show that the player hurt it
	}

	void Defeated() {
		//PLay fitting animation and sound
		GameObject.Find("AreaTitle").GetComponent<AreaTitle>()
			.SetBossDefeatText("BigBadBird Defeated");
		for(int i = 0; i < 6; i++) {
			Instantiate(Resources.Load("HealthDrop"), transform.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0), Quaternion.identity);
			Instantiate(Resources.Load("EnergyDrop"), transform.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0), Quaternion.identity);
		}
		Destroy(this.gameObject.transform.parent.gameObject);
	}

	IEnumerator OverrideY(float y) {
		forceY = true;
		vertDir = y;
		yield return new WaitForSeconds(1f);
		forceY = false;
	}
}