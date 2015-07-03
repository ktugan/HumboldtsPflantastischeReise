using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SwitchClips : MonoBehaviour {
	private AudioSource source;
	[SerializeField]
	private float fadetime;


	void Start () {
		source = GetComponent<AudioSource>();
		source.loop = true;
	}
	public IEnumerator fade(float start, float target, float time)
	{
		float timeoriginal = time;
		while(time > 0)
		{
			time -= Time.deltaTime;
			source.volume = Mathf.Lerp(start, target, 1f -time/timeoriginal);
			yield return new WaitForEndOfFrame();
		}
	}
	public void switchClip(AudioClip clip)
	{
		StartCoroutine(switchClips(clip));
	}
	public IEnumerator switchClips(AudioClip clip)
	{
		yield return StartCoroutine(fade(1f, 0f, fadetime));
		source.Stop();
		source.clip = clip;
		source.Play();
		StartCoroutine(fade(0f, 1f, fadetime));
	}


}
