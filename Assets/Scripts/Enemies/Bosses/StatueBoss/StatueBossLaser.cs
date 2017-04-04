using UnityEngine;
using System.Collections;

public class StatueBossLaser : MonoBehaviour {
	public LayerMask hitLayers;
	public GameObject aimTarget;
	private LineRenderer lineRenderer, childLineRenderer;
	Vector2 laserHit;
	RaycastHit2D hit;
	public int damage = 1, health;
	bool shooting;
	
	void Start() {
		lineRenderer = GetComponent<LineRenderer> ();
		childLineRenderer = transform.GetChild (0).GetComponent<LineRenderer> ();
		lineRenderer.useWorldSpace = true;
	}

	void Update() {
		//keep eye sprite upright.
		transform.rotation = Quaternion.identity;

		hit = Physics2D.Raycast (transform.position, -aimTarget.transform.InverseTransformPoint(transform.position), Mathf.Infinity, hitLayers);
		laserHit = hit.point;

		if (shooting) {
			lineRenderer.SetPosition (0, transform.position);
			lineRenderer.SetPosition (1, laserHit);
			HitByLaser (hit);
		}

		childLineRenderer.SetPosition (0, transform.position);
		childLineRenderer.SetPosition (1, laserHit);
	}

	public void Shoot() {
		//disable aimObject animation.
		aimTarget.GetComponent<Animator>().enabled = false;

		//avoid graphical issue by setting positions before enabling line.
		hit = Physics2D.Raycast (transform.position, -aimTarget.transform.InverseTransformPoint(transform.position), Mathf.Infinity, hitLayers);
		laserHit = hit.point;
		lineRenderer.SetPosition (0, transform.position);
		lineRenderer.SetPosition (1, laserHit);

		childLineRenderer.enabled = false;
		lineRenderer.enabled = true;
		shooting = true;
		Invoke ("KillLaser", 2f);

	}

	void  KillLaser() {
		aimTarget.GetComponent<Animator>().enabled = true;
		childLineRenderer.enabled = true;
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