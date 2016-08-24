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
			if(Random.Range(0,2) == 0) {
				GetMirrored ();
				ResetDeltaX ();
			}
			else {
				GetMirrored ();
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
			//If jumpspeed is high enough, do damage and knock.
			//Otherwise do a bite attack
				//Should also knock the player away
				//Should do bite attack here
			break;

		case "PickupableItem":
			if (col.gameObject.GetComponent<PickUpableItem> ().GetItemType () == "Rock") {
				//Maybe play grunt
				Debug.Log ("Threw rock at boss!");
				if (Random.Range (0, 2) == 0) 
					Instantiate (Resources.Load ("HealthDrop"), transform.position, Quaternion.identity);
				else
					Instantiate (Resources.Load ("EnergyDrop"), transform.position, Quaternion.identity);
				Destroy (col.gameObject);
				Charge ();
			}
			break;

		case "Wall":
			GetMirrored ();
			ResetDeltaX ();
			break;

		case "SecretTrigger":
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

	void Charge() {
		//FinalBoss is charging, play relevant things
		Debug.Log("FinalBoss is charging");
		transform.GetChild (0).GetComponent<Phase2Head> ().OpenMouth (3f);
		moveSpeed += 5;
		damage += 2;
		Invoke ("StopCharge", 1f);
	}

	void StopCharge() {
		Debug.Log ("FinalBoss stopped rushing");
		//FinalBoss stops charging, play relevant things
		damage -= 2;
		moveSpeed -= 5;
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
