using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	InputManager input;
	MenuControl menu;
	AudioControl audiocontrol;
	bool inputWasDisabled;

	void Start() {
		Time.timeScale = 1;

		input = GameObject.Find ("InputManager").GetComponent<InputManager> ();
		audiocontrol = GameObject.Find ("Audio").GetComponent<AudioControl> ();

		menu = GetComponent<MenuControl> ();
		menu.HideOverlay ();

	}
		
	void Update() {
		//uses the Escape button to pause and unpause the game
		if (Input.GetButtonDown ("Pause")) {
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
			Pause ();
		}
		else if (Time.timeScale == 0) {
			Play ();
		}
	}

	void Play () {
		Time.timeScale = 1;
		if (!inputWasDisabled)
			input.Disable (0.2f);
		inputWasDisabled = false;
		audiocontrol.SetMultiplier (100);
		menu.HideOverlay ();
	}

	void Pause () {
		Time.timeScale = 0;
		if (!input.Enabled ())
			inputWasDisabled = true;
		input.Disable ();
		audiocontrol.SetMultiplier (20);
		menu.ShowOverlay ();
	}

	//loads inputted level
	public void LoadLevel(string level) {
		Time.timeScale = 1;
		SceneManager.LoadScene(level);
	}

	//resumes the game
	public void Resume() {
		GameObject.Find ("Char").GetComponent<CharJump> ().holdJump = true; //prevent jumping when resuming
		PauseControl ();
		Debug.Log ("Resume");
	}
}