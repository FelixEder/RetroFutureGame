using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour {
	GameObject[] pauseObjects;

	void Start() {
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		HidePaused();
	}
		
	void Update() {
	//uses the Escape button to pause and unpause the game
		if (Input.GetButtonDown("Pause"))
			PauseControl ();
	}

	//Reloads the Level
	public void Reload() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	//controls the pausing of the scene
	public void PauseControl() {
		if (Time.timeScale == 1) {
			Time.timeScale = 0;
			ShowPaused();
		}
		else if (Time.timeScale == 0) {
			Time.timeScale = 1;
			HidePaused();
		}
	}

	//shows objects with ShowOnPause tag
	public void ShowPaused() {
		foreach (GameObject g in pauseObjects) {
			g.SetActive(true);
		}
	}

	//hides objects with ShowOnPause tag
	public void HidePaused() {
		foreach (GameObject g in pauseObjects) {
			g.SetActive(false);
		}
	}

	//loads inputted level
	public void LoadLevel(string level) {
		SceneManager.LoadScene(level);
	}

	public void Resume() {
		GameObject.Find ("char").GetComponent<CharJump> ().holdJump = true; //prevent jumping when resuming
		Time.timeScale = 1;
		HidePaused ();
	}
}