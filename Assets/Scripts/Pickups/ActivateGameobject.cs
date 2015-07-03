using UnityEngine;
using System.Collections;

public class ActivateGameobject : CollectibleBase {
	[SerializeField]
	private bool turnoffSelf = false;
	[SerializeField]
	private GameObject go;
	private ParticleSystem[] particles = new ParticleSystem[0];

	void Awake()
	{
		particles = go.GetComponentsInChildren<ParticleSystem>();
	}

	protected override void triggerEffect()
	{
		go.SetActive(!go.activeSelf);
		foreach(ParticleSystem ps in particles)
		{
			ps.enableEmission = !ps.enableEmission;
		}
		if(turnoffSelf) gameObject.SetActive(false);
	}



}
