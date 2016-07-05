using UnityEngine;
using System.Collections;

public class CharStatus : MonoBehaviour {
	public bool onGround, onLeftWall, onRightWall, isMirrored, spaceDown;
	public float velocityX, velocityY;
	void Update() {
		velocityX = GetComponent<Rigidbody2D> ().velocity.x;
		velocityY = GetComponent<Rigidbody2D>().velocity.y;
	}
}
