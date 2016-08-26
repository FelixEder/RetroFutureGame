using UnityEngine;
using System.Collections;

public class TutorialTrigger : MonoBehaviour {
	public string tutorial;
	TutorialManager manager;

	void Start() {
		manager = GameObject.Find("tutorialPanel").GetComponent<TutorialManager> ();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag.Equals ("Char")) {
			Debug.Log ("Tutorial triggered");
			manager.DisplayTutorial (tutorial);
			Destroy (gameObject);
		}
	}
}
