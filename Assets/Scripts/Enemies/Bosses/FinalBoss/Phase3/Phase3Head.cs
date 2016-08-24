using UnityEngine;
using System.Collections;

public class Phase3Head : MonoBehaviour {
	public float knockForce, jumpSpeed;
	float moveSpeed = 4;
	public int health;
	public Sprite normal, biting;
	public bool isMirrored = false;
	Rigidbody2D rb2D;
	public int deltaX, damage;

	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D> ();
		InvokeRepeating ("Charge", 5f, 5f);
	}

	void FixedUpdate() {
		if (!isMirrored) {
			rb2D.velocity = new Vector2 (moveSpeed, rb2D.velocity.y);
		} else {
			rb2D.velocity = new Vector2 (-1 * moveSpeed, rb2D.velocity.y);
		}
	}

	// Update is called once per frame
	void Update () {
		if(Mathf.Abs(deltaX - (int) transform.position.x) >= 10) {
			if(Random.Range(0,5) == 0) {
				GetMirrored ();
				ResetDeltaX ();
			}
			else {
				ResetDeltaX ();
			}
		}
	}

	public void ResetDeltaX() {
		deltaX = (int) transform.position.x;
	}
		
	void OnCollisionEnter2D(Collision2D col) {
		switch (col.gameObject.tag) {
		case "Char":
			//Does the speed need to be higher or lower?
			if (rb2D.velocity.y > 5f) {
				col.gameObject.GetComponent<CharHealth> ().TakeDamage (damage);
			}
			else {
				Bite ();
			}
			col.gameObject.GetComponent<CharKnockback> ().Knockback (gameObject, knockForce);
			break;

		case "PickupableItem":
			if (col.gameObject.GetComponent<PickUpableItem> ().GetItemType () == "Rock") {
				//Maybe play grunt
				Debug.Log ("Threw rock at boss!");
				Charge ();
			}
			break;

		case "Wall":
			GetMirrored ();
			ResetDeltaX ();
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

	public void Jump () {
		if (Random.Range (0, 100) < 1) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, jumpSpeed);
		}
	}

	void Charge() {
		//FinalBoss is charging, play relevant things
		Debug.Log("FinalBossHead is charging");
		transform.GetChild (0).GetComponent<Phase2Head> ().OpenMouth (3f);
		moveSpeed += 5;
		Bite ();
		Invoke ("StopCharge", 1f);
	}

	void StopCharge() {
		Debug.Log ("FinalBoss stopped rushing");
		//FinalBoss stops charging, play relevant things
		moveSpeed -= 5;
	}

	void OnTriggerEnter2D(Collider2D snack) {
		if (snack.gameObject.tag.Equals ("Char"))
			snack.gameObject.GetComponent<CharHealth> ().TakeDamage (damage + 2);
			snack.gameObject.GetComponent<CharKnockback> ().Knockback (gameObject, knockForce);
	}

	void Bite() {
		GetComponent<SpriteRenderer> ().sprite = biting;
		GetComponent<CircleCollider2D> ().enabled = true;
		Invoke ("CloseBite", 3f);
	}

	void CloseBite() {
		GetComponent<SpriteRenderer> ().sprite = normal;
		GetComponent<CircleCollider2D> ().enabled = false;
	}

	public void GetHurt(int damage) {
		health -= damage;
		//Increase all stats here
		if (health <= 0) {
			Defeated ();
		}
	}

	void Defeated() {
		//Now phase 3 starts
		for (int i = 0; i < 5; i++) {
			Instantiate (Resources.Load ("HealthDrop"), transform.position, Quaternion.identity);
			Instantiate (Resources.Load ("EnergyDrop"), transform.position, Quaternion.identity);
		}
	}
}