using UnityEngine;
using System.Collections;

public static class ExtensionMethods{

	public static float Difference(this float f, float a, float b){
		return Mathf.Abs(a-b);
	}
}
