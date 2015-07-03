using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Echo : MonoBehaviour {
	[SerializeField]
	private bool playOnReceiveNewEcho;
	private AudioClip lastEcho;
	private AudioSource source;

	
	void Start () {
		source = GetComponent<AudioSource>();
	}
	void OnEnable()
	{
		EchoSource.newEcho += HandlenewEcho;
	}
	
	void OnDisable()
	{
		EchoSource.newEcho -= HandlenewEcho;
	}

	private void HandlenewEcho (AudioClip clip)
	{
		lastEcho = clip;
		source.clip = lastEcho;
		if(playOnReceiveNewEcho) playEcho();
	}
	public void playEcho()
	{
		if(lastEcho != null) source.Play();
	}

}
