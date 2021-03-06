﻿using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour {
	GameObject[] prefabspawners, itemspawners;
	GameObject player;
	InputManager input;
	Animator anim;

	void Start() {
		input = GameObject.Find("InputManager").GetComponent<InputManager>();
		player = GameObject.Find("Player");

		prefabspawners = GameObject.FindGameObjectsWithTag("PrefabSpawner");
		itemspawners = GameObject.FindGameObjectsWithTag("PIS");
		anim = GetComponent<Animator>();
	}

	public void Gameover() {
		input.Force(false, true);
		anim.SetTrigger("GameOver");
	}

	public void Respawn() {

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

		//Revive player.
		player.GetComponent<PlayerHealth>().Revive();

		input.Force(true, false);
	}
}
