using UnityEngine;
using System.Collections;

public class SetPlantsPref : MonoBehaviour {
	[SerializeField]
	private string prefID;

	public void foundPlant()
	{
		PlayerPrefs.SetInt(prefID, 1);
	}

}
