using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMover : MonoBehaviour {
    public GameObject player;
    RelativeJoint2D target;
	GameObject current;
    Vector2 direction;

    // Use this for initialization
    void Start() {
        target = GetComponent<RelativeJoint2D>();
		DisableAll();
    }

    // Update is called once per frame
    void FixedUpdate() {
		if(target.connectedBody == null)
			target.connectedBody = player.GetComponent<Rigidbody2D>();
        if(target.connectedBody.gameObject != player) {
            direction = target.connectedBody.transform.position - player.transform.position;
            direction.Normalize();
            target.linearOffset = direction;
        }
		else
			target.linearOffset = Vector2.right;
    }
	
	public void SetCurrent(GameObject tutorial, Rigidbody2D _target) {
		if(current != null)
			DisableCurrent();
		Enable(tutorial, _target);
	}
	
	public void ChangeTarget(Rigidbody2D _target) {
		target.connectedBody = _target;	
	}
	
	public void DisableAll() {
		foreach(Transform child in transform)
			child.gameObject.SetActive(false);
		target.connectedBody = null;
	}
	
	public void DisableCurrent() {
		current.SetActive(false);
		target.connectedBody = null;
		current = null;
	}
	
	void Enable(GameObject tutorial, Rigidbody2D _target) {
		tutorial.SetActive(true);
		target.connectedBody = _target;
		current = tutorial;
	}
}
