using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class SimpleCCD : MonoBehaviour
{
	public int iterations = 5;
	
	[Range(0.01f, 1)]
	public float damping = 1;

	public Transform target;
	public Transform endTransform;
	
	public Node[] angleLimits = new Node[0];

	Dictionary<Transform, Node> nodeCache; 
	[System.Serializable]
	public class Node
	{
		public Transform Transform;
		public float min;
		public float max;
	}

	void OnValidate()
	{
		// min & max has to be between 0 ... 360
		foreach (var node in angleLimits)
		{
			node.min = Mathf.Clamp (node.min, 0, 360);
			node.max = Mathf.Clamp (node.max, 0, 360);
		}
	}

	void Start()
	{
		// Cache optimization
		nodeCache = new Dictionary<Transform, Node>(angleLimits.Length);
		foreach (var node in angleLimits)
			if (!nodeCache.ContainsKey(node.Transform))
				nodeCache.Add(node.Transform, node);
	}

	void LateUpdate()
	{
		if (!Application.isPlaying)
			Start();

		if (target == null || endTransform == null)
			return;

		int i = 0;

		while (i < iterations)
		{
			CalculateIK ();
			i++;
		}

		endTransform.rotation = target.rotation;
	}

	void CalculateIK()
	{		
		Transform node = endTransform.parent;

		while (true)
		{
			RotateTowardsTarget (node);

			if (node == transform)
				break;

			node = node.parent;
		}
	}

	void RotateTowardsTarget(Transform transform)
	{		
		Vector2 toTarget = target.position - transform.position;
		Vector2 toEnd = endTransform.position - transform.position;

		// Calculate how much we should rotate to get to the target
		float angle = SignedAngle(toEnd, toTarget);

		// Flip sign if character is turned around
		angle *= Mathf.Sign(transform.root.localScale.x);

		// "Slows" down the IK solving
		angle *= damping; 

		// Wanted angle for rotation
		angle = -(angle - transform.eulerAngles.z);

		// Take care of angle limits 
		if (nodeCache.ContainsKey(transform))
		{
			// Clamp angle in local space
			var node = nodeCache[transform];
			float parentRotation = transform.parent ? transform.parent.eulerAngles.z : 0;
			angle -= parentRotation;
			angle = ClampAngle(angle, node.min, node.max);
			angle += parentRotation;
		}

		transform.rotation = Quaternion.Euler(0, 0, angle);
	}

	public static float SignedAngle (Vector3 a, Vector3 b)
	{
		float angle = Vector3.Angle (a, b);
		float sign = Mathf.Sign (Vector3.Dot (Vector3.back, Vector3.Cross (a, b)));

		return angle * sign;
	}

	float ClampAngle (float angle, float min, float max)
	{
		if (GameObject.Find ("Char").GetComponent<CharStatus> ().isMirrored) {
			min += 180;
			max += 180;
			if (min > 360)
				min -= 360;
			if (max > 360)
				max -= 360;
		}
		angle = Mathf.Abs((angle % 360) + 360) % 360;
		return Mathf.Clamp(angle, min, max);
	}
}
