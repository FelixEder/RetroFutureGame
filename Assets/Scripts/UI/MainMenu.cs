using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour {
	public GameObject loadingImage;
	public Material glitchMaterial, standardMaterial;
	public Color standardTextColor, highlightedTextColor;
	GameObject lastSelected;

	void Update() {
		//Detects if none of the buttons are selected and selects the last selected
		if(GameObject.Find("EventSystem").GetComponent<EventSystem>().currentSelectedGameObject == null)
			SetSelected(lastSelected);
	}

	/**Loads scene "level".*/
	public void LoadLevel(string level) {
		loadingImage.SetActive(true);
		SceneManager.LoadScene(level);
	}

	/**Quits the application.*/
	public void ExitApplication() {
		Debug.Log("Exit application");
		Application.Quit();
	}

	/**Sets "selected" gameobject as selected and updates lastSelected.*/
	public void SetSelected(GameObject selected) {
		GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(selected);
		lastSelected = selected;
	}

	/**Sets the color of the gameobject's text component to specified color.*/
	public void HoverTextColor(GameObject text) {
		text.GetComponent<Text>().color = highlightedTextColor;
	}

	/**Sets the color of the gameobject's text component to specified color.*/
	public void NormalTextColor(GameObject text) {
		text.GetComponent<Text>().color = standardTextColor;
	}

	/**Sets the material of the gameobject's text component to specified material.*/
	public void HoverMaterial(GameObject text) {
		text.GetComponent<Text>().material = glitchMaterial;
	}

	/**Sets the material of the gameobject's text component to specified material.*/
	public void NormalMaterial(GameObject text) {
		text.GetComponent<Text>().material = standardMaterial;
	}
}
