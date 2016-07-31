using UnityEngine;
using System.Collections;

public class Phase1 : MonoBehaviour {
	public float moveSpeed, knockForce;
	public int health, damage;
	public Sprite normal, kickPunching, fell;
	bool stunned;
	Rigidbody2D rb2D;
	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		//Implement the simple movement of the boss here
	}

	void OnCollisionEnter2D(Collision2D col) {
		switch (col.gameObject.tag) {
		case "Wall":
			//Here the enemy should start moving towards the player instead
			break;

		case "Char":
			KickPunching ();
			break;

		case "Rock":
			KickPunching ();
			break;
		}
	}

	public void Stunned() {
		Debug.Log ("Boss is stunned!");
		//Stuns the boss a few seconds and makes it drop a few pick-Ups
		stunned = true;
		for (int i = 0; i < 3; i++) {
			Instantiate (Resources.Load ("HealthDrop"), transform.position, Quaternion.identity);
			Instantiate (Resources.Load ("EnergyDrop"), transform.position, Quaternion.identity);
		}
		Invoke ("UnStunned", 3f);
	}

	void UnStunned() {
		Debug.Log ("Boss is UnStunned");
		stunned = false;
	}

	void Fall() {
		//Makes the boss fall down
	}

	void KickPunching() {
		if (!stunned) {
			GetComponent<SpriteRenderer> ().sprite = kickPunching;
			this.gameObject.transform.GetChild (0).gameObject.SetActive (true);
			Invoke ("FinishKickPunching", 1f);
		}
	}

	void FinishKickPunching() {
		GetComponent<SpriteRenderer> ().sprite = normal;
		this.gameObject.transform.GetChild (0).gameObject.SetActive (false);	
	}
}
