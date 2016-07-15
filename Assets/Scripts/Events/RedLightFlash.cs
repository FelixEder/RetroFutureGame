using UnityEngine;
using System.Collections;

public class RedLightFlash : MonoBehaviour {
	public GameObject light1, light2;
	float intensity;

	void Start() {
		StartCoroutine (LightFlicker ());
	}

	IEnumerator LightFlicker() {
		while (true) {
			intensity = Random.Range (0.0f, 1.0f);
			yield return new WaitForSeconds (Random.Range (0.5f, 2.0f));
			light1.GetComponent<Light> ().intensity = intensity;
			light2.GetComponent<Light> ().intensity = intensity;
			yield return new WaitForSeconds (Random.Range (0.1f, 0.3f));
			light1.GetComponent<Light> ().intensity = 0;
			light2.GetComponent<Light> ().intensity = 0;

		}
	}
}
