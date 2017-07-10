using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FollowRange {
	public float vertical, up, down;
}

[RequireComponent(typeof(Animator))]
public class EnemyMovement : MonoBehaviour {
	public FollowRange followRange;
	[Space(10)]
	public float moveSpeed = 3, wanderSpeed;
	public float wanderDist = 5, timeBeforeWander;
	[Space(10)]
	public Vector2 groundcheckPos = Vector2.one;
	public Vector2 wallcheckPos = Vector2.one;
	[Space(10)]
	public LayerMask groundcheckMask;
	public LayerMask wallcheckMask, raycastMask;

	bool grounded, wallcheck;
	int wanderDir = -1;
	float startPos;
	RaycastHit2D raycastHit;
	Rigidbody2D rb2D;
	GameObject player;

	void OnDrawGizmosSelected() {
		Gizmos.color = new Color(1, 1, 0, 0.5f); //yellow
		Gizmos.DrawCube(transform.position - new Vector3(0, groundcheckPos.y, 0), new Vector3(groundcheckPos.x, 0.1f, 0));
		Gizmos.DrawCube(transform.position - new Vector3(wallcheckPos.x * -wanderDir, 0, 0), new Vector3(0.1f, wallcheckPos.y, 0));
	}

	void Start() {
		rb2D = GetComponent<Rigidbody2D>();
		player = GameObject.Find("Char");
		startPos = transform.position.x;
		wanderDir = 0;
		if(timeBeforeWander > 0)
			Invoke("StartWander", timeBeforeWander);
	}

	void Update() {
		raycastHit = Physics2D.Raycast(transform.position, RaycastDirection(), followRange.vertical, raycastMask);
		grounded = Physics2D.OverlapBox(transform.position - new Vector3(0, groundcheckPos.y, 0), new Vector3(groundcheckPos.x, 0.1f, 0), 0, groundcheckMask);
		wallcheck = Physics2D.OverlapBox(transform.position - new Vector3(wallcheckPos.x * -wanderDir, 0, 0), new Vector3(0.1f, wallcheckPos.y, 0), 0, wallcheckMask);

		if(wallcheck) {
			wanderDir *= -1;
		}

		SpriteFacing();
	}

	void FixedUpdate() {
		if(grounded) {
			if(raycastHit) {
				if(raycastHit.transform.name == "Char") {
					rb2D.velocity += Mathf.Abs(rb2D.velocity.x) < moveSpeed ? new Vector2(moveSpeed * 0.1f * Mathf.Sign(player.transform.position.x - transform.position.x), 0) : Vector2.zero;
					wanderDir *= (int) Mathf.Sign(player.transform.position.x - transform.position.x);
				}
				else if(!(wanderDist == 0 && Mathf.Abs(transform.position.x - startPos) < 0.5f))
						rb2D.velocity += Mathf.Abs(rb2D.velocity.x) < wanderSpeed ? new Vector2(moveSpeed * 0.1f * wanderDir, 0) : Vector2.zero;
			}
			else if(!(wanderDist == 0 && Mathf.Abs(transform.position.x - startPos) < 0.5f))
				rb2D.velocity += Mathf.Abs(rb2D.velocity.x) < wanderSpeed ? new Vector2(moveSpeed * 0.1f * wanderDir, 0) : Vector2.zero;

			if(transform.position.x - startPos > wanderDist && wanderDir == 1 || startPos - transform.position.x > wanderDist && wanderDir == -1)
				wanderDir *= -1;
		}
	}

	Vector2 RaycastDirection() {
		Vector2 raycastDirection = player.transform.position - transform.position;
		if(raycastDirection.y < -followRange.down || raycastDirection.y > followRange.up || Mathf.Abs(raycastDirection.x) > followRange.vertical)
			raycastDirection = new Vector2(followRange.vertical * wanderDir, 0);
		Debug.DrawRay(transform.position, raycastDirection);
		return raycastDirection;
	}

	void SpriteFacing() {
		if(rb2D.velocity.x > 1f) {
			transform.rotation = Quaternion.Euler(0, 180, 0);
			GetComponent<Animator>().enabled = true;
		}
		else if(rb2D.velocity.x < -1f) {
			transform.rotation = Quaternion.Euler(0, 0, 0);
			GetComponent<Animator>().enabled = true;
		}
		else {
			GetComponent<Animator>().enabled = false;
		}

	}

	void OnBecameVisible() {
		if(timeBeforeWander == 0)
			StartWander();
	}

	void StartWander() {
		if(Random.Range(0, 2) == 0)
			wanderDir = -1;
		else
			wanderDir = 1;
	}
}
