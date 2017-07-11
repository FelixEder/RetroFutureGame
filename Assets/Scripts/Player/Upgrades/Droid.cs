using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droid : MonoBehaviour {
	public Transform followTarget;
	public GameObject player;

	float lastYpos;
	LineRenderer line;
	InputManager input;

	void Start () {
		line = GetComponent<LineRenderer>();
		input = GameObject.Find("InputManager").GetComponent<InputManager>();
	}

	void Update() {
		if(input.GetKey("shoot") || Input.GetAxis("RightAnalogH") != 0 || Input.GetAxis("RightAnalogV") != 0) {
			//line position 0
			line.SetPosition(0, transform.position + new Vector3(0, 0.3f, -5));

			//line position 1
			Vector3 aimDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			aimDir = new Vector3(aimDir.x, aimDir.y, -5);
			Vector3 analogDir = new Vector3(Input.GetAxis("RightAnalogH") * 100, Input.GetAxis("RightAnalogV") * 100, -5);
			if(analogDir.magnitude != 0)
				aimDir = transform.position + analogDir;
			line.SetPosition(1, aimDir);

			//enable line
			line.enabled = true;
		}
		else
			//disable line
			line.enabled = false;
	}

	void FixedUpdate () {
		Vector3 position = transform.position, target = followTarget.position;
		if(player.GetComponent<PlayerStatus>().inWater)
			target = new Vector3(target.x, lastYpos, target.y);
		else
			lastYpos = target.y;

		transform.position = new Vector3(Mathf.Lerp(position.x, target.x, FollowSpeed()), Mathf.Lerp(position.y, target.y, FollowSpeed() / 2), 0);
	}

	float FollowSpeed() {
		return 0.05f * Vector2.Distance(transform.position, followTarget.transform.position);
	}
}
