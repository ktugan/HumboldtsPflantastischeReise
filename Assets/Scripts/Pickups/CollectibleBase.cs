using UnityEngine;
using System.Collections;

public abstract class CollectibleBase : MonoBehaviour {
	[SerializeField]
	protected bool oneoff = true;
	private bool used = false;
	[TagSelectorAttribute]
	[SerializeField]
	protected string tagToCheck;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(!used && other.CompareTag(tagToCheck))
		{
			triggerEffect();
			if(oneoff) used = true;
		}
	}
	protected virtual void triggerEffect()
	{
	}
}
