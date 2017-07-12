using UnityEngine;
using System.Collections;

public class CameraTrigger : MonoBehaviour {
	public GameObject cameraGameobject;
	float lastX = 0, lastY = 0, lastSize = 5;
	GameObject lastFocus;

	void OnTriggerEnter2D(Collider2D col) {
		Debug.Log("Camera | Adjusting | " + col.gameObject.name + " " + col.gameObject.layer);
		CameraAdjuster adjust = col.gameObject.GetComponent<CameraAdjuster>();
		cameraGameobject.GetComponent<CameraMovement>().AdjustPosition(adjust.GetX(), adjust.GetY(), adjust.GetSize(), adjust.GetSpeed(), adjust.GetFocus());
	}

	void OnTriggerExit2D(Collider2D col) {
		Debug.Log("Camera | Exited adjuster | " + col.gameObject.name);
		var adjust = col.gameObject.GetComponent<CameraAdjuster>();
		if(adjust.GetReset()) {
			Debug.Log("Camera | Reverting to " + lastX + ", " + lastY + ", " + lastSize + ", " + lastFocus);
			cameraGameobject.GetComponent<CameraMovement>().AdjustPosition(lastX, lastY, lastSize, adjust.GetSpeed(), lastFocus);
		}
		else {
			lastX = adjust.GetX();
			lastY = adjust.GetY();
			lastSize = adjust.GetSize();
			lastFocus = adjust.GetFocus();
		}
	}
}
