using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	private RectTransform rectTransform;
	private Image itemImage;

	private Item _item;
	public Item item
	{
		get { return _item; }
		set
		{
			Debug.Log("set '" + value + "' to cell '" + name + "'.");
			if(value == null)
			{
				if (transform.childCount != 0)
				{
					transform.GetChild(0).SetParent(item.transform);
				}
				_item = null;
				return;
			}
			if (item != null)
			{
				Debug.Log("Cell '" + name + "' was occupied!");
				return; //checks if the cell is free or not
			}
			if (value.itemType.IndexOf(acceptedItemTypes, useSpecifiedItemType: false) == -1)
			{
				Debug.LogWarning("InventoryCell '" + name + "' can't take Items of the type " + value.itemType);
				_item = null;
				return;
			}
			_item = value;
			value.itemImage.transform.SetParent(transform);
		}
	}

	public ItemType[] acceptedItemTypes = {};

	private bool hoverOverCell = false;
	// Use this for initialization
	void Start () {
		rectTransform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		if (item != null && hoverOverCell)
		{
			item.UpdateHowerText();
			ContextMenu.contextMenu.hoverText.text = item.contextText;
		}
	}

	/// <summary>
	/// Checks if the cell is currently free and if the item type machtes with the accepted items of the cell.
	/// If all checks are true, the function will return true;
	/// </summary>
	/// <param name="i"></param>
	/// <returns></returns>
	public bool CanTakeItem(Item i)
	{
		if (item) return false;
		if (i.itemType.IndexOf(acceptedItemTypes, false) != -1) return true;
		return false;
	}

	public void OnDrop(PointerEventData eventData)
	{
		if(!item)
		{
			Dragable.currentlyDraging.transform.SetParent(transform);
			item = Dragable.currentlyDraging.GetComponent<Dragable>().item;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		hoverOverCell = true;
		if (item != null)
		{
			ContextMenu.contextMenu.visible = true;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		hoverOverCell = false;
		if (item != null)
		{
			ContextMenu.contextMenu.visible = false;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		ItemPanel.itemPanel.currentItem = item;
		Weapon w = item as Weapon;
		if(w != null)
		{
			WeaponUpgradePanel.weaponUpgradePanel.currentWeapon = w;
		}
	}
}
