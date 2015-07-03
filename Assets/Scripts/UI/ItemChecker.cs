using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ItemChecker : MonoBehaviour {
	private bool currentlyAllowed = false;
	[SerializeField]
	private string itemname;
	[SerializeField]
	private bool oneoff;
	[SerializeField]
	private UnityEvent onItemPresent;
	private Inventar inventar;
	private bool itemArrived = false;



	void OnEnable()
	{
		Inventar.itemArrived += checkArrival;
	}
	void OnDisable()
	{
		Inventar.itemArrived -= checkArrival;
	}
	void Start()
	{
		inventar = GameObject.FindGameObjectWithTag(Tags.Inventar).GetComponent<Inventar>();
	}
	private void checkArrival(string arrivalname)
	{
		if(arrivalname.ToUpper().Contains(itemname.ToUpper())) itemArrived = true;
	}
	public void CallIfItemPresent()
	{
		if(itemArrived) onItemPresent.Invoke();
		if(oneoff) itemArrived = false;
	}
	public void AttachActionIfItemPresent()
	{
		if(!itemArrived) return;
		inventar.findByName(itemname).addAction(()=>
		{
		     if(currentlyAllowed) onItemPresent.Invoke();
			 if(oneoff) currentlyAllowed = false;
		});
	}
	public void flashItem(bool On)
	{
		if(itemArrived) inventar.flashByName(itemname, On);
	}

	public bool CurrentlyAllowed {
		get {
			return currentlyAllowed;
		}
		set {
			currentlyAllowed = value;
		}
	}
}
