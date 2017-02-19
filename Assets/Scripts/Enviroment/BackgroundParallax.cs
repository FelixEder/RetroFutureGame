using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour {
	GameObject camera;
	public float parallaxX, parallaxY;
	float offsetX, offsetY;

	void Start () {
		camera = GameObject.Find ("Main Camera");
		offsetX = transform.position.x - transform.position.x / parallaxX;
		offsetY = transform.position.y - transform.position.y / parallaxY;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector2 (camera.transform.position.x / parallaxX + offsetX, camera.transform.position.y / parallaxY + offsetY);
	}
}