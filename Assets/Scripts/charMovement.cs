using UnityEngine;
using System.Collections;

public class charMovement : MonoBehaviour {
	//Fields
	float horizontalSpeed = 2.0f;
	float verticalSpeed = 5.0f;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		var horizontalMove = new Vector2(Input.GetAxis("Horizontal"), 0);
		var verticalMove = new Vector2(0, Input.GetAxis("Vertical"));

		transform.position += horizontalMove * verticalMove * speed * Time.deltaTime;
	
}
