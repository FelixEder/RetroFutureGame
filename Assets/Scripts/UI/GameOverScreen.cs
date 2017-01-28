using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour {
	GameObject[] gameoverObjects, prefabspawners;
	GameObject player;
	InputManager input;

	void Start() {
		gameoverObjects = GameObject.FindGameObjectsWithTag("ShowOnDeath");
		prefabspawners = GameObject.FindGameObjectsWithTag("PrefabSpawner");
		input = GameObject.Find ("InputManager").GetComponent<InputManager> ();
		player = GameObject.Find ("Char");
		HideGameover ();
	}

	void Update () {
	
	}

	public void ShowGameover() {
		foreach (GameObject g in gameoverObjects)
			g.SetActive(true);
		Time.timeScale = 0;
	}

	public void HideGameover() {
		foreach (GameObject g in gameoverObjects)
			g.SetActive(false);
		Time.timeScale = 1;
	}

	public void Respawn() {
		player.GetComponent<CharHealth> ().MaximizeHealth ();
		player.GetComponent<CharEnergy> ().MaximizeEnergy ();
		foreach (GameObject g in prefabspawners) {
			g.GetComponent<PrefabSpawner> ().KillChildren ();
			g.GetComponent<PrefabSpawner> ().SetToRespawn ();
		}
		player.transform.position = player.GetComponent<Checkpoint> ().activeCheckpoint.position;
		HideGameover ();
	}
}
