using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class NPCMovement : MonoBehaviour {
	[SerializeField]
	private bool moveOnStartUp;
	[SerializeField]
	private Transform[] targets;
	[SerializeField]
	private float moveSpeed = .2f;
	[SerializeField]
	private UnityEvent onReachedTarget;
	[SerializeField]
	private string moveBoolAlias;
	private Vector3 lastTarget, currentTarget;
	private WaitForEndOfFrame waitForFrameEnd = new WaitForEndOfFrame();
	private float progress;
	private int index = 0;
	private Animator anim;
	private bool patroling = false;


	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	void Start () {
		if(moveOnStartUp) patrol();
	}
	public void patrol(int startindex = 0)
	{
		if(patroling)
		{
			Stop();
			patroling = false;
			patrol();
			return;
		}
		index = startindex;
		StartCoroutine(moveEnumerator(targets[index].position, () =>{
			setNextTarget();
		}));
		patroling = true;

	}
	private void setNextTarget()
	{
		index = (index < targets.Length- 1) ? (index +1) : 0;
		StartCoroutine(moveEnumerator(targets[index].position, () =>{
			setNextTarget();
		}));
	}

	public void MoveTo (Transform target)
	{
		MoveTo (target, ()=>{});
	}	

	public void MoveTo(Transform target, Action OnComplete)
	{
		StartCoroutine(moveEnumerator(target.position, OnComplete));
	}
	public void MoveToAndCallEvent(Transform target)
	{
		MoveTo (target, ()=>{onReachedTarget.Invoke();});
	}
	private void switchDirection()
	{
		float modifier = (lastTarget.x < currentTarget.x) ? 1f : -1f;
		transform.localScale = new Vector3(modifier * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
	}
	private IEnumerator moveEnumerator(Vector3 target, Action OnComplete)
	{
		progress = 0f;
		lastTarget = transform.position;
		currentTarget = target;
		switchDirection();
		anim.SetBool(moveBoolAlias, true);
		while(progress < 0.99f)
		{
			progress += Time.unscaledDeltaTime  * moveSpeed;
			transform.position = Vector3.Lerp(lastTarget, currentTarget, progress);
			yield return waitForFrameEnd;
		}
		anim.SetBool(moveBoolAlias, false);
		OnComplete();
	}
	public void Stop()
	{
		StopAllCoroutines();
		anim.SetBool(moveBoolAlias, false);
	}
}
