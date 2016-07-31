using UnityEngine;
using System.Collections;

public class SpikesfromGoogle : MonoBehaviour {
	public float knockForce;
	public int damage = 1;

	void OnCollisionStay2D(Collision2D col) {
		switch(col.gameObject.tag) {

		case "Char":
				col.gameObject.GetComponent<CharHealth> ().TakeDamage (damage);
				col.gameObject.GetComponent<Knockback> ().Knock (this.gameObject, knockForce);
			break;

		case "SmallCritter" :
			break;

		case "JumpingCritter":
			break;

		case "HardEnemy" :
			break;

		case "BigEyeGuy" :
			break;

		case "CrawlerCritter":
			break;

		case "ShellMan":
			break;

		case "Wall" :
			break;

		case "Door" :
			break;

		case "Rock":
			break;

		case "Branch":
			break;

		}
	}
}