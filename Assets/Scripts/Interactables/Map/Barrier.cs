using UnityEngine;
using System.Collections;

public class Barrier : MonoBehaviour {
	//The speciality of this door, is given in the editor as indicated by which item is needed to destroy it.
	//Ex. "rock" for doors that can only be destroyed by regular doors.
	public string barrierType;
	public int health;
	public Sprite sprite;
	bool broken;

	/**
	 * The barrier of the door has been broken, it is now destroyed.
	 */
	void Broken() {
		broken = true;
		//Play animation and 
		if(barrierType == "Branch") {
			GetComponent<SpriteRenderer>().sprite = sprite;
			StartCoroutine(Lower());
		}
		else //TODO Here cool animations could also be added when the barrier gets destroyed
			Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "PickupableItem" && col.gameObject.GetComponent<PickUpableItem>().GetItemType() == barrierType && col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude >= 1.0f) {
			TakeDamage(col.gameObject.GetComponent<PickUpableItem>().damage);
			if(col.gameObject.GetComponent<PickUpableItem>().GetItemType() == "Branch")
				col.gameObject.GetComponent<PickUpableItem>().Break();
		}
	}

	public void TakeDamage(int damage) {
		if(broken)
			return;
		health -= damage;
		if(health <= 0) {
			Broken();
		}
	}

	/**Returns the barrier type.*/
	public string GetBarrierType() {
		return barrierType;
	}

	IEnumerator Lower() {
		GetComponent<AudioPlayer>().PlayClip(0, 0.6f);
		yield return new WaitForSeconds(0.5f);
		for(int i = 0; i < GetComponent<SpriteRenderer>().size.y / 0.05f + 2; i++) {
			transform.position = new Vector2(transform.position.x, transform.position.y - 0.05f);
			yield return 0;
		}
	}
}