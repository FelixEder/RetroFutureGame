using UnityEngine;
using System.Collections;

	public class LaserBeam : MonoBehaviour {
		LineRenderer lineRenderer;
		Transform laserHit;

		void Start() {
			lineRenderer = GetComponent<lineRenderer> ();
			lineRenderer.enabled = true;
			lineRenderer.useWorldSpace = true;
			}

		void Shoot() {
			//Should later on try to shoot the player instead of just downwards.
			RayCastHit2D hit = Physics2D.RayCast (transform.position, transform.down);
			Debug.DrawLine (transform.position, hit.point);
			laserHit.position = hit.point;
			lineRenderer.SetPosition (0, transform.position);
			lineRenderer.SetPosition (1, laserHit.position);
		}
	}	

