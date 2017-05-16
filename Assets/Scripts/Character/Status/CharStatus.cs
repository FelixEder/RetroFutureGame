using UnityEngine;
using System.Collections;

public class CharStatus : MonoBehaviour {
	public bool onGround, onLeftWall, onRightWall, isMirrored, isFloating, onSurface, invulnerable;
	public bool megaPunch, chargedMegaPunch, isSmall;
	public float velocityX, velocityY;

	public bool grounded, againstLeft, againstRight, onPlatform;
	public LayerMask whatIsGround, whatIsWall;
	public Transform downCheck, backCheck, frontCheck;

	void Update() {
		velocityX = GetComponent<Rigidbody2D> ().velocity.x;
		velocityY = GetComponent<Rigidbody2D>().velocity.y;
	}

	void FixedUpdate() {
		grounded = GetComponent<Rigidbody2D>().velocity.y < 1f ? Physics2D.OverlapBox (downCheck.position, new Vector2 (0.6f, 0.1f), 0, whatIsGround) : false;
		onPlatform = Physics2D.OverlapBox (downCheck.position, new Vector2 (0.59f, 0.1f), 0, LayerMask.NameToLayer("Platform"));
		againstLeft = isMirrored ? Physics2D.OverlapBox (frontCheck.position, new Vector2 (0.1f, 1.9f), 0, whatIsWall) : Physics2D.OverlapBox (backCheck.position, new Vector2 (0.1f, 1.9f), 0, whatIsWall);
		againstRight = isMirrored ? Physics2D.OverlapBox (backCheck.position, new Vector2 (0.1f, 1.9f), 0, whatIsWall) : Physics2D.OverlapBox (frontCheck.position, new Vector2 (0.1f, 1.9f), 0, whatIsWall);
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = new Color (1, 0, 0, 0.5f);
		Gizmos.DrawCube (downCheck.position, new Vector3 (0.55f, 0.1f, 1));
		Gizmos.DrawCube (backCheck.position, new Vector3 (0.1f, 1.9f, 1));
		Gizmos.DrawCube (frontCheck.position, new Vector3 (0.1f, 1.9f, 1));
	}

	/**
	 * Returns true if the player is in the air.
	 */
	public bool InAir() {
		return (!grounded && !againstLeft && !againstRight);
	}

	public bool IsMegaPunching() {
		return (megaPunch || chargedMegaPunch);
	}

	public void NoLongerMegaPunching() {
		megaPunch = false;
		chargedMegaPunch = false;
	}

	public void Invulnerable(float time) {
		invulnerable = true;
		Invoke ("SetVulnerable", time);
	}

	void SetVulnerable() {
		invulnerable = false;
	}

	public bool Invulnerable() {
		return invulnerable;
	}

	/*public void changeSize() {
		isSmall = !isSmall;
	}
	*/
}
