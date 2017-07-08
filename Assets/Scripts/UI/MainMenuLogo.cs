using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuLogo : MonoBehaviour {
	bool growing, clockwise;
	float rotation = 0;
	RectTransform rectTransform;

	void Start() {
		rectTransform = gameObject.GetComponent<RectTransform>();
		StartCoroutine(Resize());
		StartCoroutine(Rotate());
	}

	void Update() {
		if(rectTransform.localScale.x >= 1)
			growing = false;
		else if(rectTransform.localScale.x <= 0.9)
			growing = true;
		if(rotation <= -5)
			clockwise = false;
		else if(rotation >= 5)
			clockwise = true;
	}

	IEnumerator Resize() {
		while(true) {
			while(!growing) {
				rectTransform.localScale -= new Vector3(Time.deltaTime / 10, Time.deltaTime / 10, 0);
				yield return new WaitForSeconds(0.01f);
			}

			while(growing) {
				rectTransform.localScale += new Vector3(Time.deltaTime / 10, Time.deltaTime / 10, 0);
				yield return new WaitForSeconds(0.01f);
			}
		}
	}

	IEnumerator Rotate() {
		while(true) {
			while(!clockwise) {
				rotation += Time.deltaTime;
				rectTransform.localRotation = Quaternion.Euler(0, 0, rotation);
				yield return new WaitForSeconds(0.01f);
			}

			while(clockwise) {
				rotation -= Time.deltaTime;
				rectTransform.localRotation = Quaternion.Euler(0, 0, rotation);
				yield return new WaitForSeconds(0.01f);
			}
		}
	}
}
