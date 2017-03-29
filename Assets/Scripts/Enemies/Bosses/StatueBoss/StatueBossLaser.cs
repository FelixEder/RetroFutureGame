using UnityEngine;
using System.Collections;

public class StatueBossLaser : MonoBehaviour {
	public LayerMask hitLayers;
	private LineRenderer lineRenderer;
	Vector2 laserHit;
	RaycastHit2D hit;
	public int damage = 1, health;
	bool shooting;
	
	void Start() {
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.useWorldSpace = true;
	}

	void Update() {
		if (shooting) {
			hit = Physics2D.Raycast (transform.position, -transform.up, Mathf.Infinity, hitLayers);
			laserHit = hit.point;
			lineRenderer.SetPosition (0, transform.position);
			lineRenderer.SetPosition (1, laserHit);
			HitByLaser (hit);
		}
	}

	public void Shoot() {
		//Should later on try to shoot the player instead of just downwards.
		hit = Physics2D.Raycast (transform.position, -transform.up, Mathf.Infinity, hitLayers);
		laserHit = hit.point;
		lineRenderer.SetPosition (0, transform.position);
		lineRenderer.SetPosition (1, laserHit);

		Debug.DrawLine (transform.position, hit.point);
		lineRenderer.enabled = true;
		shooting = true;
		Invoke ("KillLaser", 2f);

	}

	void  KillLaser() {
		lineRenderer.enabled = false;
		shooting = false;
	}

	void HitByLaser(RaycastHit2D victim) {
		switch(victim.collider.gameObject.tag) {

		case "Char":
			Debug.Log ("Hit by laser!!");
			victim.transform.gameObject.GetComponent<CharHealth> ().TakeDamage (damage, gameObject, 10f);
			break;

		case "SmallCritter":
			victim.transform.gameObject.GetComponent<SmallCritter> ().Knockback (gameObject, 5);
			victim.transform.gameObject.GetComponent<SmallCritter> ().TakeDamage (damage);
			break;

		default:
			break;	
		}
	}


	/**
	 * The barrier of the door has been broken, it is now destroyed.
	 */
	void Broken() {
		//Play animation and 
		Destroy(gameObject);
		Destroy (transform.parent.gameObject);
	}

	public void TakeDamage(int damage) {
		health -= damage;
		if (health <= 0) {
			Broken();
		}
	}
}