using Cinemachine;
using UnityEngine;

public class VCamActivator : MonoBehaviour {
	public GameObject vCamManager, newVCam;

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name == "Player") {
		vCamManager.GetComponent<VCamManager>().activeCamera.GetComponent<CinemachineVirtualCamera>().Priority = 10;
		newVCam.GetComponent<CinemachineVirtualCamera>().Priority = 11;
		vCamManager.GetComponent<VCamManager>().activeCamera = newVCam;
		}
	}
}
