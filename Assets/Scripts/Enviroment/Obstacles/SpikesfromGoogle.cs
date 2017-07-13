using UnityEngine;
using System.Collections;

public class SpikesfromGoogle : MonoBehaviour {
	public float knockForce;
	public int damage = 1;

	void OnCollisionStay2D(Collision2D col) {
		switch(col.gameObject.tag) {
			case "Player":
				col.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage, gameObject, knockForce);
				break;

			case "SmallCritter":
			case "JumpingCritter":
			case "HardCritter":
			case "BigEyeGuy":
			case "CrawlerCritter":
			case "ShellMan":
				col.gameObject.GetComponent<EnemyHealth>().TakeDamage(99);
				break;

			case "PickupableItem":
				col.gameObject.GetComponent<PickUpableItem>().Break();
				break;
			case "Wall":
			case "Door":
				break;
		}
	}
}