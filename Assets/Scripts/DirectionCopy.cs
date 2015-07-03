using UnityEngine;
using System.Collections;

public class DirectionCopy : MonoBehaviour {
	[SerializeField]
	private Transform source;

	

	void Update () {
		transform.localScale = new Vector3(Mathf.Sign(source.localScale.x)* transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}
}
