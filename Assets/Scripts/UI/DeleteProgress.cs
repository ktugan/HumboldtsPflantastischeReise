using UnityEngine;
using System.Collections;

public class DeleteProgress : MonoBehaviour {

	public void deletePrefs()
	{
		PlayerPrefs.DeleteAll();
	}
}
