using UnityEngine;
using System.Collections;

public class TutorialTrigger : MonoBehaviour {
	
	public float timeToDisplay;
	public bool stringIsHideKey;
	public string tutorialString;
	[TextArea]
	public string tutorialText;

	TutorialManager manager;
	
	/*
	void Start() {
		manager = GameObject.Find("tutorialPanel").GetComponent<TutorialManager>();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag.Equals("Player")) {
			Debug.Log("Tutorial triggered");
			manager.DisplayTutorial(tutorial, tutorialText, stringIsHideKey, timeToDisplay);
			Destroy(gameObject);
		}
	}
	*/
	public TutorialMover tutorialMover;
	public GameObject tutorial;
	public Rigidbody2D target;
	
	
	
	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag.Equals("Player"))
			tutorialMover.SetCurrent(tutorial, target);
	}
	
	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.tag.Equals("Player"))
			tutorialMover.ChangeTarget(col.GetComponent<Rigidbody2D>());
	}
}
