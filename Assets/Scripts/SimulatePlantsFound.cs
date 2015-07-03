using UnityEngine;
using System.Collections;

public class SimulatePlantsFound : MonoBehaviour {
	public void foundAllPlants () {
		for(int i = 1; i < 7; i++)
		{
			PlayerPrefs.SetInt("p"+i.ToString() , 1);
		}
	}
	

}
