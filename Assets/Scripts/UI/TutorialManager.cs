using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialManager : MonoBehaviour {
	Text text;
	string currentTutorial;
	InputManager input;
	public Color panelColor, textColor;

	void Start() {
		text = transform.GetChild(0).GetComponent<Text>();
		input = GameObject.Find("InputManager").GetComponent<InputManager>();
	}

	void Update() {
		if(currentTutorial != null) {
			switch(currentTutorial) {

				case "jump":
					if(input.GetKey("Jump"))
						HideTutorial(4);
					break;
				case "punch":
					if(input.GetKey("Attack"))
						HideTutorial(10);
					break;
				case "float":
					if(input.GetKey("Float"))
						HideTutorial(4);
					break;
				case "secondjump":
					if(GameObject.Find("Char").GetComponent<CharJump>().hasSecondJumped)
						HideTutorial(4);
					break;
				case "checkpoint":
					HideTutorial(1.5f);
					break;

				default:
					HideTutorial(5);
					break;
			}
		}
	}

	public void DisplayTutorial(string tutorial) {
		StopAllCoroutines();

		Debug.Log("Display tutorial [ " + tutorial + " ]");
		switch(tutorial.ToLower()) {

			case "jump":
				text.text = "Tutorial\n\nPress SPACE to jump.\nHold SPACE to jump higher.";
				break;
			case "punch":
				text.text = "Tutorial\n\nPress K to punch.\nPunch doors to open them.";
				break;
			case "health":
				text.text = "You just picked up extra health.";
				break;
			case "float":
				text.text = "Leaf.\n\nUse SHIFT to float in the air.";
				break;
			case "secondjump":
				text.text = "Doublejump.\n\nPress SPACE in the air to jump even higher.";
				break;
			case "checkpoint":
				text.text = "Checkpoint reached";
				break;

			default:
				text.text = "undefined";
				break;
		}
		currentTutorial = tutorial;
		StartCoroutine(ShowPanel());
	}

	void HideTutorial(float time) {
		currentTutorial = null;
		StartCoroutine(HidePanel(time));
	}

	IEnumerator ShowPanel() {
		for(float a = 0; a < 1; a += 0.05f) {
			GetComponent<Image>().color = new Color(panelColor.r, panelColor.g, panelColor.b, a);
			text.color = new Color(textColor.r, textColor.g, textColor.b, a);
			yield return new WaitForSeconds(0.01f);
		}
		GetComponent<Image>().color = new Color(panelColor.r, panelColor.g, panelColor.b, 1);
		text.color = new Color(textColor.r, textColor.g, textColor.b, 1);
	}

	IEnumerator HidePanel(float time) {
		yield return new WaitForSeconds(time);
		for(float a = 1; a > 0; a -= 0.05f) {
			GetComponent<Image>().color = new Color(panelColor.r, panelColor.g, panelColor.b, a);
			text.color = new Color(textColor.r, textColor.g, textColor.b, a);
			yield return new WaitForSeconds(0.01f);
		}
		GetComponent<Image>().color = new Color(panelColor.r, panelColor.g, panelColor.b, 0);
		text.color = new Color(textColor.r, textColor.g, textColor.b, 0);
		text.text = null;
	}
}
