using UnityEngine;
using System.Collections;

public class StatueBossLaser : MonoBehaviour {
	public LayerMask hitLayers;
	public GameObject aimTarget;
	public GameObject playerAim;
	public int damage = 1;

	AudioPlayer audioplay;
	private LineRenderer lineRenderer, childLineRenderer;
	Vector2 laserHit;
	RaycastHit2D hit;
	bool shooting;

	public float laserChargeTime = 1f, laserActiveTime = 2f;

	void Start() {
		audioplay = GetComponent<AudioPlayer>();
		lineRenderer = GetComponent<LineRenderer>();
		childLineRenderer = transform.GetChild(0).GetComponent<LineRenderer>();
		lineRenderer.useWorldSpace = true;
	}

	void Update() {
		//keep eye sprite upright.
		transform.rotation = Quaternion.identity;

		//Calculate laser trajectory with raycast.
		if(transform.parent.parent.GetComponent<StatueBoss>().raging)
			hit = Physics2D.Raycast(transform.position, -playerAim.transform.InverseTransformPoint(transform.position), Mathf.Infinity, hitLayers);
		else
			hit = Physics2D.Raycast(transform.position, -aimTarget.transform.InverseTransformPoint(transform.position), Mathf.Infinity, hitLayers);
		laserHit = hit.point;

		//Set linerenderer positions.
		lineRenderer.SetPosition(0, transform.position);
		lineRenderer.SetPosition(1, laserHit);

		childLineRenderer.SetPosition(0, transform.position);
		if(transform.parent.parent.GetComponent<StatueBoss>().raging)
			childLineRenderer.SetPosition(1, playerAim.transform.position);
		else
			childLineRenderer.SetPosition(1, aimTarget.transform.position);

		if(shooting)
			HitByLaser(hit);
	}

	public void Shoot() {
		StartCoroutine(ShootLaser());
	}

	IEnumerator ShootLaser() {
		if(!transform.parent.parent.GetComponent<StatueBoss>().raging)
			yield return new WaitForSeconds(Random.Range(0, 5));

		//disable aimObject animation.
		aimTarget.GetComponent<Animator>().enabled = false;
		playerAim.GetComponent<StatueBossPlayerAim>().aim = false;

		audioplay.PlayClip(0, 0.7f);

		yield return new WaitForSeconds(laserChargeTime);

		audioplay.PlayClip(1, 0.7f);

		//enable line and set shooting.
		childLineRenderer.enabled = false;
		lineRenderer.enabled = true;
		shooting = true;

		yield return new WaitForSeconds(Random.Range(laserActiveTime - 0.2f, laserActiveTime + 0.2f));

		//disable line and set not shooting.
		childLineRenderer.enabled = true;
		lineRenderer.enabled = false;
		shooting = false;

		audioplay.StopPlaying();

		yield return new WaitForSeconds(0.2f);

		//enable aimObject animation.
		aimTarget.GetComponent<Animator>().enabled = true;
		playerAim.GetComponent<StatueBossPlayerAim>().aim = true;
	}

	public void CancelLaser() {
		CancelInvoke();

		childLineRenderer.enabled = true;
		lineRenderer.enabled = false;
		shooting = false;
		aimTarget.GetComponent<Animator>().enabled = true;
		playerAim.GetComponent<StatueBossPlayerAim>().aim = true;
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