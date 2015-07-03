using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class ShowFoundPlant : MonoBehaviour {
	[SerializeField]
	private string plantKey;
	[SerializeField]
	private string plantname;


	void Start () {
		bool show = PlayerPrefs.GetInt(plantKey, 0) == 0 ? false : true;
		GetComponent<Animator>().SetBool("show", show);
		if(show) GetComponentInChildren<Text>().text = plantname;
	}

}
