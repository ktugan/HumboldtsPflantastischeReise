using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeText : CollectibleBase {
	[SerializeField]
	private Text text;
	[Multiline]
	[SerializeField]
	private string text2change;

	protected override void triggerEffect()
	{
		text.text = text2change; 
	}
}
