using UnityEngine;
using System.Collections;

public class StatueBoss : MonoBehaviour {
	public GameObject leftEye, rightEye, playerAim;
	public float shootRepeatRate;
	public bool raging;

	void Start() {
		InvokeRepeating("ShootLasers", shootRepeatRate, shootRepeatRate);

	//Also start spawning enemies from mouth and play music and such
	}

	void Update() {
		if (leftEye == null && rightEye == null) {
			Defeated ();
		}
		if (!raging && (leftEye == null || rightEye == null)) {
				Rage ();
		}
	}
	
	/**
	 * Shoots lasers from the eyes of the statue
	 */
	void ShootLasers() {
		if (leftEye != null) {
			leftEye.GetComponent<StatueBossLaser> ().Shoot ();
		}
		if (rightEye != null) {
			rightEye.GetComponent<StatueBossLaser> ().Shoot ();
		} 
	}

	void Rage() {
		playerAim.GetComponent<StatueBossPlayerAim> ().raging = true;
		raging = true;

		CancelInvoke ();
		InvokeRepeating("ShootLasers", shootRepeatRate / 2, shootRepeatRate / 2);

		if (leftEye != null) {
			leftEye.GetComponent<SpriteRenderer> ().color = new Color (1, 0.5f, 0.5f);
			StatueBossLaser leftLaser = leftEye.GetComponent<StatueBossLaser> ();
			leftLaser.laserChargeTime = 0.7f;
			leftLaser.laserActiveTime = 1f;
			leftLaser.CancelLaser ();
		}
		else if (rightEye != null) {
			rightEye.GetComponent<SpriteRenderer> ().color = new Color (1, 0.5f, 0.5f);
			StatueBossLaser rightLaser = rightEye.GetComponent<StatueBossLaser> ();
			rightLaser.laserChargeTime = 0.7f;
			rightLaser.laserActiveTime = 1f;
			rightLaser.CancelLaser ();
		}
	}
		
	/**
	 * The boss is defeated
	 */
	void Defeated() {
		//Play correct animation and such.
		Destroy(this.gameObject.transform.parent.gameObject);
	}
}
