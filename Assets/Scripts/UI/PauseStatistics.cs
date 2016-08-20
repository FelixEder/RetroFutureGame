using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseStatistics : MonoBehaviour {
	public GameObject timeText;

	void Update () {
		timeText.GetComponent<Text> ().text = ((int)Time.timeSinceLevelLoad / 60 / 60).ToString("D2") + ":" + ((int)Time.timeSinceLevelLoad / 60).ToString("D2") + ":" + ((int)Time.timeSinceLevelLoad % 60).ToString("D2");
	}
}
