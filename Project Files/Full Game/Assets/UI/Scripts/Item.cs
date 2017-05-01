using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Represent an Item. It handels the inventory drag and drop thing and basic informations.
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Item : MonoBehaviour{

	public Sprite itemSprite
	{
		get { return GetComponent<SpriteRenderer>().sprite; }
	}

	private GameObject _itemImage;

	public GameObject itemImage
	{
		get
		{
			if (!_itemImage)
			{
				_itemImage = Instantiate(InventoryMenu.sItemImagePrefab, transform);
				_itemImage.GetComponent<Image>().sprite = itemSprite;
				_itemImage.GetComponent<Dragable>().item = this;
			}
			return _itemImage;
		}
	}

	[LanguageAttribute()]
	public string itemName;

	private List<string> _itemDescriptionLines = new List<string>();
	
	public List<string> itemDescriptionLines
	{
		get { return _itemDescriptionLines; }
	}

	public ItemType itemType;

	public bool laysOnGround;

	private Renderer renderer;

	/// <summary>
	/// Makes a new line in the item description.
	/// </summary>
	public string newLine
	{
		set
		{
			_itemDescriptionLines.Add(value);
		}
	}
	/// <summary>
	/// The text of the hovering text in the inventory menu.
	/// </summary>
	public string contextText {
		get;
		protected set;
	}

	protected void Start()
	{
		renderer = GetComponent<Renderer>();
	}
	protected void Update()
	{
		renderer.enabled = laysOnGround;
	}

	public virtual void PickedUp (GameObject owner)
	{

	}

	/// <summary>
	/// gets called from the <see cref="InventoryCell"/> in the Update() method.
	/// The base method compiles the description and the titel in one string.
	/// </summary>
	public virtual void UpdateHowerText()
	{
		string itemDescription = "";
		foreach(string s in _itemDescriptionLines)
		{
			itemDescription += s + "\n";
			
		}
		contextText = itemName + "\n\n" + itemDescription;
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
	    if (!laysOnGround) return;
		if (collision.gameObject.GetComponent<Player>() != null)
		{
			bool r = InventoryMenu.inventoryMenu.AddItem(this);
		    if (!r) Debug.Log("Couldn't add item " + itemName);
		    else laysOnGround = false;
		}
	}
}

public enum GeneralItemType
{
	Weapon
}

public enum SpecificItemType
{
	Sword,
	LickAttack,
	NotSpecified
}

[Serializable]
public class ItemType
{
	public GeneralItemType generalItemType;
	public SpecificItemType specificItemType;

	public ItemType(GeneralItemType generalItemType, SpecificItemType specificItemType)
	{
		this.generalItemType = generalItemType;
		this.specificItemType = specificItemType;
	}

	public int IndexOf(ItemType[] array, bool useSpecifiedItemType = true)
	{
		for(int i = 0; i < array.Length; i++)
		{
			var ia = array[i];
			if(ia.generalItemType == generalItemType)
			{
				if (!useSpecifiedItemType) return i;
				if (ia.specificItemType == specificItemType) return i; 
			}
		}

		return -1;
	}
}

