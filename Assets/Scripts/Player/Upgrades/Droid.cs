using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droid : MonoBehaviour {
	public Transform followTarget;
	public GameObject player;
	public LayerMask aimHitMask;

	float lastYpos;
	LineRenderer line;
	Animator anim;
	InputManager input;

	void Start () {
		line = GetComponent<LineRenderer>();
		anim = GetComponent<Animator>();
		input = GameObject.Find("InputManager").GetComponent<InputManager>();
	}

	//MAKE ANIM SLOW DOWN WHEN AMING!

	void Update() {
		if(input.GetKey("shoot") || Input.GetAxis("RightAnalogH") != 0 || Input.GetAxis("RightAnalogV") != 0) {
			Vector3 origin = transform.GetChild(0).transform.position + new Vector3(0, 0.3f, -5f), analogDir = new Vector3(Input.GetAxis("RightAnalogH"), Input.GetAxis("RightAnalogV"));
			Vector2 aimDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - origin;
			if(analogDir.magnitude != 0)
				aimDir = origin + analogDir;

			var raycastHit = Physics2D.Raycast(origin, aimDir, Mathf.Infinity, aimHitMask);

			line.SetPosition(0, origin);
			line.SetPosition(1, raycastHit.point);

			line.enabled = true;

			if(anim.speed > 0.01f)
				anim.speed -= 0.01f;
		}
		else {
			line.enabled = false;

			if(anim.speed < 0.99f)
				anim.speed += 0.01f;
		}
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
