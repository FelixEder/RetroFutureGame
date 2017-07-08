using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CreditsManager : MonoBehaviour {

	// Use this for initialization
	void Start() {
		Invoke("LoadMainMenu", 5f);
	}

	void LoadMainMenu() {
		SceneManager.LoadScene("MainMenu");
	}
}
