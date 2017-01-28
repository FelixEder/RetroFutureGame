using UnityEngine;
using System.Collections;

public class Electrohaz : MonoBehaviour {
	public float knockForce;
	public int damage = 99;

	void OnCollisionStay2D(Collision2D col) {
		switch(col.gameObject.tag) {

		case "Char":
				col.gameObject.GetComponent<CharHealth> ().TakeDamage (damage);
				col.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2(0, knockForce),ForceMode2D.Impulse);
			break;

		case "SmallCritter" :
		case "JumpingCritter":
		case "HardEnemy" :
		case "BigEyeGuy" :
		case "CrawlerCritter":
		case "ShellMan":
		case "Wall" :
		case "Door" :
		case "PickupableItem":
			break;

		}
	}
}