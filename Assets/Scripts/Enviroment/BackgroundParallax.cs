using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour {
	GameObject cameraObject;
	public float parallaxX, parallaxY;
	float offsetX, offsetY;

	void Start () {
		cameraObject = GameObject.Find ("Main Camera");
		offsetX = transform.position.x - transform.position.x / parallaxX;
		offsetY = transform.position.y - transform.position.y / parallaxY;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector2 (cameraObject.transform.position.x / parallaxX + offsetX, cameraObject.transform.position.y / parallaxY + offsetY);
	}
}