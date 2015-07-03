/*Class for detecting a mobile device shake
 * with lowpassfiltering for accuracy.
 * Possible improvement: Query & average all
 * acceleration events (in between frames)
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shake : MonoBehaviour {
	public delegate void shaking();
	public static event shaking ShakenX;
	public static event shaking ShakenY;
	public static event shaking ShakenZ;

	[Range(0f, 5f)]
	public float threshold = 2f;
	private float interval = 1.0f / 60.0f;
	// The greater the value of LowPassKernelWidthInSeconds, the slower the filtered value will converge towards current input sample (and vice versa).
	[Header("LowPassKernelWidthInSeconds ")]
	public float kernel = 1.0f;
	private float LowPassFilterFactor;
	private Vector3 lowPassValue, deltaAcceleration;
	
	void Start()
	{
		LowPassFilterFactor = interval / kernel;
	}


	void Update () {
		ShakeDetected();
	}
	private void ShakeDetected()
	{
		//apply lowpassfilter to acceleration
		deltaAcceleration = Input.acceleration-LowPassFilterAccelerometer();

		/*Check for alle axis whether the filtered acceleration
		 * exceeds the threshold. If that's the case AND we have
		 * subscribers emit the event
		 */

		if(Mathf.Abs(deltaAcceleration.x)>= threshold)
		{
			if(ShakenX != null) ShakenX();
		}
		if(Mathf.Abs(deltaAcceleration.y)>= threshold)
		{
			if(ShakenY != null) ShakenY();
		}
		if(Mathf.Abs(deltaAcceleration.z)>= threshold)
		{
			if(ShakenZ != null) ShakenZ();
		}      
	}
	public static void callShake()
	{
		if(ShakenX != null) ShakenX();
		if(ShakenY != null) ShakenY();
		if(ShakenZ != null) ShakenZ();

	}
	private Vector3 LowPassFilterAccelerometer(){

		lowPassValue = Vector3.Lerp(lowPassValue, Input.acceleration, LowPassFilterFactor);
		return lowPassValue;
	}
}
