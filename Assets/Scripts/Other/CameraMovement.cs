using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public float followSpeed; float adjustX, adjustY, size = 5;
	public GameObject followTarget;
	Vector3 target, position;

	void Update () {
		target = followTarget.transform.position;
		position = transform.position;
		transform.position = new Vector3 (Mathf.Lerp(position.x, target.x + adjustX, Time.deltaTime * followSpeed), Mathf.Lerp(position.y, target.y + adjustY, Time.deltaTime * followSpeed), -10);
		GetComponent<Camera> ().orthographicSize = Mathf.Lerp(GetComponent<Camera> ().orthographicSize, size, Time.deltaTime);
	}

	public void AdjustPosition(float x, float y, float newSize) {
		adjustX = x;
		adjustY = y;
		size = newSize;
	}
}