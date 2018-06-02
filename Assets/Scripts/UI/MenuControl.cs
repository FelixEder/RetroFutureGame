using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class MenuControl : MonoBehaviour {
	public GameObject firstSelected, lastSelected;
	public string overlayObjectsTag;

	GameObject[] overlayObjects, hideOnOverlayHideObjects;
	EventSystem eventSystem;

	void Awake() {
		overlayObjects = GameObject.FindGameObjectsWithTag(overlayObjectsTag);
		eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
	}

	void Update() {
		//Detects if none of the buttons are selected and selects the last selected
		if(eventSystem.currentSelectedGameObject == null)
			SetSelected(lastSelected);
	}

	public void ShowOverlay() {
		foreach(GameObject g in overlayObjects) {
			g.SetActive(true);
		}
		SetSelected(firstSelected);
	}

	//hides objects with ShowOnPause tag
	public void HideOverlay() {
		foreach(GameObject g in overlayObjects) {
			g.SetActive(false);
		}
		SetSelected(null);
		HideOnOverlayHide();
	}

	//hides objects with HideOnPlay tag
	public void HideOnOverlayHide() {
		if(hideOnOverlayHideObjects != null) {
			foreach(GameObject g in hideOnOverlayHideObjects) {
				g.SetActive(false);
			}
		}
	}

	//shows a gameobject and updates the hideOnPlay array
	public void ShowDialog(GameObject dialog) {
		dialog.SetActive(true);
		UpdateHideOnPlay();
	}

	//hides a gameobject and updates the hideOnPlay array
	public void HideDialog(GameObject dialog) {
		dialog.SetActive(false);
		UpdateHideOnPlay();
	}

	public void UpdateHideOnPlay() {
		hideOnOverlayHideObjects = GameObject.FindGameObjectsWithTag("HideOnPlay");
	}

	//sets a gameobject as selected and updates lastSelected
	public void SetSelected(GameObject selected) {
		eventSystem.SetSelectedGameObject(selected);
		lastSelected = selected;
	}
}