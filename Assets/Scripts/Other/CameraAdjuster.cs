using UnityEngine;
using System.Collections;

public class CameraAdjuster : MonoBehaviour {
	public float adjustX, adjustY, setSize;

	public float GetX() {
		return adjustX;
	}

	public float GetY() {
		return adjustY;
	}

	public float GetSize() {
		return setSize;
	}
}
