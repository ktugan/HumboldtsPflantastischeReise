using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class SimpleWalker : MonoBehaviour {
	[SerializeField]
	private float speed;
	[SerializeField]
	private bool onStartup;
	[SerializeField]
	private string animatorRunAlias;
	private bool animating = false;

	void Start () {
		if(onStartup) startAnimating(Mathf.Sign(transform.localScale.x));
	}

	void Update () {
		if(animating)
		{
			transform.Translate(transform.right * speed * Time.deltaTime);
		}
	}
	public void startAnimating(float startdir)
	{
		transform.localScale = new Vector3(transform.localScale.x * startdir, transform.localScale.y, transform.localScale.z);
		speed = startdir * Mathf.Abs(speed);
		GetComponent<Animator>().SetBool(animatorRunAlias, true);
		Animating = true;
	}

	public bool Animating {
		get {
			return animating;
		}
		set {
			animating = value;
		}
	}
	public void reverse()
	{
		float sign = -Mathf.Sign(transform.localScale.x);
		transform.localScale = new Vector3(sign * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		speed = sign * Mathf.Abs(speed);
	}
}
