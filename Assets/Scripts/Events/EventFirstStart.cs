using UnityEngine;
using System.Collections;

public class EventFirstStart : MonoBehaviour {
	public GameObject lightF, lightB, daylightF, daylightB;

	void Start () {
		GameObject.Find ("InputManager").GetComponent<InputManager> ().Disable (5);
		StartCoroutine (LightStartup ());
	}

	void Update () {
	
	}

	IEnumerator LightStartup() {
		yield return new WaitForSeconds(3);
		LightIntensity (lightF, lightB, 0.5f); //on
		yield return new WaitForSeconds(0.05f);
		LightIntensity (lightF, lightB, 0.05f); //off
		yield return new WaitForSeconds(0.5f);
		LightIntensity (lightF, lightB, 0.7f); //on
		yield return new WaitForSeconds(0.05f);
		LightIntensity (lightF, lightB, 0); //off
		yield return new WaitForSeconds(1);
		LightIntensity (lightF, lightB, 1); //on
		yield return new WaitForSeconds(0.1f);
		LightIntensity (lightF, lightB, 0); //off
		yield return new WaitForSeconds(1);
		LightIntensity (lightF, lightB, 1); //on
		LightIntensity (daylightF, daylightB, 1); //daylight on
	}

	void LightIntensity(GameObject go1, GameObject go2, float intensity) {
		go1.GetComponent<Light> ().intensity = intensity;
		go2.GetComponent<Light> ().intensity = intensity;
	}
}
