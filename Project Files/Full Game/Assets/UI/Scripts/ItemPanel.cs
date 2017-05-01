using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour {
	public static ItemPanel itemPanel
	{
		get;
		private set;
	}

	public GameObject summaryGo;
	public Image itemImage;
	public Text itemName;

	public Text itemDescription;

	private Item _currentItem;
	public Item currentItem
	{
		get { return _currentItem; }
		set
		{
			_currentItem = value;
			if(_currentItem == null)
			{
				ResetToDefaultValues();
				return;
			}

			summaryGo.SetActive(true);

			itemImage.sprite = currentItem.itemSprite;
			itemName.text = currentItem.itemName;
			itemDescription.text = currentItem.contextText;
		}
	}

	private void Awake()
	{
		 itemPanel = this;
	}
	// Use this for initialization
	void Start () {
		ResetToDefaultValues();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateItemPanel()
	{
		currentItem.UpdateHowerText();
		currentItem = currentItem;
	}


	public void ResetToDefaultValues()
	{
		summaryGo.SetActive(false);
		itemName.text = "";
		itemDescription.text = "";
	}
}
