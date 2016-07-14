using UnityEngine;
using System.Collections;

	public class LaserBeam : MonoBehaviour {
		private LineRenderer lineRenderer;
		public Transform laserHit;
		GameObject player;
		
		void Start() {
			lineRenderer = GetComponent<LineRenderer> ();
			lineRenderer.useWorldSpace = true;
			player = GameObject.Find("char");
		}

		public void Shoot() {
			//Should later on try to shoot the player instead of just downwards.
			lineRenderer.enabled = true;
		RaycastHit2D hit = Physics2D.Raycast (transform.position, -transform.up);
			Debug.DrawLine (transform.position, hit.point);
			laserHit.position = hit.point;
			lineRenderer.SetPosition (0, transform.position);
			lineRenderer.SetPosition (1, laserHit.position);
			HitByLaser (hit);
			Invoke ("KillLaser", 2f);

		}

		void  KillLaser() {
			lineRenderer.enabled = false;
	}

	void HitByLaser(RaycastHit2D victim) {
		switch(victim.transform.gameObject.tag) {

		case "char":
			Debug.Log ("Hit by laser!!");
			player.GetComponent<Knockback>().Knock(this.gameObject, 3f);
			//Take away some of the player health
			break;
		}
	}
}

