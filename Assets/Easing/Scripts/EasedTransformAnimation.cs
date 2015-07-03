using UnityEngine;
using Easing8000;
using System.Collections;
using System;

public class EasedTransformAnimation : MonoBehaviour {
	[Header("Use EITHER transforms OR Vector3")]
	[SerializeField]
	private Vector3[] vectorStops;
	[SerializeField]
	private Transform[] transformStops;
	private Vector3 currentTarget;
	private Vector3 lastTarget;
	[Range(0,20f)]
	[SerializeField]
	private float stoptime = 1f;
	[Range(0,10f)]
	[SerializeField]
	private float speed = 5f;
	[SerializeField]
	private EasingTypes easingFunction;
	[SerializeField]
	private TransformProperty propertyToAnimate;
	private Func<float, float, float, float> easeFunc;
	private bool animating = true;
	private int lastStop = 0;
	private float progress = 0f;

	enum TransformProperty
	{
		Position, Scale, Rotation
	}
	
	void Start () {
		if(vectorStops.Length == 0 && transformStops.Length == 0) 
		{
			Debug.Log("No Stops set. Deactivating Component on Gameobject " + gameObject.name);
			this.enabled = false;
			return;
		}else if(vectorStops.Length > 0 && transformStops.Length > 0)
		{
			Debug.Log("I specifically said not to put both Transform and Vectors on here. Which one should I use? This is so confusing and I'm just a machine.. " +
				"Deactivating Component on Gameobject " + gameObject.name);
			this.enabled = false;
			return;
		}
		easeFunc = Easing.Function(easingFunction);
		setTarget(0, true);
	}
	
	void Update () {
		if(animating)
		{
			progress += Time.deltaTime  * speed;
			switch (propertyToAnimate) {
			case TransformProperty.Position:
				transform.position = Vector3.zero.ease(easeFunc,lastTarget, currentTarget, progress);
				break;
			case TransformProperty.Scale:
				transform.localScale = Vector3.zero.ease(easeFunc, lastTarget, currentTarget, progress);
				break;
			case TransformProperty.Rotation:
				transform.eulerAngles = Vector3.zero.ease(easeFunc, lastTarget, currentTarget, progress);
				break;
			default:
				throw new ArgumentOutOfRangeException ();
			}
			if(progress >= 0.9999f) StartCoroutine(stop());
		}
		
	}

	private void setTarget(int index , bool init = false )
	{
		switch (propertyToAnimate) {
		case TransformProperty.Position:
			lastTarget = init ? transform.position : currentTarget;
			currentTarget = vectorStops.Length == 0 ? transformStops[index].position : vectorStops[index];
			break;
		case TransformProperty.Scale:
			lastTarget = init ? transform.localScale : currentTarget;
			currentTarget = vectorStops.Length == 0 ? transformStops[index].localScale : vectorStops[index];
			break;
		case TransformProperty.Rotation:
			lastTarget = init ? transform.eulerAngles : currentTarget;
			currentTarget = vectorStops.Length == 0 ? transformStops[index].eulerAngles : vectorStops[index];
			break;
		default:
			throw new ArgumentOutOfRangeException ();
		}
	}
	private void nextStop()
	{
		if(lastStop == Mathf.Max(vectorStops.Length, transformStops.Length)) lastStop = 0;
		setTarget(lastStop);
		lastStop++;
	}
	
	private IEnumerator stop()
	{
		animating = false;
		yield return new WaitForSeconds(stoptime);
		progress = 0f;
		animating  = true;
		nextStop();
	}

	public EasingTypes EasingFunction {
		get {
			return easingFunction;
		}
		set {
			easingFunction = value;
			easeFunc = Easing.Function(easingFunction);
		}
	}	
	
	
}
