using Cinemachine;
using UnityEngine;

public class VCamActivator : MonoBehaviour {
	public GameObject mainCamera, vCamManager, newVCam;
	public float blendTime = 4;

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name == "Player") {
			mainCamera.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = blendTime;
			vCamManager.GetComponent<VCamManager>().activeCamera.GetComponent<CinemachineVirtualCamera>().Priority = 10;
			newVCam.GetComponent<CinemachineVirtualCamera>().Priority = 11;
			vCamManager.GetComponent<VCamManager>().activeCamera = newVCam;
		}
	}
}
