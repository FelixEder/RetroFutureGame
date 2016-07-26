using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = GameObject.Find ("Char").transform.position;
		transform.position += new Vector3 (0, 1, -5);
	}
}
