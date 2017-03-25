using UnityEngine;
using System.Collections;

public class Throw : MonoBehaviour {
	public int maxRocks, spawnLoc = 2;
	GameObject FBIM;

	void Start() {
	}

	void OnEnable() {
		if (FBIM == null) 
			FBIM = transform.parent.parent.parent.parent.gameObject;
		Debug.Log ("Boss throws things");
		//Also play relevant soundFX
		Throwing();
	}

	void Throwing() {
		string spawnType = "";
		int item = Random.Range (0, 10);
		if (item < 8 && FBIM.transform.childCount < maxRocks) {
			spawnType = "Rock";
		} else if (item < 9) {
			spawnType = "HealthDrop";
		} else {
			spawnType = "EnergyDrop";
		}
		GameObject instance = (GameObject) DropSpawner (spawnType, transform.position);

		//	Instantiate (Resources.Load (spawnType), transform.position, Quaternion.identity) as GameObject;
		if(spawnType.Equals( "Rock")) {
			instance.GetComponent<Rigidbody2D>().AddForce(new Vector2(8f, 3f), ForceMode2D.Impulse);
			instance.transform.SetParent (FBIM.transform);
		}
	}

	Object DropSpawner(string type, Vector3 pos) {
		Vector3 dropLoc = new Vector3 (pos.x + Random.Range (-spawnLoc, spawnLoc), pos.y + Random.Range (-spawnLoc, spawnLoc), pos.z);
		return Instantiate (Resources.Load (type), dropLoc, Quaternion.identity);
	}
}