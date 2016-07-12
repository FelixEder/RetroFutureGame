using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public Sprite open, close;
	public GameObject cover1, cover2;
	public int health;

	public void SetInvisible() {
		GetComponent<SpriteRenderer> ().sprite = open;
		GetComponent<Collider2D> ().isTrigger = true;
		UncoverMap ();
	}

	void OnBecameInvisible() {
		GetComponent<SpriteRenderer> ().sprite = close;
		GetComponent<Collider2D> ().isTrigger = false;
		CoverMap ();
	}

	void UncoverMap() {
		if (cover1 != null)
			cover1.GetComponent<SpriteRenderer> ().color = new Color (cover1.GetComponent<SpriteRenderer> ().color.r, cover1.GetComponent<SpriteRenderer> ().color.g, cover1.GetComponent<SpriteRenderer> ().color.b, 0);
		if (cover2 != null)
			cover2.GetComponent<SpriteRenderer> ().color = new Color (cover2.GetComponent<SpriteRenderer> ().color.r, cover2.GetComponent<SpriteRenderer> ().color.g, cover2.GetComponent<SpriteRenderer> ().color.b, 0);
	}

	void CoverMap() {
		if (cover1 != null && !cover1.GetComponent<MapCover> ().isInside)
			cover1.GetComponent<SpriteRenderer> ().color = new Color (cover1.GetComponent<SpriteRenderer> ().color.r, cover1.GetComponent<SpriteRenderer> ().color.g, cover1.GetComponent<SpriteRenderer> ().color.b, 1);
		if (cover2 != null && !cover2.GetComponent<MapCover>().isInside)
			cover2.GetComponent<SpriteRenderer> ().color = new Color (cover2.GetComponent<SpriteRenderer> ().color.r, cover2.GetComponent<SpriteRenderer> ().color.g, cover2.GetComponent<SpriteRenderer> ().color.b, 1);
	}
}