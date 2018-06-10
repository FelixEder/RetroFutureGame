using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droid : MonoBehaviour {
	public Transform followTarget;
	public GameObject player;
	public LayerMask aimHitMask;

	float lastYpos;
	Transform sprite;
	LineRenderer line;
	Animator anim;
	InputManager input;

	void Start () {
		sprite = transform.GetChild(0);
		line = GetComponent<LineRenderer>();
		anim = GetComponent<Animator>();
		input = GameObject.Find("InputManager").GetComponent<InputManager>();
	}

	void Update() {

		if((input.GetKey("shoot") || input.GetAxis("RightX") != 0 || input.GetAxis("RightY") != 0) && sprite.GetComponent<DroidLaser>().canShoot) {

			Vector3 origin = sprite.position + new Vector3(0, 0.3f, -5f), analogDir = new Vector3(input.GetAxis("RightX"), input.GetAxis("RightY"));
			Vector2 aimDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - origin;
			if(analogDir.magnitude != 0)
				aimDir = analogDir;

			var raycastHit = Physics2D.Raycast(origin, aimDir, Mathf.Infinity, aimHitMask);

			line.SetPosition(0, origin);
			if(raycastHit)
				line.SetPosition(1, raycastHit.point);
			else
                line.SetPosition(1, (Vector2)origin + (aimDir * 100));

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
