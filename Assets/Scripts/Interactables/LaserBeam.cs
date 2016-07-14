using UnityEngine;
using System.Collections;

	public class LaserBeam : MonoBehaviour {
		LineRenderer lineRenderer;
		public Transform laserHit;

		void Start() {
			lineRenderer = GetComponent<LineRenderer> ();
			lineRenderer.useWorldSpace = true;
		}

		public void Shoot() {
			//Should later on try to shoot the player instead of just downwards.
			lineRenderer.enabled = true;
			RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.up);
			Debug.DrawLine (transform.position, hit.point);
			laserHit.position = hit.point;
			lineRenderer.SetPosition (0, transform.position);
			lineRenderer.SetPosition (1, laserHit.position);
			Invoke ("killLaser", 2f);
		}

		void  killLaser() {
			lineRenderer.enabled = false;
	}
}

