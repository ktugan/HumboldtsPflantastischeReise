using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	[SerializeField]
	private string boolAlias;
	[SerializeField]
	private Animator menuAnimator;
	[SerializeField]
	private AudioMixerSnapshot normalMusic;
	[SerializeField]
	private AudioMixerSnapshot pausedMusic;
	private bool paused;

	void Start()
	{
		if(!menuAnimator) menuAnimator = GetComponent<Animator>();
	}

	public void TogglePauseState()
	{
		paused = !paused;

		if(paused)
		{
			pausedMusic.TransitionTo(0.4f);
			StartCoroutine(delayedPause());
		}else
		{
			Time.timeScale = paused ? 0f : 1f;
			normalMusic.TransitionTo(0.4f);
		}
		menuAnimator.SetBool(boolAlias, paused);

		
	}
	private IEnumerator delayedPause(float delay = 0.5f)
	{

		yield return new WaitForSeconds(delay);
		Time.timeScale = paused ? 0f : 1f;
	}
}
