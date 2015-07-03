using UnityEngine;
using System.Collections;

public class TouchInputManager : UnitySingleton<TouchInputManager>{

	/*
	 * A central class for recognising touch gestures
	 * and emmitting them as events that
	 * other classes can then register to (Observer Pattern)
	 */
	public delegate void noParams();
	public static event noParams UpSwipe;
	public static event noParams DownSwipe;
	public static event noParams LeftSwipe;
	public static event noParams RightSwipe;
	public static event noParams SingleTap;
	public static event noParams DoubleTap;
	public delegate void Rotation(bool clockwise, float angle);
	public static event Rotation Rotate;
	public delegate void Zooming(float strength);
	public static event Zooming Zoom;

	[Header("Minimum Touch Movement in Screen Space percentage")]
	[Range(0,1f)]
	public float threshold = 0.1f;
	[Header("No Zoom below this Threshold")]
	[Range(0, 50f)]
	public float zoomThreshold = 1f;

	private Touch t, tOne, tTwo;
	private Vector2 startTouch;
	private Vector2 lastTouch;
	private Vector2 touchThreshold;
	private Vector2 currentDistance;
	private Vector2 previousDistance;
	private Vector3 crossVector;
	private float angleOffset;
	private float touchDelta;
	private bool clockwise;
	

	void Awake () {
		// Set touch threshold according to screen size
		touchThreshold = new Vector2((float)Screen.width * threshold,(float)Screen.height * threshold );
	}
	

	void Update () {

		switch(Input.touchCount)
		{	
		case 1:
			t = Input.GetTouch(0);
			switch(t.phase){
			case TouchPhase.Began:
				//get the start position of the Touch
				startTouch = t.position;
				break;

			case TouchPhase.Moved:

			
				break;

			case TouchPhase.Ended:
				// stationary Touch (below threshold)
				if(Mathf.Abs(startTouch.y - t.position.y) < touchThreshold.y && Mathf.Abs(startTouch.x - t.position.x) < touchThreshold.x)
				{
					if(t.tapCount ==1 && SingleTap != null) SingleTap();
					if(t.tapCount ==2 && DoubleTap != null) DoubleTap();

				}else
				{
					// find out type of gesture (X-Axis or Y-Axis)
					if(Mathf.Abs(startTouch.y - t.position.y) > Mathf.Abs(startTouch.x - t.position.x)){
						if(t.position.y > startTouch.y){
							if(UpSwipe != null) UpSwipe();
						}else{
							if(DownSwipe != null) DownSwipe();
						}
					}else{
						if(t.position.x > startTouch.x){
							if(RightSwipe != null) RightSwipe();
						}else{
							if(LeftSwipe != null) LeftSwipe();
						}
					}
				}
				startTouch = Vector2.zero;
				break;
			}
			break;
		case 2:
			tOne = Input.GetTouch(0);
			tTwo = Input.GetTouch(1);
			//early out if both fingers aren't moving
			if(tOne.phase != TouchPhase.Moved && tTwo.phase != TouchPhase.Moved) break;

			currentDistance = tOne.position - tTwo.position;
			previousDistance = (tOne.position - tOne.deltaPosition) - (tTwo.position - tTwo.deltaPosition);

			touchDelta = currentDistance.magnitude - previousDistance.magnitude;

			angleOffset = Vector2.Angle(previousDistance, currentDistance);
			crossVector = Vector3.Cross(previousDistance, currentDistance);
			clockwise = (crossVector.z > 0) ? false : true;

			if(angleOffset > 0.01 && Rotate != null) Rotate(clockwise, angleOffset);

			//positive value will be zooming in, negative one will be zooming out (Field of View)
			if(Zoom != null && Mathf.Abs(touchDelta) > zoomThreshold) Zoom(touchDelta);
			break;
		default:
			break;
		}
	}
	
}
