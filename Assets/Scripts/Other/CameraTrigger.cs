using UnityEngine;
using System.Collections;

public class CameraTrigger : MonoBehaviour {
	public GameObject cameraGameobject;

	void OnTriggerEnter2D(Collider2D col) {
		Debug.Log ("Adjusting Camera  | " + col.gameObject.name);
		CameraAdjuster adjust = col.gameObject.GetComponent<CameraAdjuster> ();
		cameraGameobject.GetComponent<CameraMovement>().AdjustPosition(adjust.GetX (), adjust.GetY (), adjust.GetSize ());
	}
}
