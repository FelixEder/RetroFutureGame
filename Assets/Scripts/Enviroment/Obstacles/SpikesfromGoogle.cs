using UnityEngine;
using System.Collections;

public class SpikesfromGoogle : MonoBehaviour {
	public float knockForce;
	public int damage = 1;

	void OnCollisionStay2D(Collision2D col) {
		switch(col.gameObject.tag) {

		case "Char":
			col.gameObject.GetComponent<CharHealth> ().TakeDamage (damage, gameObject, knockForce);
			break;

		case "SmallCritter":
			col.gameObject.GetComponent<SmallCritter>().Knockback(gameObject, 0);
			col.gameObject.GetComponent<SmallCritter> ().TakeDamage (99);
			break;

		case "JumpingCritter":
		case "HardEnemy":
		case "BigEyeGuy":
		case "CrawlerCritter":
		case "ShellMan":
		case "Wall":
		case "Door":
			break;

		case "PickupableItem":
			col.gameObject.GetComponent<PickUpableItem> ().Break ();
			break;

		}
	}
}