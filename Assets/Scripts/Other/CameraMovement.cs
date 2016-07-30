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

	public void AdjustPosition(float x, float y, float newSize, float speed) {
		StopAllCoroutines ();
		StartCoroutine (AdjustTransition (x, y, newSize, speed));
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
}