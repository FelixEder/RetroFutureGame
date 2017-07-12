using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour {
	public GameObject _camera;

	GameObject[] prefabspawners, itemspawners;
	GameObject player;
	MenuControl menu;
	InputManager input;

	void Start() {
		menu = GetComponent<MenuControl>();
		input = GameObject.Find("InputManager").GetComponent<InputManager>();
		player = GameObject.Find("Player");

		prefabspawners = GameObject.FindGameObjectsWithTag("PrefabSpawner");
		itemspawners = GameObject.FindGameObjectsWithTag("PIS");
		player = GameObject.Find("Player");
		menu.HideOverlay();
	}

	public void Gameover() {
		input.Force(false, true);
		menu.ShowOverlay();
	}

	public void Respawn() {
		//Revive player.
		player.GetComponent<PlayerHealth>().Revive();

		//Reset prefabspawners.
		foreach(GameObject g in prefabspawners) {
			g.GetComponent<PrefabSpawner>().KillChildren();
			g.GetComponent<PrefabSpawner>().SetToRespawn();
		}

		//Reset undefeated bosses.
		GameObject[] bossSpawners = GameObject.FindGameObjectsWithTag("BossActivator");
		foreach(GameObject g in bossSpawners) {
			g.GetComponent<BossActivator>().KillExtraChild();
			g.GetComponent<BossActivator>().Trigger(true);
		}

		//Destroy pickupable items.
		foreach(GameObject g in itemspawners)
			g.GetComponent<PickupableItemSpawner>().KillChildren();

		//Destroy monsterdrops.
		GameObject[] drops = GameObject.FindGameObjectsWithTag("Drops");
		foreach(GameObject g in drops)
			Destroy(g);

		//Place player at last checkpoint, reset camera and hide gameover overlay.
		player.transform.position = player.GetComponent<Checkpoint>().activeCheckpoint.transform.position;
		player.transform.position += new Vector3(0, 1, 0);
		//_camera.transform.position = player.transform.position;
		_camera.GetComponent<CameraMovement>().followSpeed = 5;

		input.Force(true, false);
		menu.HideOverlay();
	}
}
