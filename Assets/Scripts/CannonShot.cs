using UnityEngine;
using System.Collections;

public class CannonShot : MonoBehaviour {
	public GameObject Projectile;
	public Transform Muzzle;
	public Vector2 Limits;
	public float StartForce = 20f;
	public float damping = 1f;
	private GameObject cache;
	private float inputHeight, currentVel, currentZ;


	void Update () {
		#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1
		if(Input.touches.Length > 0)inputHeight = Input.GetTouch(0).position.y/Screen.height;
		#endif
		#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBPLAYER
		inputHeight = Input.mousePosition.y / Screen.height;
		if(Input.GetKeyUp(KeyCode.F)) shoot();
		#endif
		float newZ = Mathf.LerpAngle(Limits.x, Limits.y, inputHeight);
		currentZ = Mathf.SmoothDamp(currentZ, newZ, ref currentVel, damping);
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, currentZ);


	}

	public void shoot(){
		cache = Instantiate(Projectile, Muzzle.position, Muzzle.rotation) as GameObject;
		cache.GetComponent<Rigidbody2D>().AddForce(Muzzle.right * StartForce,ForceMode2D.Impulse);
	}
}
