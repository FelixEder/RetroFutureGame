using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour {
	public Sprite hatched;
//	public GameObject[] hatchlings;

	// Use this for initialization
	void Start () {
		Invoke("Hatch", 5f);
	}
	
	void Hatch() {
		GetComponent<SpriteRenderer>().sprite = hatched;
		//Play hatch SoundFX	
		string spawnType = "";
		int critterChoice = Random.Range(0, 20);
		if(critterChoice < 5) {
			spawnType = "SmallCritter";
		}
		else if(critterChoice < 10) {
			spawnType = "JumpingCritter";
		}
		else if(critterChoice < 15) {
			spawnType = "CrawlerCritter";
		}
		else if(critterChoice < 20) {
			spawnType = "BigEyeGuy";
		} 
	
		GameObject instance = Instantiate(Resources.Load(spawnType), transform.position, Quaternion.identity) as GameObject;
		instance.transform.parent = transform.parent.parent;

		Invoke("Kill", 2f);
	}

	void Kill() {
		Destroy(gameObject);
	}
}