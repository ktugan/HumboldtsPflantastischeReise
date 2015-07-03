using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class MarioCam : MonoBehaviour {
	[SerializeField]
	private Transform target;
	[SerializeField]
	private float damping = 1f;
	[SerializeField]
	private float leftOffsetBeforeMove = 0.3f;
	[SerializeField]
	private float rightOffsetBeforeMove = 0.7f;
	private Vector3 currentVelocity;
	private Vector2 screenPos;
	private Camera cam;
	private Vector3 lastTargetPosition;

	void Start () {
		cam = GetComponent<Camera>();
		lastTargetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
	}
	

	void Update () {
		screenPos = cam.WorldToViewportPoint(target.position);
		if(screenPos.x < leftOffsetBeforeMove || screenPos.x > rightOffsetBeforeMove)
		{
			lastTargetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
		}
		Vector3 newPos = Vector3.SmoothDamp(transform.position, lastTargetPosition, ref currentVelocity, damping);
		
		transform.position = newPos;
	}
}
