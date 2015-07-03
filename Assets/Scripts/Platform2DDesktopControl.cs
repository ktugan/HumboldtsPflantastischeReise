using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

[RequireComponent(typeof(Platform2DMobileControl))]
[RequireComponent(typeof (PlatformerCharacter2D))]
public class Platform2DDesktopControl : MonoBehaviour {
	#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
	[SerializeField]
	private KeyCode shakeKey;
	[SerializeField]
	private KeyCode sprintKey = KeyCode.Space;
	[SerializeField]
	private KeyCode goLeft = KeyCode.LeftArrow;
	[SerializeField]
	private KeyCode goRight = KeyCode.RightArrow;
	private PlatformerCharacter2D character;
	private float m;
	private bool sprint = false;


	void Awake()
	{
		character = GetComponent<PlatformerCharacter2D>();
		//shut mobile Input down;
		GetComponent<Platform2DMobileControl>().enabled = false;
	}

	void FixedUpdate () {
		if(Input.GetKey(goLeft)) m = -.5f;
		else if(Input.GetKey(goRight)) m = .5f;
		else m = 0;
		if(sprint) m *= 2f;
		character.Move(m, false, false);

		if(Input.GetKey(shakeKey)) Shake.callShake();
	}

	//workaround to detect Unity Remote

	void Update()
	{
		#if UNITY_EDITOR
		if(Input.touches.Length > 0)
		{
			GetComponent<Platform2DMobileControl>().enabled = true;
			this.enabled = false;
		}
		#endif
		if(Input.GetKeyUp(sprintKey)) sprint = !sprint;
	}
	public void Stop()
	{
		character.Move(0f, false, false);
	}
	#endif
}
