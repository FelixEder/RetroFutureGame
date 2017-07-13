using UnityEngine;
using System.Collections;

public class SpikesfromGoogle : MonoBehaviour {
	public float knockForce;
	public int damage = 1;

	void OnCollisionStay2D(Collision2D col) {
		switch(col.gameObject.tag) {
			case "Player":
				if(!col.gameObject.GetComponent<PlayerStatus>().Invulnerable())
					col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * knockForce, ForceMode2D.Impulse);
				col.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
				break;

			case "SmallCritter":
			case "JumpingCritter":
		//	case "HardCritter":
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