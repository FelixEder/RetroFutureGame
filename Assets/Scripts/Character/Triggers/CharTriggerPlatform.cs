using UnityEngine;
using System.Collections;

public class CharTriggerPlatform : MonoBehaviour {
	public GameObject player, platformCollider;

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.tag == "Platform") {
			if (player.GetComponent<CharJump> ().jumpDown) {
				Physics2D.IgnoreCollision (platformCollider.GetComponent<Collider2D> (), col.gameObject.GetComponent<Collider2D> (), true);
				player.GetComponent<CharJump> ().jumpDown = false;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Platform") {
			Physics2D.IgnoreCollision (platformCollider.GetComponent<Collider2D> (), col.gameObject.GetComponent<Collider2D> (), false);
		}
	}
}
