using UnityEngine;
using System.Collections;

public class charMovement : MonoBehaviour {
	//Fields
	float speed = 2.0f;
	float jumpSpeed = 10.0f;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		rigidbody2d.veloctiy = new Vector2 (Input.GetAxis ("Horizontal") * speed, Input.GetAxis ("Vertical") * speed);
		if (Input.GetButton (JumpButton)) Jump ();
	}

	void Jump() {
		rigidbody2D.AddForce (Vector2.up * jumpSpeed);
	}
}