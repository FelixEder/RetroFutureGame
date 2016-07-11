using UnityEngine;
using System.Collections;

	public class LaserBeam : MonoBehaviour {
		LineRenderer lineRenderer;
		Transform laserHit;

		void Start() {
			lineRenderer = GetComponent<LineRenderer> ();
			lineRenderer.enabled = true;
			lineRenderer.useWorldSpace = true;
			}

		public void Shoot() {
			//Should later on try to shoot the player instead of just downwards.
			RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down);
			Debug.DrawLine (transform.position, hit.point);
			laserHit.position = hit.point;
			lineRenderer.SetPosition (0, transform.position);
			lineRenderer.SetPosition (1, laserHit.position);
		}
	}	

