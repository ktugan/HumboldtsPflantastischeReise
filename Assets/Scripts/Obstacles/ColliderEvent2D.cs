using UnityEngine;
using UnityEngine.Events;
using System;
using System.Reflection;
using System.Collections;

public class ColliderEvent2D : MonoBehaviour {
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
	void OnCollisionEnter2D(Collision2D coll)
	{
		callMethods(coll.collider, onEnter);
	}
	void OnCollisionExit2D(Collision2D coll)
	{
		callMethods(coll.collider, onExit);
	}
	void OnCollisionStay2D(Collision2D coll)
	{
		callMethods(coll.collider, onStay);
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
