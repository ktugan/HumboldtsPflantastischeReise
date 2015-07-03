using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

[RequireComponent(typeof(Collider2D))]
public class LethalObstacle : MonoBehaviour {

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			coll.gameObject.GetComponent<PlatformerCharacter2D>().Dead = true;
		}
	}
}
