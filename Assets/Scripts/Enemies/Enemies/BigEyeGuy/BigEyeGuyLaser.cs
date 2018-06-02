using UnityEngine;
using System.Collections;

public class BigEyeGuyLaser : MonoBehaviour {
public LayerMask hitLayers;
	public int damage = 1;

//	AudioPlayer audioplay;
	private LineRenderer lineRenderer;
	Vector2 laserHit;
	RaycastHit2D hit;
	bool shooting;

	void Start() {
//		audioplay = GetComponent<AudioPlayer>();
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.useWorldSpace = true;
		
		InvokeRepeating("Shoot", 5f, 5f);
	}

	void Update() {
		//keep eye sprite upright.
		transform.rotation = Quaternion.identity;

		//Calculate laser trajectory with raycast.
		hit = Physics2D.Raycast(transform.position, Vector2.left, Mathf.Infinity, hitLayers);
		laserHit = hit.point;

		//Set linerenderer positions.
		lineRenderer.SetPosition(0, transform.position);
		lineRenderer.SetPosition(1, laserHit);

		if(shooting)
			HitByLaser(hit);
	}

	public void Shoot() {
		StartCoroutine(ShootLaser());
	}

	IEnumerator ShootLaser() {
		//disable aimObject animation.

//		audioplay.PlayClip(1, 0.7f);

		//enable line and set shooting.
		lineRenderer.enabled = true;
		shooting = true;

		yield return new WaitForSeconds(0.1f);

		//disable line and set not shooting.
		lineRenderer.enabled = false;
		shooting = false;

//		audioplay.StopPlaying();
	}

	public void CancelLaser() {
		CancelInvoke();

		lineRenderer.enabled = false;
		shooting = false;
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


	/**
	 * The barrier of the door has been broken, it is now destroyed.
	 */
	void Broken() {
		//Play animation and 
		Destroy(gameObject);
		Destroy(transform.parent.gameObject);
	}
}