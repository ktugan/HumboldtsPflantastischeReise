using UnityEngine;
using UnityEngine.Events;
using System;
using System.Reflection;
using System.Collections;

public class TriggerEvent2D : MonoBehaviour {
[TagSelectorAttribute(false)]
[SerializeField]
private string[] tagsToCheck;
private bool notags = false;
public UnityEvent onEnterEvent, onExitEvent, onStayEvent;
private Action onEnter, onExit, onStay;

	void Start()
	{
		//have we got tags to check
		notags = tagsToCheck.Length == 0 ? true : false;
		//chache invoke calls at startup
		onEnter = (Action)Delegate.CreateDelegate(typeof(Action), onEnterEvent, "Invoke");
		onExit = (Action)Delegate.CreateDelegate(typeof(Action), onExitEvent, "Invoke");
		onStay = (Action)Delegate.CreateDelegate(typeof(Action), onStayEvent, "Invoke");
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		callMethods(other, onEnter);
	}
	void OnTriggerExit2D(Collider2D other)
	{
		callMethods(other, onExit);
	}
	void OnTriggerStay2D(Collider2D other)
	{
		callMethods(other, onStay);
	}
	private void callMethods(Collider2D other, Action a)
	{
		if(notags)
		{
			a();
			return;
		}
		//check for all given tags wheter they are on the collider. break on finding the first one, since an object can have just one tag
		for(int i = 0; i < tagsToCheck.Length;i++)
		{
			if(other.CompareTag(tagsToCheck[i])) 
			{
				a();
				break;
			}
		}
	}
}
