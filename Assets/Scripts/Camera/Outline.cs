using UnityEngine;
using System;
using System.Collections;
using Easing8000;


[RequireComponent(typeof(SpriteRenderer))]
public class Outline : MonoBehaviour {
	[SerializeField]
	protected SpriteRenderer outlineSprite;
	[SerializeField]
	protected float fadeSpeed;
	[SerializeField]
	protected EasingTypes easingfunction;
	[SerializeField]
	protected bool blink;
	private Func<float, float, float, float> easeFunc;
	private bool fadedIn = false;
	private float alphatarget = 1f;
	private float alphasource = 0f;
	private float currentAlpha;
	private float progress;


	void Start () {
		outlineSprite.sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;
		easeFunc = Easing.Function(easingfunction);
	}

	IEnumerator easedFade()
	{
		progress += Time.deltaTime  * fadeSpeed;
		currentAlpha = easeFunc(alphasource, alphatarget, progress);
		outlineSprite.color = new Color(currentAlpha, currentAlpha, currentAlpha, 1f);
		yield return new WaitForEndOfFrame();
		if(progress >= 0.99f) reverseTarget();
		else StartCoroutine(easedFade());
	}

	private void reverseTarget()
	{
		if(!fadedIn || !blink) return;
		progress = 0f;
		float oldTarget = alphatarget;
		alphatarget = alphasource;
		alphasource = oldTarget;
		StartCoroutine(easedFade());
	}

	public bool FadedIn {
		get {
			return fadedIn;
		}
		set {
			if(fadedIn != value)
			{
			fadedIn = value;
			StopCoroutine(easedFade());
			progress = 0f;
			alphatarget = fadedIn ? 1f : 0;
			alphasource = outlineSprite.color.r;
			StartCoroutine(easedFade());
			}
		}
	}
}
