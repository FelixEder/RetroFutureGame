using UnityEngine;
using System.Collections;

public class ForegroundCover : MonoBehaviour {
	Color spriteColor;
	public GameObject cover;

	void Start() {
		spriteColor = cover.GetComponent<SpriteRenderer> ().color;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name == "char")
			StartCoroutine (Hide ());
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.name == "char") 
			StartCoroutine (Show ());
	}

	IEnumerator Hide() {
		for (float a = 1; a > 0; a -= 0.05f) {
			GetComponent<SpriteRenderer> ().color = new Color (spriteColor.r, spriteColor.g, spriteColor.b, a);
			yield return new WaitForSeconds (0.01f);
		}
		GetComponent<SpriteRenderer> ().color = new Color (spriteColor.r, spriteColor.g, spriteColor.b, 0);
	}

	IEnumerator Show() {
		for (float a = 0; a < 1; a += 0.05f) {
			GetComponent<SpriteRenderer> ().color = new Color (spriteColor.r, spriteColor.g, spriteColor.b, a);
			yield return new WaitForSeconds (0.01f);
		}
		GetComponent<SpriteRenderer> ().color = new Color (spriteColor.r, spriteColor.g, spriteColor.b, 1);
	}
}
