using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour {
	private LineRenderer lineRenderer;
	public Transform laserHit;
	RaycastHit2D hit;
	public int damage = 1;
	bool shooting;
	
	void Start() {
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.useWorldSpace = true;
	}

	void Update() {
		if (shooting) {
			hit = Physics2D.Raycast (transform.position, -transform.up);
			laserHit.position = hit.point;
			lineRenderer.SetPosition (0, transform.position);
			lineRenderer.SetPosition (1, laserHit.position);
			HitByLaser (hit);
		}
	}

	public void Shoot() {
		//Should later on try to shoot the player instead of just downwards.
		shooting = true;
		lineRenderer.enabled = true;
		Debug.DrawLine (transform.position, hit.point);
		Invoke ("KillLaser", 1.2f);

	}

	void  KillLaser() {
		lineRenderer.enabled = false;
		shooting = false;
	}

	void HitByLaser(RaycastHit2D victim) {
		switch(victim.transform.gameObject.tag) {

		case "Char":
			Debug.Log ("Hit by laser!!");
			victim.transform.gameObject.GetComponent<CharHealth> ().TakeDamage (damage, gameObject, 10f);
			break;

		case "SmallCritter":
			victim.transform.gameObject.GetComponent<SmallCritter> ().TakeDamage (damage);
			break;
		}
	}
}