using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class VictoryScreen : MonoBehaviour {
	GameObject[] victoryObjects;
	GameObject player;
	InputManager input;

	void Start() {
		victoryObjects = GameObject.FindGameObjectsWithTag("ShowOnVictory");
		HideVictory ();
	}

	void Update () {
		
	}

	public void ShowVictory() {
		foreach (GameObject g in victoryObjects)
			g.SetActive(true);
		Time.timeScale = 0;
	}

	public void HideVictory() {
		foreach (GameObject g in victoryObjects)
			g.SetActive(false);
	}

	public void Continue() {
		Time.timeScale = 1;
		SceneManager.LoadScene ("Credits");
	}
}