using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class BootLogo : MonoBehaviour {
	void Start() {
		StartCoroutine (TransitionImage ());
	}
	IEnumerator TransitionImage() {
		GetComponent<Image>().CrossFadeAlpha(0, 0, false);
		yield return new WaitForSeconds(1);
		GetComponent<Image>().CrossFadeAlpha(1, 2, false);
		yield return new WaitForSeconds(8);
		GetComponent<Image>().CrossFadeAlpha(0, 2, false);
		yield return new WaitForSeconds(4);
		SceneManager.LoadScene ("MainMenu");
	}
}
