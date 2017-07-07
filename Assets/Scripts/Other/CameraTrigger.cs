using UnityEngine;
using System.Collections;

public class CameraTrigger : MonoBehaviour {
	public GameObject cameraGameobject;
	float lastX = 0, lastY = 0, lastSize = 5;
	GameObject lastFocus;

	void OnTriggerEnter2D (Collider2D col) {
		Debug.Log ("Adjusting Camera  | " + col.gameObject.name + " " + col.gameObject.layer);
		CameraAdjuster adjust = col.gameObject.GetComponent<CameraAdjuster> ();
		cameraGameobject.GetComponent<CameraMovement>().AdjustPosition(adjust.GetX (), adjust.GetY (), adjust.GetSize (), adjust.GetSpeed (), adjust.GetFocus());
	}

	void OnTriggerExit2D (Collider2D col) {
		Debug.Log ("Exited adjuster | " + col.gameObject.name);
		CameraAdjuster adjust = col.gameObject.GetComponent<CameraAdjuster> ();
		if (adjust.GetReset ()) {
			Debug.Log (lastX + "|" + lastY + "|" + lastSize + "|" + lastFocus);
			cameraGameobject.GetComponent<CameraMovement> ().AdjustPosition (lastX, lastY, lastSize, adjust.GetSpeed (), lastFocus);
		} else {
			lastX = adjust.GetX ();
			lastY = adjust.GetY ();
			lastSize = adjust.GetSize ();
			lastFocus = adjust.GetFocus ();
		}
	}
}
