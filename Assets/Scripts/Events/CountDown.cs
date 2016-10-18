using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountDown : MonoBehaviour {
	int timeLeft = 300;
	public Text text;

	void Start () {
		text.enabled = true;
		InvokeRepeating ("Counting", 5, 1);
	}

	void Counting() {
		text.alignment = TextAnchor.MiddleCenter;
		timeLeft--;
		text.text = timeLeft.ToString();
		if (timeLeft <= 0) {
		//Load game-over screen
		}
	}
}
