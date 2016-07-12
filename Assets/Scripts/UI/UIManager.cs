using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour {
GameObject[] pauseObjects;

// Use this for initialization
	void Start() {
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		HidePaused();
	}

	// Update is called once per frame
	void Update() {
	//uses the Escape button to pause and unpause the game
		if (Input.GetButtonDown("Pause")) {
			Debug.Log ("esc");
			PauseControl ();
	}
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
			Debug.Log ("paused");
		}
		else if (Time.timeScale == 0) {
			Time.timeScale = 1;
			HidePaused();
			Debug.Log ("unpaused");
		}
	}

//shows objects with ShowOnPause tag
	public void ShowPaused() {
		foreach (GameObject g in pauseObjects) {
			g.SetActive(true);
			Debug.Log ("showpause");
		}
	}

	//hides objects with ShowOnPause tag
	public void HidePaused() {
		foreach (GameObject g in pauseObjects) {
			g.SetActive(false);
			Debug.Log ("hidepause");
		}
	}

	//loads inputted level
	public void LoadLevel(string level) {
		SceneManager.LoadScene(level);
	}		
}