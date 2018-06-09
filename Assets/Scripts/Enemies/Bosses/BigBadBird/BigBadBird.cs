using UnityEngine;
using System.Collections;
using Cinemachine;

public class BigBadBird : MonoBehaviour {
	public Sprite regular, spitting, winging;
	public float moveSpeed, knockForce, vertDir;
	public bool isMirrored, isSpitting, mirrorCooldown, forceY;
	public int health = 6, damage;
	//Should it really use a rigidBody?
	Rigidbody2D rb2D;
	GameObject player;
	int spitChance = 100;

	void Start() {
		rb2D = GetComponent<Rigidbody2D>();
		player = GameObject.Find("Player");
		GetComponent<SpriteRenderer>().sprite = regular;
		//Set the start values here
		//Also set the standard sprite.

		GameObject.Find("BirdBoss target").GetComponent<CinemachineTargetGroup>().m_Targets[0].target = transform;
	}

	void Update() {
		if(health <= 0)
			Defeated();
		if(Random.Range(0, spitChance) < 1 && !isSpitting)
			SpitAttack();
	}

	void FixedUpdate() {
		if(!forceY) {
			vertDir = 0;
			if(isMirrored && transform.position.x - player.transform.position.x > 0 || !isMirrored && transform.position.x - player.transform.position.x < 0)
				vertDir = (transform.position.y - player.transform.position.y) * -1;
			else
				vertDir = 5;
		}
		if(isMirrored) {
			rb2D.velocity = new Vector2(-1 * moveSpeed, vertDir * 0.2f);
		}
		else {
			rb2D.velocity = new Vector2(moveSpeed, vertDir * 0.2f);
		}
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
				col.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage, gameObject, knockForce);
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
		//Make some swooshing animation with feather
		//If player is hit by the hitbox, he will get hurt
		//Will probably activate some child-trigger that roughly encapsulates the wing being slapped.
		this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
		GetComponent<SpriteRenderer>().sprite = winging;
		//Is there a better way to disable a child after a certain time than a Invoke-call to an unneccecary method?
		Invoke("FinishWingAttack", 0.6f);
	}

	void FinishWingAttack() {
		this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
		GetComponent<SpriteRenderer>().sprite = regular;
	}

	void SpitAttack() {
		isSpitting = true;
		CancelInvoke("FinishSpitAttack");
		this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
		//		GetComponent<SpriteRenderer> ().sprite = spitting;
		Invoke("FinishSpitAttack", 1f);
	}

	void FinishSpitAttack() {
		//		GetComponent<SpriteRenderer> ().sprite = regular;
		this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
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
		Destroy(this.gameObject.transform.parent.gameObject);
		for(int i = 0; i < 5; i++) {
			Instantiate(Resources.Load("HealthDrop"), transform.position, Quaternion.identity);
			Instantiate(Resources.Load("EnergyDrop"), transform.position, Quaternion.identity);
		}
	}

	IEnumerator OverrideY(float y) {
		forceY = true;
		vertDir = y;
		yield return new WaitForSeconds(1f);
		forceY = false;
	}
}