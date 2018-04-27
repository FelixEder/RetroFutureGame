using Cinemachine;
using UnityEngine;

public class VCamActivator : MonoBehaviour {
	public GameObject vCamManager, newVCam;

	void OnTriggerEnter2D(Collider2D col) {
		newVCam.GetComponent<CinemachineVirtualCamera>().Priority += 1;
		vCamManager.GetComponent<VCamManager>().activeCamera.GetComponent<CinemachineVirtualCamera>().Priority -= 1;
		vCamManager.GetComponent<VCamManager>().activeCamera = newVCam;
	}
}
