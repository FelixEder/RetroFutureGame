using UnityEngine;
using System.Collections;

	public class StatueBoss : MonoBehaviour {
	GameObject LeftEye, RightEye, LeftBlock, RightBlock;

		void Start() {
		LeftEye = this.gameObject.transform.GetChild (0).gameObject;
		RightEye = this.gameObject.transform.GetChild (1).gameObject;
		LeftBlock = this.gameObject.transform.GetChild (2).gameObject;
		RightBlock = this.gameObject.transform.GetChild (3).gameObject;

		//Also start spawning enemies from mouth and play music and such
		}

		void Update() {
			if (LeftEye == null && RightEye == null) {
				Defeated ();
			}
			Invoke ("ShootLasers", 3f);
		}
		
		/**
		 * Shoots lasers from the eyes of the statue
		 */
		void ShootLasers() {
			if (LeftEye != null) {
				LeftEye.GetComponent<LaserBeam> ().Shoot ();
			}
			if (RightEye != null) {
				RightEye.GetComponent<LaserBeam> ().Shoot ();
			}
		}

		/**
		 * The boss is defeated
		 */
		void Defeated() {
			//Play correct animation and such.
			Destroy (this.gameObject);
		}
}
