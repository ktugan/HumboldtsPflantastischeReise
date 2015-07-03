using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NPCConstantMovement))]
public class NPCFollow : MonoBehaviour {
	[SerializeField]
	private Transform followTarget;
	[SerializeField]
	private float distanceThreshold = 1f;
	[SerializeField]
	private float waitTime = 0.5f;
	private NPCConstantMovement moveScript;
	[SerializeField]
	private bool direction;
	private float speed;


	void Start () {
		moveScript = GetComponent<NPCConstantMovement>();
		follow();
	}
	
	public void follow()
	{
		//check Direction
		float sign = -Mathf.Sign(transform.position.x - followTarget.position.x);
		transform.localScale = new Vector3(sign * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		moveScript.speed = sign * Mathf.Abs(moveScript.speed);

		if(Mathf.Abs(Vector3.Distance(followTarget.position, transform.position)) > distanceThreshold)
		{
			moveScript.Animating = true;
		}else{
			moveScript.Animating = false;
		}
		StartCoroutine(waitAndCheckAgain());
	}

	private IEnumerator waitAndCheckAgain()
	{
		yield return new WaitForSeconds(waitTime);
		follow();
	}

	public Transform FollowTarget {
		get {
			return followTarget;
		}
		set {
			followTarget = value;
		}
	}
}
