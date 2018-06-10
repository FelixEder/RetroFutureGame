using UnityEngine;
using System.Collections;

public class StatueBossLaser : MonoBehaviour {
	public LayerMask hitLayers, aimHitLayers;
	public GameObject aimTarget, playerAim, statueVein;
	public int damage = 1;

	Vector3 origin, aimDir;
	float speed = 0.5f;
	bool shoot = true;

	RaycastHit2D aimHit;
	AudioPlayer audioplay;
	LineRenderer line, childLine;

	public float laserChargeTime = 1f, laserActiveTime = 2f;

	void Start() {
		audioplay = GetComponent<AudioPlayer>();
		line = GetComponent<LineRenderer>();
		childLine = transform.GetChild(0).GetComponent<LineRenderer>();
		line.useWorldSpace = true;
	}

	void Update() {
		//keep eye sprite upright.
		transform.rotation = Quaternion.identity;

		if(transform.parent.parent.GetComponent<StatueBoss>().raging)
			aimHit = Physics2D.Raycast(transform.position, -playerAim.transform.InverseTransformPoint(transform.position), Mathf.Infinity, aimHitLayers);
		else
			aimHit = Physics2D.Raycast(transform.position, -aimTarget.transform.InverseTransformPoint(transform.position), Mathf.Infinity, aimHitLayers);
			
		childLine.SetPosition(0, transform.position); 
		childLine.SetPosition(1, aimHit.point);
	}

	public void Shoot() {
		StartCoroutine(ActivateLaser());
	}

	public void CancelShoot() {
		StopAllCoroutines();

		shoot = true;
		childLine.enabled = true;
		line.enabled = false;
		audioplay.StopPlaying();
		statueVein.GetComponent<Animator>().enabled = true;
		aimTarget.GetComponent<Animator>().enabled = true;
		playerAim.GetComponent<StatueBossPlayerAim>().aim = true;
	}

	IEnumerator ActivateLaser() {
		if(!transform.parent.parent.GetComponent<StatueBoss>().raging)
			yield return new WaitForSeconds(Random.Range(0, 5));

		//disable aimObject animation.
		statueVein.GetComponent<Animator>().enabled = false;
		aimTarget.GetComponent<Animator>().enabled = false;
		playerAim.GetComponent<StatueBossPlayerAim>().aim = false;

		audioplay.PlayClip(0, 0.7f);

		yield return new WaitForSeconds(laserChargeTime);

		audioplay.PlayClip(1, 0.7f);
		
		origin = transform.position + new Vector3(0, 0, -5f);

		if(transform.parent.parent.GetComponent<StatueBoss>().raging)
			aimDir = -playerAim.transform.InverseTransformPoint(transform.position);
		else
			aimDir = -aimTarget.transform.InverseTransformPoint(transform.position);

		aimDir = ((Vector2)aimDir * 10).normalized;

		line.SetPosition(0, origin);
		line.SetPosition(1, origin);
		line.enabled = true;

		StartCoroutine(DeactivateLaser());

		RaycastHit2D raycastHit = new RaycastHit2D();

		for(int i = 0; shoot; i++) {
			raycastHit = Physics2D.Raycast(origin, aimDir, i * 0.5f, hitLayers);
			if(raycastHit) {
				line.SetPosition(1, raycastHit.point);
				HitByLaser(raycastHit);
			}
			else
				line.SetPosition(1, origin + (aimDir * i * 0.5f));

			yield return new WaitForSeconds(0.0001f);
		}
	}

	IEnumerator DeactivateLaser() {
		yield return new WaitForSeconds(Random.Range(laserActiveTime - 0.2f, laserActiveTime + 0.2f));

		shoot = false;

		StartCoroutine(EnableLaser());
		for(float i = 0; Mathf.Sign(line.GetPosition(1).x - line.GetPosition(0).x) == Mathf.Sign(aimDir.x) && Mathf.Sign(line.GetPosition(1).y - line.GetPosition(0).y) == Mathf.Sign(aimDir.y) && i < 10000; i++) {
			origin += (aimDir * speed);
			line.SetPosition(0, origin);
			Debug.Log("Origin = " + origin);

			yield return new WaitForSeconds(0.0001f);
		}

		line.enabled = false;
	}

	IEnumerator EnableLaser() {
		audioplay.StopPlaying();
		yield return new WaitForSeconds(0.2f);
		
		shoot = true;
		//enable aimObject animation.
		statueVein.GetComponent<Animator>().enabled = true;
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