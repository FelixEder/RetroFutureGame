using UnityEngine;
using System.Collections;

public class BigEyeGuyLaser : MonoBehaviour {
	private LineRenderer lineRenderer;
	public Transform laserHit;
	public int damage = 2;

	void Start() {
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.useWorldSpace = true;
		InvokeRepeating ("Shoot", 2f, 2f);
	}

	public void Shoot() {
		Debug.Log ("Is shooting laser!");
		lineRenderer.enabled = true;
		RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.right);
		Debug.DrawLine (transform.position, hit.point);
		laserHit.position = hit.point;
		lineRenderer.SetPosition (0, transform.position);
		lineRenderer.SetPosition (1, laserHit.position);
		HitByLaser (hit);
		Invoke ("KillLaser", 0.2f);
	}

	void  KillLaser() {
		lineRenderer.enabled = false;
	}

	void HitByLaser(RaycastHit2D victim) {
		switch(victim.transform.gameObject.tag) {
			case "Char":
			Debug.Log ("Hit by laser!!");
			victim.transform.gameObject.GetComponent<CharHealth> ().TakeDamage (damage, gameObject, 3f);
			break;
		}
	}
}