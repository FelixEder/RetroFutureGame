using UnityEngine;
using System.Collections;

public class CharTriggerPlatform : MonoBehaviour {

	void Start () {
	
	}

	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.tag == "Platform" && transform.parent.gameObject.GetComponent<Rigidbody2D> ().velocity.y < -5)
			transform.parent.gameObject.GetComponent<Rigidbody2D> ().velocity += new Vector2 (0, 1);
	}
}
