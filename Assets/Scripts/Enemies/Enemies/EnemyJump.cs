using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJump : MonoBehaviour {
	public float jumpForce = 7;
	[Range(1, 100)]
	public int jumpChance = 10;

	public Vector2 groundcheckPos = Vector2.one;
	public Vector3 obstaclecheckPos = Vector2.one;
	public LayerMask groundMask = 262656, obstacleMask = 262144;

	bool grounded, obstaclecheck;
	Rigidbody2D rb2D;

	void OnDrawGizmosSelected() {
		Gizmos.color = new Color(0, 0, 1, 0.5f); //blue
		Gizmos.DrawCube(transform.position - new Vector3(0, groundcheckPos.y / 2, 0), new Vector3(groundcheckPos.x, groundcheckPos.y, 0));
		Gizmos.DrawCube(transform.position - new Vector3((obstaclecheckPos.x + obstaclecheckPos.z)/ 2 * FrontCheckDir(), 0, 0), new Vector3(obstaclecheckPos.x - obstaclecheckPos.z, obstaclecheckPos.y, 0));
	}

	void Start() {
		rb2D = GetComponent<Rigidbody2D>();
	}

	void Update() {
		grounded = Physics2D.OverlapBox(transform.position - new Vector3(0, groundcheckPos.y / 2, 0), new Vector3(groundcheckPos.x, groundcheckPos.y, 0), 0, groundMask);
		obstaclecheck = Physics2D.OverlapBox(transform.position - new Vector3(obstaclecheckPos.x / 2 * FrontCheckDir(), 0, 0), new Vector3(obstaclecheckPos.x, obstaclecheckPos.y, 0), 0, obstacleMask);
	}

	void FixedUpdate() {
		if(grounded && rb2D.velocity.y < jumpForce / 10) {
			if(obstaclecheck) {
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
