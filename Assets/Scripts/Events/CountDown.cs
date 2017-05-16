using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountDown : MonoBehaviour {
	int timeLeft = 295;
	public Text text;
	public GameObject gamover;

	void Start () {
		text.enabled = true;
		text.alignment = TextAnchor.MiddleCenter;
		InvokeRepeating ("Counting", 5, 1);

	}

	void Counting() {
		if (timeLeft > 0) {
			text.text = (timeLeft / 60).ToString ("D1") + ":" + (timeLeft % 60).ToString ("D2");
		} else if (timeLeft == -3) {
			text.enabled = false;
			gamover.GetComponent<GameOverScreen> ().Gameover ();
		}
		else {
			text.text = "KABOOM";
			//Load game-over screen
		}
		timeLeft--;
	}
}
