using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJump : MonoBehaviour {
	public float jumpForce;
	[Range(1, 100)]
	public int jumpChance = 1;

	public Vector2 groundcheckPos = Vector2.one, wallcheckPos = Vector2.one;
	public LayerMask groundcheckMask, wallcheckMask;

	bool grounded, wallcheck;
	Rigidbody2D rb2D;

	void OnDrawGizmosSelected() {
		Gizmos.color = new Color(0, 0, 1, 0.5f); //blue
		Gizmos.DrawCube(transform.position - new Vector3(0, groundcheckPos.y, 0), new Vector3(groundcheckPos.x, 0.02f, 0));
		Gizmos.DrawCube(transform.position - new Vector3(wallcheckPos.x / 2 * FrontCheckDir(), 0, 0), new Vector3(wallcheckPos.x, wallcheckPos.y, 0));
	}

	void Start() {
		rb2D = GetComponent<Rigidbody2D>();
	}

	void Update() {
		grounded = Physics2D.OverlapBox(transform.position - new Vector3(0, groundcheckPos.y, 0), new Vector3(groundcheckPos.x, 0.02f, 0), 0, groundcheckMask);
		wallcheck = Physics2D.OverlapBox(transform.position - new Vector3(wallcheckPos.x / 2 * FrontCheckDir(), 0, 0), new Vector3(wallcheckPos.x, wallcheckPos.y, 0), 0, wallcheckMask);
	}

	void FixedUpdate() {
		if(grounded) {
			if(wallcheck) {
				if(Random.Range(0, 10) < 1)
					rb2D.AddForce(new Vector2(jumpForce / -5 * FrontCheckDir(), jumpForce), ForceMode2D.Impulse);
			}
			else {
				if(Random.Range(0, 1000) < jumpChance)
					rb2D.AddForce(new Vector2(jumpForce / -5 * FrontCheckDir(), jumpForce), ForceMode2D.Impulse);
			}
		}
	}

	int FrontCheckDir() {
		if(transform.rotation.y == 0)
			return 1;
		else
			return -1;
	}
}
