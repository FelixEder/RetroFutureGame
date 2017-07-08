using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public Sprite open, close;
	//	public GameObject cover1, cover2;
	Color cover1Color, cover2Color;
	public int health;
	bool becoming;

	void Start() {
		//		cover1Color = cover1.GetComponent<SpriteRenderer> ().color;
		//		cover2Color = cover2.GetComponent<SpriteRenderer> ().color;
	}

	public void SetInvisible() {
		GetComponent<SpriteRenderer>().sprite = open;
		GetComponent<Collider2D>().isTrigger = true;
		//		StartCoroutine(UncoverMap ());
	}

	void SetVisible() {
		GetComponent<SpriteRenderer>().sprite = close;
		GetComponent<Collider2D>().isTrigger = false;
	}

	void OnBecameInvisible() {
		Invoke("SetVisible", 20);
		//		CoverMap ();
	}

	void OnBecameVisible() {
		CancelInvoke("SetVisible");
	}

	/*	IEnumerator UncoverMap() {
			for (float a = 1; a > 0; a -= 0.05f) {
				if (cover1 != null && cover1Color.a > 0)
					cover1.GetComponent<SpriteRenderer> ().color = new Color (cover1Color.r, cover1Color.g, cover1Color.b, a);
				if (cover2 != null && cover2Color.a > 0)
					cover2.GetComponent<SpriteRenderer> ().color = new Color (cover2Color.r, cover2Color.g, cover2Color.b, a);
				yield return new WaitForSeconds (0.01f);
			}
			if (cover1 != null)
				cover1.GetComponent<SpriteRenderer> ().color = new Color (cover1Color.r, cover1Color.g, cover1Color.b, 0);
			if (cover2 != null)
				cover2.GetComponent<SpriteRenderer> ().color = new Color (cover2Color.r, cover2Color.g, cover2Color.b, 0);
		}

		void CoverMap() {
			if (cover1 != null && !cover1.GetComponent<MapCover> ().isInside)
				cover1.GetComponent<SpriteRenderer> ().color = new Color (cover1Color.r, cover1Color.g, cover1Color.b, 1);
			if (cover2 != null && !cover2.GetComponent<MapCover> ().isInside)
				cover2.GetComponent<SpriteRenderer> ().color = new Color (cover2Color.r, cover2Color.g, cover2Color.b, 1);
		}
	*/
}