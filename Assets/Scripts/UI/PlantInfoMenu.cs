using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlantInfoMenu : MonoBehaviour {
	[SerializeField]
	private PlanzenInfoHolder[] pflanzenInfos;
	[SerializeField]
	private Text titleText;
	[SerializeField]
	private Image originalImage;
	[SerializeField]
	private Text infoText;
	
	public void swapInfoAndShow(int index)
	{
		if(index > pflanzenInfos.Length -1) return;
		PlanzenInfoHolder selectedInfo = pflanzenInfos[index];
		titleText.text = selectedInfo.Title;
		originalImage.sprite = selectedInfo.realImage;
		infoText.text = selectedInfo.InfoText;
	}
}
