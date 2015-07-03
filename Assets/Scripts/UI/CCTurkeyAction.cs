using UnityEngine;
using System.Collections;

public class CCTurkeyAction : MonoBehaviour, ICustomItemAction {
	[SerializeField]
	private GameObject walkingTurkey;
	private Transform turkeyStartTransform;


	void Start()
	{
		turkeyStartTransform = GameObject.FindGameObjectWithTag(Tags.Player).transform;
	}
	

	#region ICustomItemAction implementation
	public void doCustomAction ()
	{
		GameObject instance = Instantiate(walkingTurkey, turkeyStartTransform.position, Quaternion.identity) as GameObject;
		instance.GetComponent<SimpleWalker>().startAnimating(Mathf.Sign(turkeyStartTransform.localScale.x));

	}
	#endregion
}
