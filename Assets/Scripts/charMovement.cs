using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	//Fields
	float speed = 1.0f;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		int move = Vector3(Input.getAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		transform.position += move * speed * Time.deltaTime;
	}
}
