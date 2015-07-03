using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class OutlineOnEnter : Outline {

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag(Tags.Player))
		{
			FadedIn = true;
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.CompareTag(Tags.Player))
		{
			FadedIn = false;
		}
	}

}
