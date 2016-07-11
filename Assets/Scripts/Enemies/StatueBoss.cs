using UnityEngine;
using System.Collections;

	public class StatueBoss : MonoBehaviour {
	GameObject LeftEye, RightEye, LeftBlock, RightBlock;

		void Start() {
		LeftEye = this.gameObject.transform.GetChild (0);
		RightEye = this.gameObject.transform.GetChild (1);
		LeftBlock = this.gameObject.transform.GetChild (2);
		RightBlock = this.gameObject.transform.GetChild (3);
		}

		void Update() {
			if (LeftEye == null && RightEye == null) {
				Defeated ();
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
