using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlaySound : CollectibleBase {
	private AudioSource source;
	void Start()
	{
		source = GetComponent<AudioSource>();
	}
	protected override void triggerEffect()
	{
		source.Play();
	}
}
