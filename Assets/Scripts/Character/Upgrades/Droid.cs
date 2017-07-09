using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droid : MonoBehaviour {
	public Transform followTarget;
	public GameObject player;

	float lastYpos;

	void Start () {

	}

	void Update() {
		
	}

	void FixedUpdate () {
		Vector3 position = transform.position, target = followTarget.position;
		if(player.GetComponent<CharStatus>().inWater)
			target = new Vector3(target.x, lastYpos, target.y);
		else
			lastYpos = target.y;

		transform.position = new Vector3(Mathf.Lerp(position.x, target.x, FollowSpeed()), Mathf.Lerp(position.y, target.y, FollowSpeed() / 2), 0);
	}

	float FollowSpeed() {
		return 0.05f * Vector2.Distance(transform.position, followTarget.transform.position);
	}

	//if char in water, set y position to its own y.
}
