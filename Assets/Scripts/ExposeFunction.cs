using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ExposeFunction : MonoBehaviour {
	public UnityEvent functionToCall;

	public void CallFunction()
	{
		functionToCall.Invoke();
	}
}
