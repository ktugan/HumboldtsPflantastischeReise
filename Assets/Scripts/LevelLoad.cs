using UnityEngine;
using System.Collections;

public class LevelLoad : MonoBehaviour {
	public int LevelNumber = 0;
	[SerializeField]
	private string LevelName;

	void Start()
	{

	}
	public void loadLevel()
	{
		if(string.IsNullOrEmpty(LevelName)) loadLevel(LevelNumber);
		else loadLevel(LevelName);
	}
	public void loadLevel(string name)
	{
		Application.LoadLevel(name);
	}
	public void loadLevel(int number)
	{
		Application.LoadLevel(number);
	}
}
