using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class NPCConstantMovement : MonoBehaviour {
	public float speed;
	[SerializeField]
	private string walkAlias;
	private bool animating = false;
	private Animator anim;


	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	void Update () {
		if(animating)
		{
			transform.Translate(transform.right * speed * Time.deltaTime);
		}
	}


	public bool Animating {
		get {
			return animating;
		}
		set {
			animating = value;
			anim.SetBool(walkAlias, animating);

		}
	}
}
