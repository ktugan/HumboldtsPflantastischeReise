using UnityEngine;
using System.Collections;

public abstract class UnitySingleton<T> : MonoBehaviour where T : MonoBehaviour{
	/*
	 * A Monobehaviour variant of the Singleton Pattern
	 * adapted from http://wiki.unity3d.com/index.php/Singleton
	 */

	private static T _instance = null;
	private static bool applicationIsQuitting = false;
	
	
	public static T instance {
		
		get {
			if(applicationIsQuitting) {
				return null;
			}
			
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType(typeof(T)) as T;
				if (_instance == null) {
					_instance = new GameObject().AddComponent<T>();
					_instance.gameObject.name = _instance.GetType().Name;
				}
			}
			
			return _instance;
			
		}
		
	}
	
	public static bool HasInstance {
		get {
			return !IsDestroyed;
		}
	}
	
	public static bool IsDestroyed {
		get {
			return (_instance == null) ? true : false;
		}
	}

	void OnDestroy () {
		_instance = null;
		applicationIsQuitting = true;
	}
	
	void OnApplicationQuit () {
		_instance = null;
		applicationIsQuitting = true;
	}
	
}