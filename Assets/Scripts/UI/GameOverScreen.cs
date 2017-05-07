using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour {
	GameObject[] gameoverObjects, prefabspawners, itemspawners;
	GameObject player;
	InputManager input;

	void Start() {
		input = GameObject.Find ("InputManager").GetComponent<InputManager>();

		gameoverObjects = GameObject.FindGameObjectsWithTag("ShowOnDeath");
		prefabspawners = GameObject.FindGameObjectsWithTag("PrefabSpawner");
		itemspawners = GameObject.FindGameObjectsWithTag("PIS");
		player = GameObject.Find ("Char");
		HideGameover ();
	}

	void Update () {
	
	}

	public void ShowGameover() {
		foreach (GameObject g in gameoverObjects)
			g.SetActive(true);
		GetComponent<PauseMenu> ().SetSelected (GetComponent<PauseMenu> ().firstSelected);
		input.Force (false, true);
//		Time.timeScale = 0;
	}

	public void HideGameover() {
		foreach (GameObject g in gameoverObjects)
			g.SetActive(false);
		GetComponent<PauseMenu> ().SetSelected (null);
		input.Force (true, false);
//		Time.timeScale = 1;
	}

	public void Respawn() {
		//Revive player.
		player.GetComponent<CharHealth> ().Revive ();

		//Reset prefabspawners.
		foreach (GameObject g in prefabspawners) {
			g.GetComponent<PrefabSpawner> ().KillChildren ();
			g.GetComponent<PrefabSpawner> ().SetToRespawn ();
		}

		//Reset undefeated bosses.
		GameObject[] bossSpawners = GameObject.FindGameObjectsWithTag("BossActivator");
		foreach (GameObject g in bossSpawners) {
			g.GetComponent<BossActivator> ().KillExtraChild ();
			g.GetComponent<BossActivator> ().Trigger (true);
		}

		//Destroy pickupable items.
		foreach (GameObject g in itemspawners)
			g.GetComponent<PickupableItemSpawner> ().KillChildren ();

		//Destroy monsterdrops.
		GameObject[] drops = GameObject.FindGameObjectsWithTag("Drops");
		foreach(GameObject g in drops)
			Destroy (g);

		//Place player at last checkpoint and hide gameover overlay.
		player.transform.position = player.GetComponent<Checkpoint> ().activeCheckpoint.transform.position;
		GameObject camera = GameObject.Find ("Main Camera");
		camera.transform.position = player.transform.position;
		camera.GetComponent<CameraMovement> ().followSpeed = 5;
		HideGameover ();
	}
}
