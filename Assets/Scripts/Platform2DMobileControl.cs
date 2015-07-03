using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

[RequireComponent(typeof (SpriteRenderer))]
[RequireComponent(typeof (PlatformerCharacter2D))]
public class Platform2DMobileControl : MonoBehaviour {
	private float move = 0f;
	private bool jump = false;
	private PlatformerCharacter2D character;
	private SpriteRenderer rndr;
	private Vector2 char_screen_pos = Vector2.zero;
	private float touchX;
	private Camera cam;
	private bool sprint = false;
	
	void Awake()
	{
		character = GetComponent<PlatformerCharacter2D>();
		cam = Camera.main;
		rndr = GetComponent<SpriteRenderer>();
	}
	void OnEnable()
	{
		TouchInputManager.UpSwipe += HandleUpSwipe;
		TouchInputManager.DownSwipe += HandleDownSwipe;;
	}
	void OnDisable()
	{
		TouchInputManager.UpSwipe -= HandleUpSwipe;
		TouchInputManager.DownSwipe -= HandleDownSwipe;;
	}

	void HandleDownSwipe ()
	{
		sprint = false;
	}

	void HandleUpSwipe ()
	{
		sprint = true;
	}

	void FixedUpdate () {
		move = calculateDirection();
		if(sprint) move *= 2f; 
		character.Move(move,false,false);
	}

	private float calculateDirection()
	{
		//early out with no touch detected
		if(Input.touches.Length < 1) return 0f;
		char_screen_pos.x = cam.WorldToScreenPoint(rndr.bounds.min).x;
		char_screen_pos.y = cam.WorldToScreenPoint(rndr.bounds.max).x;

		touchX = Input.GetTouch(0).position.x;
		//moving where the finger is relative to the character
		//relative Speed deprecated
//		if(touchX > char_screen_pos.y) return Mathf.Max(0.2f, ((touchX.Difference(touchX, char_screen_pos.y))/Screen.width)*3f );
//		else if(touchX < char_screen_pos.x) return Mathf.Min(-0.2f,-((touchX.Difference(touchX, char_screen_pos.x))/Screen.width)*3f);
		//absolute speed (to avoid Camera problems)
		if(touchX > char_screen_pos.y) return .5f;
		else if(touchX < char_screen_pos.x) return -.5f;
		else return 0f;
	}

    public void Stop()
    {
        character.Move(0f, false, false);
    }
	public float Move {
		get {
			return move;
		}
		set {
			move = value;
		}
	}

	public bool Jump {
		get {
			return jump;
		}
		set {
			jump = value;
		}
	}
}
