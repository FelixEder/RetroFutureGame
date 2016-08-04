using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	GameObject[] pauseObjects, hideOnPlay;
	GameObject lastSelected;
	EventSystem eventSystem;
	InputManager input;
	bool inputWasDisabled;
//	bool cursorVisible = true;

	void Start() {
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		eventSystem = GameObject.Find ("EventSystem").GetComponent<EventSystem> ();
		input = GameObject.Find ("InputManager").GetComponent<InputManager> ();
		HidePaused();
	}
		
	void Update() {
		//uses the Escape button to pause and unpause the game
		if (Input.GetButtonDown("Pause"))
			PauseControl ();
/*
		if ((Input.GetAxis ("Mouse X") != 0 || Input.GetAxis ("Mouse Y") != 0) && !cursorVisible) {
			Cursor.visible = true;
			cursorVisible = true;
		}
		if ((Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0) && cursorVisible) {
			Cursor.visible = false;
			cursorVisible = false;
		}
*/
		//Detects if none of the buttons are selected and selects the last selected
		if (GameObject.Find ("EventSystem").GetComponent<EventSystem> ().currentSelectedGameObject == null)
			SetSelected (lastSelected);
	}

	//Reloads the Level
	public void Reload() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	//controls the pausing of the scene
	public void PauseControl() {
		if (Time.timeScale == 1) {
			Time.timeScale = 0;
			if (!input.Enabled ())
				inputWasDisabled = true;
			input.Disable ();
			ShowPaused();
			SetSelected (GameObject.Find("ResumeButton"));
		}
		else if (Time.timeScale == 0) {
			Time.timeScale = 1;
			if (!inputWasDisabled)
				input.Disable (0.2f);
			inputWasDisabled = false;
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

	//hides objects with HideOnPlay tag
	public void HideOnPlay() {
		if (hideOnPlay != null) {
			foreach (GameObject g in hideOnPlay) {
				g.SetActive (false);
			}
		}
	}

	//loads inputted level
	public void LoadLevel(string level) {
		Time.timeScale = 1;
		SceneManager.LoadScene(level);
	}

	//resumes the game
	public void Resume() {
		GameObject.Find ("Char").GetComponent<CharJump> ().holdJump = true; //prevent jumping when resuming
		Time.timeScale = 1;
		HidePaused ();
	}

	//shows a gameobject and updates the hideOnPlay array
	public void ShowDialog(GameObject dialog) {
		dialog.SetActive (true);
		UpdateHideOnPlay ();
	}

	//hides a gameobject and updates the hideOnPlay array
	public void HideDialog(GameObject dialog) {
		dialog.SetActive (false);
		UpdateHideOnPlay ();
	}

	public void UpdateHideOnPlay() {
		hideOnPlay = GameObject.FindGameObjectsWithTag ("HideOnPlay");
	}

	//sets a gameobject as selected and updates lastSelected
	public void SetSelected(GameObject selected) {
		eventSystem.SetSelectedGameObject (selected);
		lastSelected = selected;
	}
}