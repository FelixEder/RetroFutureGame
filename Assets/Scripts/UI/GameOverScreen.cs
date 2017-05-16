using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour {
	GameObject[] prefabspawners, itemspawners;
	GameObject player;
	MenuControl menu;

	void Start () {
		menu = GetComponent<MenuControl> ();
		player = GameObject.Find ("Char");

		prefabspawners = GameObject.FindGameObjectsWithTag("PrefabSpawner");
		itemspawners = GameObject.FindGameObjectsWithTag("PIS");
		player = GameObject.Find ("Char");
		menu.HideOverlay ();
	}

	public void Gameover () {
		menu.ShowOverlay ();
	}

	public void Respawn () {
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

		//Place player at last checkpoint, reset camera and hide gameover overlay.
		player.transform.position = player.GetComponent<Checkpoint> ().activeCheckpoint.transform.position;
		player.transform.position += new Vector3 (0, 1, 0);
		GameObject camera = GameObject.Find ("Main Camera");
		camera.transform.position = player.transform.position;
		camera.GetComponent<CameraMovement> ().followSpeed = 5;
		menu.HideOverlay ();
	}
}
