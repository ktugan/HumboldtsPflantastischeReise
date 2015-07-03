using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(LayoutElement))]
[RequireComponent(typeof(Image))]
public class InventarItem : MonoBehaviour {
	[HideInInspector]
	public int id;
	[HideInInspector]
	public string title;
	private Button button;
	private Animator anim;
	private Inventar parentInventar;
	private const string flashing = "flash";
	private bool flash = false;
	private ICustomItemAction customAction;

	
	public InventarItem init(int newid, Action OnClick = null)
	{
		button = GetComponent<Button>();
		parentInventar = GetComponentInParent<Inventar>();
		id = newid;
		customAction = GetComponent<ICustomItemAction>();
		button.onClick.AddListener(()=>{
			if(OnClick != null)OnClick();
			if(customAction != null) customAction.doCustomAction();
			parentInventar.inventarItemClicked(id);
		});
		anim = GetComponent<Animator>();
		return this;
	}
	public void toggleFlash()
	{
		flash = !flash;
		anim.SetBool(flashing, flash);
	}
	public void toggleFlash(bool On)
	{
		flash = On;
		anim.SetBool(flashing, flash);
	}
	public void InvokeAction()
	{
		button.onClick.Invoke();
	}
	public void addAction(UnityEngine.Events.UnityAction newAction)
	{
		button.onClick.AddListener(newAction);
	}
}
