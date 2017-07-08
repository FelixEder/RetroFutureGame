using UnityEngine;
using System.Collections;

public class SmallCritter : MonoBehaviour {
	Rigidbody2D rb2D;
	GameObject player;

	void Start() {
		audioplay = GetComponent<AudioPlayer>();
		player = GameObject.Find("Char");
		rb2D = GetComponent<Rigidbody2D>();
	}

	void OnBecameVisible() {
		if(initialFreezeTime == 0)
			InitializeMoveSpeed();
	}

	void OnCollisionEnter2D(Collision2D col) {
		switch(col.gameObject.tag) {

			case "Char":
				col.gameObject.GetComponent<CharHealth>().TakeDamage(damage, gameObject, knockForce);

				break;
		}
	}

	void InitializeMoveSpeed() {
		activeMoveSpeed = moveSpeed;
	}
}