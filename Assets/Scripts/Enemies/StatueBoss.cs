using UnityEngine;
using System.Collections;

	public class StatueBoss : MonoBehaviour {
	GameObject LeftEye, RightEye;

		void Start() {
		LeftEye = this.gameObject.transform.GetChild (1).gameObject;
		RightEye = this.gameObject.transform.GetChild (0).gameObject;
		InvokeRepeating("ShootLasers", 5f, 3f);

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
				LeftEye.transform.GetChild(1).gameObject.GetComponent<LaserBeam> ().Shoot ();
			}
			if (RightEye != null) {
				RightEye.transform.GetChild(1).gameObject.GetComponent<LaserBeam> ().Shoot ();
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
