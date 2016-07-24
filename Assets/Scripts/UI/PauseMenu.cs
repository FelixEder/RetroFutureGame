using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	GameObject[] pauseObjects, hideOnPlay;
	EventSystem eventSystem;
	bool cursorVisible = true;

	void Start() {
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		eventSystem = GameObject.Find ("EventSystem").GetComponent<EventSystem> ();
		HidePaused();
	}
		
	void Update() {
		//uses the Escape button to pause and unpause the game
		if (Input.GetButtonDown("Pause"))
			PauseControl ();
		if ((Input.GetAxis ("Mouse X") != 0 || Input.GetAxis ("Mouse Y") != 0) && !cursorVisible) {
			Cursor.visible = true;
			cursorVisible = true;
		}
		if ((Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0) && cursorVisible) {
			Cursor.visible = false;
			cursorVisible = false;
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
			SetSelected (GameObject.Find("ResumeButton"));
		}
		else if (Time.timeScale == 0) {
			Time.timeScale = 1;
			SetSelected (null);
			HideOnPlay ();
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

	public void HideOnPlay() {
		if (hideOnPlay != null) {
			foreach (GameObject g in hideOnPlay) {
				g.SetActive (false);
			}
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

	public void ShowDialog(GameObject dialog) {
		dialog.SetActive (true);
		hideOnPlay = GameObject.FindGameObjectsWithTag ("HideOnPlay");
	}

	public void HideDialog(GameObject dialog) {
		dialog.SetActive (false);
		hideOnPlay = GameObject.FindGameObjectsWithTag ("HideOnPlay");
	}

	public void SetSelected(GameObject selected) {
		eventSystem.SetSelectedGameObject (selected);
	}
}