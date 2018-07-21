using UnityEngine;
using System.Collections;

public class Phase3Head : MonoBehaviour {
	public float knockForce, jumpSpeed;
	float moveSpeed = 4;
	public int health = 3, spawnLoc = 2;
	public Sprite normal, biting;
	public bool isMirrored = false;
	Rigidbody2D rb2D;
	public int deltaX, damage;
	bool invulnerable;

	// Use this for initialization
	void Start() {
		rb2D = GetComponent<Rigidbody2D>();
		InvokeRepeating("Charge", 5f, 5f);
		InvokeRepeating("Spit", 3f, 10f);
	}

	void FixedUpdate() {
		if(!isMirrored) {
			rb2D.velocity = new Vector2(moveSpeed, rb2D.velocity.y);
		}
		else {
			rb2D.velocity = new Vector2(-1 * moveSpeed, rb2D.velocity.y);
		}
	}

	// Update is called once per frame
	void Update() {
		if(Mathf.Abs(deltaX - (int) transform.position.x) >= 10) {
			if(Random.Range(0, 5) == 0) {
				GetMirrored();
				ResetDeltaX();
			}
			else {
				ResetDeltaX();
			}
		}
	}

	public void ResetDeltaX() {
		deltaX = (int) transform.position.x;
	}

	void OnCollisionEnter2D(Collision2D col) {
		switch(col.gameObject.tag) {
			case "Player":
				//Does the speed need to be higher or lower?
				if(rb2D.velocity.y > 5f) {
					col.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
				}
				else {
					Bite();
				}
				col.gameObject.GetComponent<PlayerHealth>().Knockback(gameObject, knockForce);
				break;

			case "PickupableItem":
				if(col.gameObject.GetComponent<PickUpableItem>().GetItemType() == "Rock") {
					//Maybe play grunt
					Debug.Log("Threw rock at boss!");
					Charge();
				}
				break;

			case "Wall":
				GetMirrored();
				ResetDeltaX();
				break;
		}
	}

	/**
	 * Mirrors the boss and therefor makes it change direction.
	 */
	void GetMirrored() {
		if(!isMirrored) {
			transform.rotation = Quaternion.Euler(0, 180, 0);
			isMirrored = true;
		}
		else {
			transform.rotation = Quaternion.Euler(0, 0, 0);
			isMirrored = false;
		}
	}

	public void Spit() {
		Bite();
		transform.GetChild(0).GetComponent<FinalBossSpitAttack>().enabled = true;
		Invoke("DoneSpit", 4f);
	}

	void DoneSpit() {
		transform.GetChild(0).GetComponent<FinalBossSpitAttack>().enabled = false;
	}

	public void Jump() {
		if(Random.Range(0, 100) < 1) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpSpeed);
		}
	}

	void Charge() {
		//FinalBoss is charging, play relevant things
		Debug.Log("FinalBossHead is charging");
		Bite();
		moveSpeed += 5;
		Invoke("StopCharge", 1f);
	}

	void StopCharge() {
		Debug.Log("FinalBoss stopped rushing");
		//FinalBoss stops charging, play relevant things
		moveSpeed -= 5;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag.Equals("Player")) {
			col.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage + 2, gameObject, knockForce);
		}
	}

	void Bite() {
		GetComponent<SpriteRenderer>().sprite = biting;
		GetComponent<CircleCollider2D>().enabled = true;
		Invoke("CloseBite", 3f);
	}

	void CloseBite() {
		GetComponent<SpriteRenderer>().sprite = normal;
		GetComponent<CircleCollider2D>().enabled = false;
		GetMirrored();
	}

	public void TakeDamage() {
		if(!invulnerable) {
			Debug.Log("Hurt final boss");
			health--;
			Charge();
			moveSpeed++;
			knockForce++;
			jumpSpeed++;
			if(health <= 0) {
				Defeated();
			}
			StartCoroutine(Invulnerable());
		}
		else
			Debug.Log("Boss is invulnerable");
	}

	void Defeated() {
		//TODO Blow up the face and start the count-down
		GameObject.Find("AreaTitle").GetComponent<AreaTitle>()
			.SetBossDefeatText("Final Boss Defeated");
		for(int i = 0; i < 9; i++) {
			Instantiate(Resources.Load("HealthDrop"), transform.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0), Quaternion.identity);
			Instantiate(Resources.Load("EnergyDrop"), transform.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0), Quaternion.identity);
		}
		Destroy(gameObject);
		//Start the countdown somehow
	}

	IEnumerator Invulnerable() {
		invulnerable = true;
		yield return new WaitForSeconds(5f);
		invulnerable = false;
	}
}