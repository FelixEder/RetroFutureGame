using UnityEngine;
using System.Collections;

public class FinalBossJump : MonoBehaviour {

	void OnTriggerStay2D() {
		transform.parent.GetComponent<Phase3Head>().Jump();
	}
}