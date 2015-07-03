using UnityEngine;
using System.Collections;

public class TruthahnReverser : MonoBehaviour {
	private bool jaguarPresent;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag(Tags.Truthahn) && jaguarPresent)
		{
			other.gameObject.GetComponent<SimpleWalker>().reverse();
		}
	}

	public bool JaguarPresent {
		get {
			return jaguarPresent;
		}
		set {
			jaguarPresent = value;
		}
	}
}
