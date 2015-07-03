using UnityEngine;
using System.Collections;

public class BlinkOnEnter : Outline {

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
