using UnityEngine;
using System.Collections;

public class BigEyeGuy : MonoBehaviour {
	public LayerMask hitLayers;
	public int damage = 1;
	bool canShoot = true;

	Vector3 origin, aimDir;
	float speed = 0.5f, origWanderSpeed;

	public LineRenderer line;
	EnemyMovement enemyMove;
	AudioPlayer audioplay;


	void Start() {
		audioplay = GetComponent<AudioPlayer>();
		enemyMove = transform.GetComponent<EnemyMovement>();
		line.useWorldSpace = true;
	}

	void Update() {
		aimDir = transform.rotation.y == 0 ? Vector2.left : Vector2.right;

		if(enemyMove.raycastHit && canShoot) {
			if(enemyMove.raycastHit.collider.name == "Player") {
				canShoot = false;
				origWanderSpeed = enemyMove.wanderSpeed;
				enemyMove.wanderSpeed = 0;
				StartCoroutine(ActivateLaser());
			}
		}
	}

	IEnumerator ActivateLaser() {
		yield return new WaitForSeconds(1f);

		audioplay.PlayClip(0, 0.7f);

		yield return new WaitForSeconds(1.5f);

		origin = line.transform.position + new Vector3(0, 0, -5f);
		var start = origin;

		line.SetPosition(0, origin);
		line.SetPosition(1, origin);
		line.enabled = true;

		StartCoroutine(DeactivateLaser());

		RaycastHit2D raycastHit = new RaycastHit2D();

		for(int i = 0; !raycastHit && i < 50; i++) {
			raycastHit = Physics2D.Raycast(origin, aimDir, 3f, hitLayers);

			if(raycastHit)
				line.SetPosition(1, raycastHit.point);
			else if(i < 7)
				line.SetPosition(1, origin + (aimDir * i * 0.5f));
			else
				line.SetPosition(1, origin + (aimDir * 3f));

			yield return new WaitForSeconds(0.0001f);
		}
		if(raycastHit) {
			HitByLaser(raycastHit);
		}
	}

	IEnumerator DeactivateLaser() {
		yield return new WaitForSeconds(0.1f);

		StartCoroutine(EnableLaser());
		for(float i = 0; Mathf.Sign(line.GetPosition(1).x - line.GetPosition(0).x) == aimDir.x && i < 50; i++) {
			origin += (aimDir * speed);
			line.SetPosition(0, origin);

			yield return new WaitForSeconds(0.0001f);
		}

		line.enabled = false;
	}

	IEnumerator EnableLaser() {
		yield return new WaitForSeconds(1f);

		canShoot = true;
		enemyMove.wanderSpeed = origWanderSpeed;
	}

	void HitByLaser(RaycastHit2D victim) {
		switch(victim.collider.gameObject.tag) {

			case "Player":
				Debug.Log("Hit by laser!!");
				victim.collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage, gameObject, 10f);
				break;

			case "SmallCritter":
				victim.collider.GetComponent<EnemyHealth>().TakeDamage(99);
				break;

			case "PickupableItem":
				victim.collider.gameObject.GetComponent<PickUpableItem>().Break();
				break;

			default:
				break;
		}
	}
}