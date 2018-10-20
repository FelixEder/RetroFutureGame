using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour {
	public Sprite regular, hatched;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer>().sprite = regular;
		Invoke("Hatch", 10f);
	}
	
	void Hatch() {
		GetComponent<SpriteRenderer>().sprite = hatched;
		//Play hatch SoundFX
		string spawnType = "";
		int critterChoice = Random.Range(0, 20);
		if(critterChoice < 5) {
			spawnType = "SmallCritter";
		}
		else if(critterChoice < 9) {
			spawnType = "JumpingCritter";
		}
		else if(critterChoice < 11) {
			spawnType = "CrawlerCritter";
		}
		else if(critterChoice < 14) {
			spawnType = "BigEyeGuy";
		}
		else
			return;
		GameObject instance = Instantiate(Resources.Load(spawnType), transform.position, Quaternion.identity) as GameObject;
		instance.transform.parent = transform.parent.parent;
		instance.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.x * 1.5f, 0);

		Invoke("Destroy", 2f);
	}

	void Destroy() {
		Destroy(gameObject);
	}
}