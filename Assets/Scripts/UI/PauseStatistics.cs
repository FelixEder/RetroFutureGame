using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class PauseStatistics : MonoBehaviour {
	public GameObject timeText, tooltipText;
	public MenuControl menuControl;
	public EventSystem eventSystem;

	void Update() {
		timeText.GetComponent<Text>().text = ((int) Time.timeSinceLevelLoad / 60 / 60).ToString("D2") + ":" + ((int) Time.timeSinceLevelLoad / 60).ToString("D2") + ":" + ((int) Time.timeSinceLevelLoad % 60).ToString("D2");
		
//		if(eventSystem.currentSelectedGameObject != null) {
			if(eventSystem.currentSelectedGameObject.GetComponent<UpgradeSlot>() != null) {
				tooltipText.GetComponent<Text>().text = eventSystem.currentSelectedGameObject.GetComponent<UpgradeSlot>().tooltip;
				tooltipText.transform.parent.GetComponent<Image>().enabled = true;
			}
			else {
				tooltipText.GetComponent<Text>().text = null;
				tooltipText.transform.parent.GetComponent<Image>().enabled = false;
			}
//		}
	}
}
