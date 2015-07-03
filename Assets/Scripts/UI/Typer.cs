using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class Typer : MonoBehaviour {
	[Multiline]
	public string message = "Replace me with some Text," +
		"I'll be flying in";
	public float startDelay = 2f;
	public float typeDelay = 0.03f;
	private Text textComp;

	public void ShowText() {
		StartCoroutine(TypeIn());
	}
	public void HideText() {
		StartCoroutine(TypeOff());
	}
	
	void Awake(){
		textComp = GetComponent<Text>();
	}
	void Start()
	{
		ShowText();
	}

	public IEnumerator TypeIn(){
		yield return new WaitForSeconds(startDelay);

		for(int i = 0; i <= message.Length; i++){
			textComp.text = message.Substring(0,i);
			yield return new WaitForSeconds(typeDelay);
		}
	}
	public IEnumerator TypeOff(){
		
		for(int i = message.Length; i >= 0; i--){
			textComp.text = message.Substring(0,i);
			yield return new WaitForSeconds(typeDelay);
		}
	}

}
