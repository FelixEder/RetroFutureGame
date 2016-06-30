using UnityEngine;
using System.Collections;

public class charMovement : MonoBehaviour {
	//Fields
	public float moveSpeed;
	public float jumpSpeed;

	// Use this for initialization
	void Start() {
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			Jump ();
		}
		GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
	}

	void Jump() {
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
	}
}