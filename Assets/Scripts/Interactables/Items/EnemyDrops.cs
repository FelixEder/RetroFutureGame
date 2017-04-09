using UnityEngine;
using System.Collections;

public class EnemyDrops : MonoBehaviour {
	public string dropType;
	float livedTime;

	void Start() {
		StartCoroutine (Decay ());
	}

	void Update() {
		livedTime += Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag.Equals("Char")) {
			//Play correct music and animation depending on what upgrade is choosen
			switch (dropType) {
			//Add more switch-statements as more drops are implemented in the game.
				case "Health":
					col.gameObject.GetComponent<CharHealth> ().IncreaseCurrentHealth (1);
					break;

				case "Energy":
					col.gameObject.GetComponent<CharEnergy> ().IncreaseCurrentEnergy (1);
					break;
			}
			Destroy (gameObject);
		}
	}

	IEnumerator Decay() {
		while (livedTime < 15) {
			yield return 0;
		}
		GetComponent<Rigidbody2D> ().gravityScale = 0.01f;
		GetComponent<ParticleSystem> ().Stop ();
		while (livedTime < 20 && transform.localScale.x > 0) {
			transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime * 1.5f, transform.localScale.y - Time.deltaTime * 1.5f, 1);
			yield return 0;
		}
		Destroy (gameObject);
	}
}