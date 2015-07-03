using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class ForegroundSwitch : Shake2Rotate {
	private bool playerPresent = false;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag(Tags.Player)) playerPresent = true;
	}
	void OnTriggerExit2D(Collider2D other) {
		if(other.CompareTag(Tags.Player)) playerPresent = false;
	}
	//check if Player nearby and if so call the inherited rotation Method
	protected override void triggerSwitch()
	{
		if(!playerPresent) return;
		rotateAroundPivot();
	}
}
