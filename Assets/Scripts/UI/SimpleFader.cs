using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Image))]
public class SimpleFader : UnitySingleton<SimpleFader> {
	[SerializeField]
	private bool FadeInOnStartup = true;
	[SerializeField]
	private float FadeSpeed = 1f;
	private CanvasGroup cgroup;
	private WaitForEndOfFrame wait = new WaitForEndOfFrame();
	private float progress, lastState;


	void Start () {
		cgroup = GetComponent<CanvasGroup>();
		cgroup.alpha = FadeInOnStartup ? 1f : 0f;
		if(FadeInOnStartup) fade(true, ()=>{});
	}
	
	public void fade(bool fadeIn, Action OnComplete)
	{
		float target = fadeIn ? 0f : 1f;
		StartCoroutine(fadeEnumerator(target, OnComplete));

	}
	public void fadeOutAndLoadLevel(int levelindex)
	{
		fade(false, ()=>{
			Application.LoadLevel(levelindex);
		});
	}
	private IEnumerator fadeEnumerator(float target, Action OnComplete)
	{
		Time.timeScale = 1f;
		lastState = cgroup.alpha;
		progress = 0f;
		while(progress < 0.999f)
		{
			progress += Time.unscaledDeltaTime  * FadeSpeed;
			cgroup.alpha = Mathf.Lerp(lastState, target, progress);
			yield return wait;
		}
		OnComplete();
	}
}
