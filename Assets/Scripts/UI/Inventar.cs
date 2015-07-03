using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(HorizontalLayoutGroup))]
public class Inventar : MonoBehaviour {
	[SerializeField]
	private GameObject[] startItems = new GameObject[0];
	[SerializeField]
	private bool doubles = false;
	[SerializeField]
	private Image itemFoundImage;
	[SerializeField]
	private Text itemTitle;
	private List<InventarItem> items = new List<InventarItem>();
	private Animator previewAnimator;
	public delegate void itemPresentNotifier(string presentItemName);
	public static event itemPresentNotifier itemArrived;
	


	void Start () {
		previewAnimator = itemFoundImage.GetComponent<Animator>();
		foreach(GameObject prefab in startItems)
		{
			addItem(prefab);
		}
	}
	public void addItemWithShow (GameObject item)
	{
		itemFoundImage.sprite = item.GetComponent<Image>().sprite;
		itemTitle.text = item.GetComponent<InventarItem>().title;
		previewAnimator.SetTrigger("show");
		addItem (item);
	}

	public void addItem(GameObject item)
	{
		if(!doubles && isPresent(item.name)) return;
		GameObject instance = Instantiate(item, transform.position, Quaternion.identity) as GameObject;
		instance.transform.SetParent(transform, false);
		items.Add(instance.GetComponent<InventarItem>().init(items.Count));
		if(itemArrived != null) itemArrived(instance.name);
		
	}
	public void removeItemByName(string itemname)
	{
		if(isPresent(itemname))
		{
			InventarItem item = findByName(itemname);
			items.Remove(item);
			Destroy(item.gameObject);
		}
	}
	public bool isPresent(string itemname)
	{
		return findByName(itemname) == null ? false : true;
	}
	public InventarItem findByName(string itemname)
	{
		return items.Find(x=> x.gameObject.name.ToUpper().Contains(itemname.ToUpper()));
	}
	public void inventarItemClicked(int id)
	{
	#if UNITY_EDITOR
		Debug.Log(id + " Clicked");
	#endif
	}
	public void flashByName(string itemname)
	{
		if(!isPresent(itemname)) return;
		findByName(itemname).toggleFlash();
	}
	public void flashByName(string itemname, bool On)
	{
		if(!isPresent(itemname)) return;
		findByName(itemname).toggleFlash(On);
	}

	public void invokeAction(string itemname)
	{
		if(!isPresent(name)) return;
		findByName(itemname).InvokeAction();
	}
	public void invokeAction(int id)
	{
		if(id < items.Count) 
			items[id].InvokeAction();
	}
}
