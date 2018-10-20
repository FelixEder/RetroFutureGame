using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMover : MonoBehaviour {
	public GameObject player;
	RelativeJoint2D target;
	GameObject current;
	Vector2 direction;
	float distance = 1;

	void Start() {
		target = GetComponent<RelativeJoint2D>();
		DisableAll();
	}

	void FixedUpdate() {
		if(target.connectedBody == null)
			target.connectedBody = player.GetComponent<Rigidbody2D>();
		if(target.connectedBody.gameObject != player) {
			direction = target.connectedBody.transform.position - player.transform.position;
			direction.Normalize();
			target.linearOffset = direction * distance;
		}
		else
			target.linearOffset = Vector2.right;
	}

	public void SetCurrent(GameObject tutorial, Rigidbody2D _target, float _distance) {
		if(current != null)
			DisableCurrent();
		Enable(tutorial, _target);
		distance = _distance;
	}

	public void ChangeTarget(Rigidbody2D _target, float _distance) {
		target.connectedBody = _target;
		distance = _distance;
	}

	public void DisableAll() {
		foreach(Transform child in transform)
			child.gameObject.SetActive(false);
		target.connectedBody = null;
	}

	public void DisableCurrent() {
		current.SetActive(false);
		target.connectedBody = null;
		distance = 1;
		current = null;
	}

	void Enable(GameObject tutorial, Rigidbody2D _target) {
		tutorial.SetActive(true);
		target.connectedBody = _target;
		current = tutorial;
	}
}
