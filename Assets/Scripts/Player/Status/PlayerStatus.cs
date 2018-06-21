using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {
	public bool isMirrored, isFloating, invulnerable, isSmall;
	public float velocityX, velocityY;

	public bool grounded, againstLeft, againstRight, onPlatform, againstFront, againstStep, inWater;
	public LayerMask whatIsGround, whatIsPlatform, whatIsWall, whatIsWater;
	public Transform downCheck, backCheck, frontCheck, smallFront, smallBack;

	void Update() {
		velocityX = GetComponent<Rigidbody2D>().velocity.x;
		velocityY = GetComponent<Rigidbody2D>().velocity.y;

		grounded = GetComponent<Rigidbody2D>().velocity.y < 1f || grounded ? Physics2D.OverlapBox(downCheck.position, new Vector2(0.55f, 0.1f), 0, whatIsGround) : false;
		onPlatform = Physics2D.OverlapBox(downCheck.position, new Vector2(0.6f, 0.1f), 0, whatIsPlatform);
		
		againstStep = Physics2D.OverlapBox(frontCheck.position + new Vector3(0, -0.95f, 0), new Vector2(0.1f, 0.2f), 0, whatIsWall);

		inWater = Physics2D.OverlapBox(transform.position, new Vector2(0.6f, 0.1f), 0, whatIsWater) && Physics2D.OverlapBox(downCheck.position, new Vector2(0.6f, 0.1f), 0, whatIsWater);

		if (isSmall) {
			againstLeft = isMirrored ? Physics2D.OverlapBox(smallFront.position, new Vector2(0.1f, 0.5f), 0, whatIsWall) : Physics2D.OverlapBox(smallBack.position, new Vector2(0.1f, 0.7f), 0, whatIsWall);
			againstRight = isMirrored ? Physics2D.OverlapBox(smallBack.position, new Vector2(0.1f, 0.7f), 0, whatIsWall) : Physics2D.OverlapBox(smallFront.position, new Vector2(0.1f, 0.5f), 0, whatIsWall);
			againstFront = Physics2D.OverlapBox(smallFront.position, new Vector2(0.1f, 0.5f), 0, whatIsWall);
		} else {
			againstLeft = isMirrored ? Physics2D.OverlapBox(frontCheck.position, new Vector2(0.1f, 1.7f), 0, whatIsWall) : Physics2D.OverlapBox(backCheck.position, new Vector2(0.1f, 1.9f), 0, whatIsWall);
			againstRight = isMirrored ? Physics2D.OverlapBox(backCheck.position, new Vector2(0.1f, 1.9f), 0, whatIsWall) : Physics2D.OverlapBox(frontCheck.position, new Vector2(0.1f, 1.7f), 0, whatIsWall);
			againstFront = Physics2D.OverlapBox(frontCheck.position, new Vector2(0.1f, 1.7f), 0, whatIsWall);
		}
		//NOTE: if first statement (before ?) is true, var equals first value (after ?), else it equals second value (after :).
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = new Color(1, 1, 0, 0.5f); //yellow
		Gizmos.DrawCube(downCheck.position, new Vector3(0.55f, 0.1f, 1)); //downcheck
		Gizmos.DrawCube(backCheck.position, new Vector3(0.1f, 1.9f, 1)); //backcheck
		Gizmos.DrawCube(frontCheck.position, new Vector3(0.1f, 1.7f, 1)); //frontcheck
		Gizmos.color = new Color(0, 1f, 0, 0.5f); //green
		Gizmos.DrawCube(frontCheck.position + new Vector3(0, -0.95f, 0), new Vector3(0.1f, 0.2f, 1)); //stepcheck
	}

	/**
	 * Returns true if the player is in the air.
	 */
	public bool InAir() {
		return (!grounded && !againstLeft && !againstRight);
	}

	public void Invulnerable(float duration) {
		invulnerable = true;
		Invoke("SetVulnerable", duration);
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
