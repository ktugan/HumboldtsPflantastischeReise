using UnityEngine;
using System.Collections;

public class Shake2Rotate : MonoBehaviour {
	[Header("Time before Scene can be shaken again")]
	[SerializeField]
	[Range(0,5f)]
	private float cool_down_time = 1.5f;
	[SerializeField]
	private string AnimatorBool ="Turned";
	[SerializeField]
	private Animator anim;
	private bool turned = false;
	private bool cooled = true;

	//subscribe /unsubscribe to shake events
	void OnEnable()
	{
		Shake.ShakenX += triggerSwitch;
		Shake.ShakenY += triggerSwitch;
		Shake.ShakenZ += triggerSwitch;
	}
	void OnDisable()
	{
		Shake.ShakenX -= triggerSwitch;
		Shake.ShakenY -= triggerSwitch;
		Shake.ShakenZ -= triggerSwitch;
	}
	void Awake()
	{
	
	}
	// derive and override this for aditional functionality
	protected virtual void triggerSwitch()
	{
		rotateAroundPivot();
	}
	protected void rotateAroundPivot()
	{
		if(!cooled) return;
		turned = !anim.GetBool(AnimatorBool);
		anim.SetBool(AnimatorBool, turned);
		StartCoroutine(cool());

	}
	protected IEnumerator cool()
	{
		cooled = false;
		yield return new WaitForSeconds(cool_down_time);
		cooled = true;
	}

}
