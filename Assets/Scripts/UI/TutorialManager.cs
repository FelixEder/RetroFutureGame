using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TutorialManager : MonoBehaviour {
	public Color panelColor, textColor;

	string currentTutorial;
	float displayTime;
	bool tutorialIsKey;
	TextMeshProUGUI text;
	InputManager input;

	//ADD IMAGE UI TO PANEL AND CHANGE IMAGE TO KEY.

	void Start() {
		text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		input = GameObject.Find("InputManager").GetComponent<InputManager>();
	}

	void Update() {
		if(currentTutorial != null) {
			if(tutorialIsKey) {
				if(input.GetKey(currentTutorial))
					HideTutorial(displayTime);
			}
			else {
				switch(currentTutorial) {

					case "secondjump":
						if(GameObject.Find("Player").GetComponent<PlayerJump>().hasSecondJumped)
							HideTutorial(displayTime);
						break;

					default:
						HideTutorial(displayTime);
						break;
				}
			}
		}
	}

	public void DisplayTutorial(string tutorial, string tutorialText, bool stringIsKey, float time) {
		StopAllCoroutines();

		Debug.Log("Display tutorial [ " + tutorial + " ]");

		text.text = tutorialText;
		text.UpdateMeshPadding();

		tutorialIsKey = stringIsKey;
		displayTime = time;
		currentTutorial = tutorial;

		StartCoroutine(ShowPanel());

	}

	void HideTutorial(float time) {
		currentTutorial = null;
		StartCoroutine(HidePanel(time));
	}

	IEnumerator ShowPanel() {
		for(float a = 0; a < panelColor.a; a += 0.05f) {
			GetComponent<Image>().color = new Color(panelColor.r, panelColor.g, panelColor.b, a);
			text.color = new Color(textColor.r, textColor.g, textColor.b, a);
			yield return new WaitForSeconds(0.01f);
		}
		GetComponent<Image>().color = new Color(panelColor.r, panelColor.g, panelColor.b, panelColor.a);
		text.color = new Color(textColor.r, textColor.g, textColor.b, textColor.a);
	}

	IEnumerator HidePanel(float time) {
		yield return new WaitForSeconds(time);
		for(float a = panelColor.a; a > 0; a -= 0.05f) {
			GetComponent<Image>().color = new Color(panelColor.r, panelColor.g, panelColor.b, a);
			text.color = new Color(textColor.r, textColor.g, textColor.b, a);
			yield return new WaitForSeconds(0.01f);
		}
		GetComponent<Image>().color = new Color(panelColor.r, panelColor.g, panelColor.b, 0);
		text.color = new Color(textColor.r, textColor.g, textColor.b, 0);
		text.text = null;
	}
}
