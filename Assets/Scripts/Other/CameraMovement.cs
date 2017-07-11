using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public float followSpeed, maxSpeed;
	public GameObject followTarget, focusTarget;

	float adjustX, adjustY, size = 5;
	Vector3 target, position, followPos;

	void Start() {
		transform.position = followTarget.transform.position;
		StartCoroutine(AdjustFocus(5));
	}

	void FixedUpdate() {
		followPos = followTarget.transform.position;
		position = transform.position;
		transform.position = new Vector2(Mathf.Lerp(position.x, target.x + adjustX, FollowSpeed()), Mathf.Lerp(position.y, target.y + adjustY, FollowSpeed()));
		//Main camera
		transform.GetChild(0).GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, size, Time.fixedDeltaTime);
		//Background camera
		transform.GetChild(1).GetComponent<Camera>().fieldOfView = Mathf.Lerp(transform.GetChild(0).GetComponent<Camera>().fieldOfView, 40 + size * 7, Time.fixedDeltaTime);
		//Foreground camera
		transform.GetChild(2).GetComponent<Camera>().fieldOfView = Mathf.Lerp(transform.GetChild(1).GetComponent<Camera>().fieldOfView, 20 + size * 10, Time.fixedDeltaTime);
	}

	float FollowSpeed() {
		var speed = followSpeed * 0.01f * Vector2.Distance(transform.position, followTarget.transform.position);
		if(speed > maxSpeed)
			speed = maxSpeed;
		return speed;
	}

	public void AdjustPosition(float x, float y, float newSize, float speed, GameObject focus) {
		focusTarget = focus;
		StopAllCoroutines();
		StartCoroutine(AdjustTransition(x, y, newSize, speed));
		StartCoroutine(AdjustFocus(speed));
	}

	IEnumerator AdjustTransition(float x, float y, float newSize, float speed) {
		while(adjustX < x - 0.1f || adjustY < y - 0.1f || size < newSize - 0.1f || adjustX > x + 0.1f || adjustY > y + 0.1f || size > newSize + 0.1f) {
			adjustX = Mathf.Lerp(adjustX, x, FollowSpeed() * speed);
			adjustY = Mathf.Lerp(adjustY, y, FollowSpeed() * speed);
			size = Mathf.Lerp(size, newSize, FollowSpeed() * speed);
			yield return new WaitForSeconds(0.01f);
		}
		adjustX = x;
		adjustY = y;
		size = newSize;
	}

	IEnumerator AdjustFocus(float speed) {
		while(focusTarget != null) {
			target = new Vector2(Mathf.Lerp(target.x, (followPos.x + focusTarget.transform.position.x) / 2, FollowSpeed() * speed), Mathf.Lerp(target.y, (followPos.y + focusTarget.transform.position.y) / 2, FollowSpeed() * speed));
			yield return new WaitForSeconds(0.01f);
		}
		while(target.x < followPos.x - 0.1f || target.y < followPos.y - 0.1f || target.x > followPos.x + 0.1f || target.y > followPos.y + 0.1f) {
			target = new Vector2(Mathf.Lerp(target.x, followTarget.transform.position.x, FollowSpeed() * speed), Mathf.Lerp(target.y, followTarget.transform.position.y, FollowSpeed() * followSpeed * 2));
			yield return new WaitForSeconds(0.01f);
		}
		while(focusTarget == null) {
			target = followPos;
			yield return 0;
		}
	}
}