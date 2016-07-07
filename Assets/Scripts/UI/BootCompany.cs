using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class BootCompany : MonoBehaviour {
	void Start() {
		StartCoroutine (Wait ());
	}
	IEnumerator Wait() {
		GetComponent<Image>().CrossFadeAlpha(0, 0, false);
		yield return new WaitForSeconds(1);
		GetComponent<Image>().CrossFadeAlpha(1, 2, false);
		yield return new WaitForSeconds(8);
		GetComponent<Image>().CrossFadeAlpha(0, 2, false);
		yield return new WaitForSeconds(4);
		SceneManager.LoadScene ("MainMenu");
	}
}
