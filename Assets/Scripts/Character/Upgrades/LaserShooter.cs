using UnityEngine;
using System.Collections;

public class LaserShooter : MonoBehaviour {
	private LineRenderer lineRenderer;
	public Transform laserHit;
	CharEnergy charEnergy;
	bool holdShoot;

	void Start() {
		//Change player sprite and display tutorial
		lineRenderer = GetComponent<LineRenderer> ();
		//lineRenderer.useWorldSpace = true;
		charEnergy = GameObject.Find("char").GetComponent<CharEnergy> ();
	}

	void Update() {
		if (!Input.GetButton ("Shoot")) {
			holdShoot = false;
		}
		if (charEnergy.HasJuice (2) && Input.GetButton ("Shoot") && !holdShoot) {
			Debug.Log ("shoot");
			holdShoot = true;
			charEnergy.UseEnergy (2);
			ShootGun ();
		}
	}

	void ShootGun() {
		lineRenderer.enabled = true;
		RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.right);
		Debug.DrawLine (transform.position, hit.point);
		laserHit.position = hit.point;
		lineRenderer.SetPosition (0, transform.position);
		lineRenderer.SetPosition (1, laserHit.position);
		HitByLaser (hit);
		Invoke ("KillLaser", 0.5f);
	}

	void  KillLaser() {
		lineRenderer.enabled = false;
	}

	void HitByLaser(RaycastHit2D victim) {
		switch(victim.transform.gameObject.tag) {
			//Add more cases as more types of enemies are added to the game
		case "softEnemy":
			Debug.Log ("Enemy hit by laser!!");
			victim.transform.gameObject.GetComponent<SmallCritter> ().GetHurt (3);
			victim.transform.gameObject.GetComponent<Knockback>().Knock(this.gameObject, 3f);
			break;

		case "birdBossWeakSpot":
			Debug.Log ("Hit Bird in the Mouth!");
			victim.transform.parent.gameObject.GetComponent<BigBadBird> ().GetHurt ();
			break;
		}
	}
}