using UnityEngine;
using System.Collections;

public class PapageiAction : MonoBehaviour, ICustomItemAction {
	
	#region ICustomItemAction implementation

	public void doCustomAction ()
	{
		GameObject.FindGameObjectWithTag(Tags.Papagei).GetComponent<Echo>().playEcho();
	}

	#endregion
}
