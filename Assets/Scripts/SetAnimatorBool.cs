using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class SetAnimatorBool : MonoBehaviour {
	private Animator anim;

	void Start()
	{
		anim = GetComponent<Animator>();
	}
	public void setBoolTrue(string name)
	{
		anim.SetBool(name, true);
	}
	public void setBoolFalse(string name)
	{
		anim.SetBool(name, false);
	}
}
