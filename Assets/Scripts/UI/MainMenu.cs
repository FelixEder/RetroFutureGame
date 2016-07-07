using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public void LoadLevel(string level) {
		SceneManager.LoadScene (level);
	}
}
