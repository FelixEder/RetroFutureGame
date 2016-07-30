using UnityEngine;
using System.Collections;

public class LaserShooter : MonoBehaviour {
	CharStatus charStatus;
	private LineRenderer lineRenderer;
	public Transform laserHit;
	CharEnergy charEnergy;
	bool holdShoot, canShoot = true;
	public int damage = 2;

	void Start() {
		//Change player sprite and display tutorial
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.useWorldSpace = true;
		charStatus = transform.parent.GetComponent<CharStatus> ();
		charEnergy = GameObject.Find("Char").GetComponent<CharEnergy> ();
	}

	void Update() {
		if (!Input.GetButton ("Shoot")) {
			holdShoot = false;
		}
		if (Input.GetButton ("Shoot") && !holdShoot && canShoot && !charStatus.isSmall) {
			holdShoot = true;
			if (charEnergy.UseEnergy (2)) {
				canShoot = false;
				//charEnergy.UseEnergy (2);
				ActivateLaser ();
			}
		}
	}

	void ActivateLaser() {
		lineRenderer.enabled = true;
		RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.right);
		Debug.DrawLine (transform.position, hit.point);
		laserHit.position = new Vector3(hit.point.x, hit.point.y, -5);
		lineRenderer.SetPosition (0, transform.position);
		lineRenderer.SetPosition (1, laserHit.position);
		StartCoroutine (ShrinkLaser ());
		HitByLaser (hit);
	}

	void HitByLaser(RaycastHit2D victim) {
		Debug.Log ("Player shot: " + victim.transform.gameObject.tag);
		switch(victim.collider.transform.gameObject.tag) {
			//Add more cases as more types of enemies are added to the game
		case "SmallCritter":
			victim.transform.gameObject.GetComponent<SmallCritter> ().TakeDamage (damage);
			break;

		case "JumpingCritter":
			victim.transform.gameObject.GetComponent<JumpingCritter> ().TakeDamage (damage);
			break;

		case "HardEnemy":
			victim.transform.gameObject.GetComponent<HardCritter> ().Rush ();
			break;

		case "BigEyeGuy" :
			//Can't be hurt by laser, play relevant things
			break;

		case "CrawlerCritter":
			CrawlerCritter crawlercritter = victim.transform.gameObject.GetComponent<CrawlerCritter> ();
			if (crawlercritter.deShelled) {
				crawlercritter.GetHurt (damage);
			} else {
				//Can't be hurt by laser, play relevant things
			}
			break;

		case "ShellMan":
			ShellMan shellMan = victim.transform.gameObject.GetComponent<ShellMan> ();
			if (shellMan.deShelled) {
				shellMan.GetHurt (damage);
			} else {
				//Can't be hurt by laser, play relevant things
			}
			break;

		case "BirdBossWeakSpot":
			Debug.Log ("Hit Bird in the Mouth!");
			victim.transform.gameObject.GetComponent<BigBadBird> ().GetHurt ();
			break;

		case "BigEyeGuyWeakSpot":
			Debug.Log ("Hit EyeGuy in the Eye!");
			victim.transform.gameObject.GetComponent<BigEyeGuy> ().GetHurt (damage);
			break;
		}
	}

	IEnumerator ShrinkLaser() {
		Vector3 target = transform.position;
		Invoke ("CanShoot", 1);
		yield return new WaitForSeconds (0.1f);
		if (transform.parent.gameObject.GetComponent<CharStatus> ().isMirrored) {
			while (lineRenderer.enabled && Mathf.Abs (target.x) > Mathf.Abs (laserHit.position.x)) {
				lineRenderer.SetPosition (0, target);
				target -= transform.right * -1;
				yield return new WaitForSeconds (0.01f);
			}
		}
		else {
			while (lineRenderer.enabled && Mathf.Abs (target.x) < Mathf.Abs (laserHit.position.x)) {
				lineRenderer.SetPosition (0, target);
				target -= transform.right * -1;
				yield return new WaitForSeconds (0.01f);
			}
		}
		lineRenderer.enabled = false;
	}

	void CanShoot() {
		canShoot = true;
	}
}