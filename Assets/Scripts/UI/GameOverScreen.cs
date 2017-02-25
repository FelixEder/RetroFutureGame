using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour {
	GameObject[] gameoverObjects, prefabspawners, itemspawners;
	GameObject player;

	void Start() {
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
		Time.timeScale = 0;
	}

	public void HideGameover() {
		foreach (GameObject g in gameoverObjects)
			g.SetActive(false);
		GetComponent<PauseMenu> ().SetSelected (null);
		Time.timeScale = 1;
	}

	public void Respawn() {
		player.GetComponent<CharHealth> ().MaximizeHealth ();
		player.GetComponent<CharEnergy> ().MaximizeEnergy ();

		foreach (GameObject g in prefabspawners) {
			g.GetComponent<PrefabSpawner> ().KillChildren ();
			g.GetComponent<PrefabSpawner> ().SetToRespawn ();
		}


		GameObject[] bossSpawners = GameObject.FindGameObjectsWithTag("BossActivator");
		foreach (GameObject g in bossSpawners) {
			g.GetComponent<BossActivator> ().KillExtraChild ();
			g.GetComponent<BossActivator> ().Trigger (true);
		}
		
		foreach (GameObject g in itemspawners)
			g.GetComponent<PickupableItemSpawner> ().KillChildren ();
		
		GameObject[] drops = GameObject.FindGameObjectsWithTag("Drops");
		foreach(GameObject g in drops)
			Destroy (g);
		player.transform.position = player.GetComponent<Checkpoint> ().activeCheckpoint.transform.position;
		HideGameover ();
	}
}
