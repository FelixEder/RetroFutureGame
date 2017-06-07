using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public float followSpeed; float adjustX, adjustY, size = 5;
	public GameObject followTarget, focusTarget;
	Vector3 target, position, followPos;

	void Start() {
		transform.position = followTarget.transform.position;
		StartCoroutine (AdjustFocus (5));
	}

	void Update () {
		followPos = followTarget.transform.position;
		position = transform.position;
		transform.position = new Vector3 (Mathf.Lerp (position.x, target.x + adjustX, Time.deltaTime * followSpeed), Mathf.Lerp(position.y, target.y + adjustY, Time.deltaTime * followSpeed), -10);
		GetComponent<Camera> ().orthographicSize = Mathf.Lerp (GetComponent<Camera> ().orthographicSize, size, Time.deltaTime);
		//Background camera
		transform.GetChild(0).GetComponent<Camera>().fieldOfView = Mathf.Lerp (transform.GetChild(0).GetComponent<Camera>().fieldOfView, 40 + size * 7, Time.deltaTime);
		//Foreground camera
		transform.GetChild(1).GetComponent<Camera>().fieldOfView = Mathf.Lerp (transform.GetChild(1).GetComponent<Camera>().fieldOfView, 20 + size * 10, Time.deltaTime);
	}

	public void AdjustPosition(float x, float y, float newSize, float speed, GameObject focus) {
		focusTarget = focus;
		StopAllCoroutines ();
		StartCoroutine (AdjustTransition (x, y, newSize, speed));
		StartCoroutine (AdjustFocus (speed));
	}

	IEnumerator AdjustTransition(float x, float y, float newSize, float speed) {
		while (adjustX < x - 0.1f || adjustY < y - 0.1f || size < newSize - 0.1f || adjustX > x + 0.1f || adjustY > y + 0.1f || size > newSize + 0.1f) {
			adjustX = Mathf.Lerp (adjustX, x, Time.deltaTime * speed);
			adjustY = Mathf.Lerp (adjustY, y, Time.deltaTime * speed);
			size = Mathf.Lerp (size, newSize, Time.deltaTime * speed);
			yield return new WaitForSeconds (0.01f);
		}
		adjustX = x;
		adjustY = y;
		size = newSize;
	}

	IEnumerator AdjustFocus(float speed) {
		while (focusTarget != null) {
			target = new Vector2 (Mathf.Lerp (target.x, (followPos.x + focusTarget.transform.position.x) / 2, Time.deltaTime * speed), Mathf.Lerp (target.y, (followPos.y + focusTarget.transform.position.y) / 2, Time.deltaTime * speed));
			yield return new WaitForSeconds (0.01f);
		}
		while (target.x < followPos.x - 0.1f || target.y < followPos.y - 0.1f || target.x > followPos.x + 0.1f || target.y > followPos.y + 0.1f) {
			target = new Vector2 (Mathf.Lerp(target.x, followTarget.transform.position.x, Time.deltaTime * speed), Mathf.Lerp(target.y, followTarget.transform.position.y, Time.deltaTime * followSpeed * 2));
			yield return new WaitForSeconds (0.01f);
		}
		while (focusTarget == null) {
			target = followPos;
			yield return 0;
		}
	}
}