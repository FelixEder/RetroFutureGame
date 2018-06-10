using UnityEngine;
using System.Collections;

public class BossActivator : MonoBehaviour {
	public GameObject prefab;
	int startChildren;

	void Start() {
		startChildren = transform.childCount;
	}

	void Spawn() {
		if(transform.childCount == startChildren) {
			GameObject instance = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
			instance.transform.parent = transform;
			//		instance.GetComponent<SpriteRenderer> ().sortingOrder = (int)Time.timeSinceLevelLoad;
		}
	}

	public void KillExtraChild() {
		if(transform.childCount > startChildren) {
			GameObject.Destroy(transform.GetChild(startChildren).gameObject);
			Debug.Log("Prefab spawner:\nKilled the extra child.");
		}
	}

	public void Trigger(bool state) {
		GetComponent<Collider2D>().enabled = state;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(!col.isTrigger && col.gameObject.tag.Equals("Player")) {
			Trigger(false);
			Spawn();
		}
	}
}
