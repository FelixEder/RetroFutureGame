using UnityEngine;
using System.Collections;

public class StatueBoss : MonoBehaviour {
	public GameObject LeftEye, RightEye, playerAim;
	public float shootRepeatRate;
	public bool raging;

	void Start() {
		InvokeRepeating("ShootLasers", shootRepeatRate, shootRepeatRate);

	//Also start spawning enemies from mouth and play music and such
	}

	void Update() {
		if (LeftEye == null && RightEye == null) {
			Defeated ();
		}
		if (!raging && (LeftEye == null || RightEye == null)) {
				Rage ();
		}
	}
	
	/**
	 * Shoots lasers from the eyes of the statue
	 */
	void ShootLasers() {
		if (LeftEye != null) {
			LeftEye.GetComponent<StatueBossLaser> ().Shoot ();
		}
		if (RightEye != null) {
			RightEye.GetComponent<StatueBossLaser> ().Shoot ();
		} 
	}

	void Rage() {
		playerAim.GetComponent<StatueBossPlayerAim> ().raging = true;
		raging = true;

		CancelInvoke ();
		InvokeRepeating("ShootLasers", shootRepeatRate / 2, shootRepeatRate / 2);

		if (LeftEye != null) {
			LeftEye.GetComponent<SpriteRenderer> ().color = new Color (1, 0.5f, 0.5f);
			LeftEye.GetComponent<StatueBossLaser> ().laserChargeTime = 0.7f;
			LeftEye.GetComponent<StatueBossLaser> ().laserActiveTime = 1f;
		}
		else if (RightEye != null) {
			RightEye.GetComponent<SpriteRenderer> ().color = new Color (1, 0.5f, 0.5f);
			RightEye.GetComponent<StatueBossLaser> ().laserChargeTime = 0.7f;
			RightEye.GetComponent<StatueBossLaser> ().laserActiveTime = 1f;
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
