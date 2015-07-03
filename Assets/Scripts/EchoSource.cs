using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class EchoSource : MonoBehaviour {
	private AudioSource source;
	public delegate void changeEcho(AudioClip clip);
	public static event changeEcho newEcho;

	void Start () {
		source = GetComponent<AudioSource>();
		source.playOnAwake = false;
	}

	public void PlayClipAndChangeEcho()
	{
		source.Play();
		if(newEcho != null) newEcho(source.clip);
	}
	

}
