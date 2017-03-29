using UnityEngine;
using System.Collections;

public class StatueBoss : MonoBehaviour {
	public GameObject LeftEye, RightEye;

	void Start() {
	InvokeRepeating("ShootLasers", 5f, 6f);

	//Also start spawning enemies from mouth and play music and such
	}

	void Update() {
		if (LeftEye == null && RightEye == null) {
			Defeated ();
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

	/**
	 * The boss is defeated
	 */
	void Defeated() {
		//Play correct animation and such.
		Destroy(this.gameObject.transform.parent.gameObject);
	}
}
