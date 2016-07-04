using System.Collections;
using UnityEngine;

/**
 * This class is responsible for changes to the physics of the current scene.
 */
public class Physics : MonoBehaviour {
	public float gravityCoef = 2;

	/*
	 * Initialized when the script is called, changes the gravity of the scene.
	 */
	void start() {
		Physics.gravity.y *= gravityCoef;
	}

}

