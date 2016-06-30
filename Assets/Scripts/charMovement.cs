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
		var horizontalMove = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
		var verticalMove = new Vector3(0, Input.GetAxis("Vertical"), 0);

		transform.position += horizontalMove * horizontalSpeed * Time.deltaTime;
		transform.position += verticalMove * verticalSpeed * Time.deltaTime;
	
	}
}