using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class VictoryScreen : MonoBehaviour {
	MenuControl menu;
	GameObject player;

	void Start() {
		menu = GetComponent<MenuControl> ();
		menu.HideOverlay ();
	}

	public void Victory () {
		Time.timeScale = 0;
		menu.ShowOverlay ();
	}

	public void Continue() {
		Time.timeScale = 1;
		SceneManager.LoadScene ("Credits");
	}
}