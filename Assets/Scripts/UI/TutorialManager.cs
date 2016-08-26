using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialManager : MonoBehaviour {
	Text text;
	string currentTutorial;
	InputManager input;
	public Color panelColor, textColor;

	void Start() {
		text = transform.GetChild (0).GetComponent<Text> ();
		input = GameObject.Find ("InputManager").GetComponent<InputManager> ();
	}

	void Update() {
		if (currentTutorial != null) {
			switch (currentTutorial) {

			case "jump":
				if(input.GetKey("Jump"))
					HideTutorial(4);
				break;
			default:
				HideTutorial (5);
				break;
			}
		}
	}

	public void DisplayTutorial(string tutorial) {
		Debug.Log ("Tutorial called");
		switch (tutorial.ToLower()) {

		case "jump":
			text.text = "Tutorial\n\nPress SPACE to jump.\nHold SPACE to jump higher";
			break;
		case "health":
			text.text = "You just picked up extra health.";
			break;
		default:
			text.text = "ERROR";
			break;
		}
		currentTutorial = tutorial;
		StartCoroutine (ShowPanel ());
	}

	void HideTutorial(float time) {
		currentTutorial = null;
		StartCoroutine (HidePanel (time));
	}

	IEnumerator ShowPanel() {
		for (float a = 0; a < 1; a += 0.05f) {
			GetComponent<Image> ().color = new Color (panelColor.r, panelColor.g, panelColor.b, a);
			text.color = new Color (textColor.r, textColor.g, textColor.b, a);
			yield return new WaitForSeconds (0.01f);
		}
		GetComponent<Image> ().color = new Color (panelColor.r, panelColor.g, panelColor.b, 1);
		text.color = new Color (textColor.r, textColor.g, textColor.b, 1);
	}

	IEnumerator HidePanel(float time) {
		yield return new WaitForSeconds (time);
		for (float a = 1; a > 0; a -= 0.05f) {
			GetComponent<Image> ().color = new Color (panelColor.r, panelColor.g, panelColor.b, a);
			text.color = new Color (textColor.r, textColor.g, textColor.b, a);
			yield return new WaitForSeconds (0.01f);
		}
		GetComponent<Image> ().color = new Color (panelColor.r, panelColor.g, panelColor.b, 0);
		text.color = new Color (textColor.r, textColor.g, textColor.b, 0);
		text.text = null;
	}
}
