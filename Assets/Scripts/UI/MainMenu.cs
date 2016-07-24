using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public GameObject loadingImage;

	public void LoadLevel(string level) {
		loadingImage.SetActive (true);
		SceneManager.LoadScene (level);
	}

	public void ExitApplication() {
		Application.Quit ();
	}

	public void SetSelected(GameObject selected) {
		GameObject.Find ("EventSystem").GetComponent<EventSystem> ().SetSelectedGameObject (selected);
	}

	public void HoverTextColor(GameObject text) {
		text.GetComponent<Text> ().color = new Color(0.43359375f, 0.6484375f, 1.0f);
	}

	public void NormalTextColor(GameObject text) {
		text.GetComponent<Text> ().color = new Color(1.0f, 1.0f, 1.0f);
	}
}
