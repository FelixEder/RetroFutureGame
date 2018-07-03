using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialArea : MonoBehaviour {
	public TutorialMover tutorialMover;

	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.tag.Equals("Player"))
			tutorialMover.DisableCurrent();
	}
}
