using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class DelayedCall : MonoBehaviour {
	[Range(0.01f, 5f)]
	[SerializeField]
	private float delay = 1f;
	[SerializeField]
	private UnityEvent callAfterDelay;
	[SerializeField]
	private bool oneoff;
	private bool blocked;

	public void delayAndCall()
	{
		if(blocked) return;
		StartCoroutine(delayedAction());
		if(oneoff) blocked = true;
	}

	private IEnumerator delayedAction()
	{
		yield return new WaitForSeconds(delay);
		callAfterDelay.Invoke();
	}
}
