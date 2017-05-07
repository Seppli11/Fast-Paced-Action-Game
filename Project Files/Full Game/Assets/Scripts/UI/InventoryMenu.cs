

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handels Inventory(cells) and the players weapon and the weaponPanels.
/// </summary>
public class InventoryMenu : MonoBehaviour {
	public static InventoryMenu inventoryMenu; //gets set in the Awake Method of InventoryMenu
	public GameObject itemImagePrefab;
	public static GameObject sItemImagePrefab
	{
		get;
		private set;
	}

	public GameObject invisibleItems;

	public Transform parentOfInventoryCells;
	public InventoryCell weapon1Cell, weapon2Cell;
	public WeaponPanel weaponPanel1, weaponPanel2;

	public Weapon weapon1
	{
		get
		{
			return (Weapon) weapon1Cell.item;
		}

		set
		{
			weapon1Cell.item = value;
			if (value != null)
			{
				if (value.itemSprite != null)
				{
					weaponPanel1.weaponSprite = value.itemSprite; //sets the sprite of the weaponPanel1
					Debug.Log("Set weaponPanel1 Sprite");
				} else
					Debug.LogError("Item '" + value.itemName + "' has not sprite assigned!");

			} else
			{
				weaponPanel1.weaponSprite = null;
			}
		}
	}

	public Weapon weapon2
	{
		get
		{
			return (Weapon)weapon2Cell.item;
		}

		set
		{
			weapon2Cell.item = value;
			if (value != null)
			{
				if (value.itemSprite != null)
					weaponPanel2.weaponSprite = value.itemSprite; //sets the sprite of the weaponPanel2
				else
					Debug.LogError("Item '" + value.itemName + "' has not sprite assigned!");
			}
			else
			{
				weaponPanel2.weaponSprite = null;
			}
		}
	}

	private void Awake()
	{
		inventoryMenu = this;
		sItemImagePrefab = itemImagePrefab;
		
	}

	// Use this for initialization
	void Start () {
		weapon1 = WeaponFactory.weaponFactory.GetSword(Player.player, 6, 1);
	}
	
	// Update is called once per frame
	void Update () {

		/*weaponPanel1.weaponSprite = weapon1.itemSprite;
		weaponPanel2.weaponSprite = weapon2.itemSprite;*/
	}

	public bool AddItem(Item i, int position = -1)
	{
		if (i == null) return false;
		InventoryCell freeCell = null;
		for(int n = 0; n < parentOfInventoryCells.childCount; n++)
		{
			Transform childTransform = parentOfInventoryCells.GetChild(n);
			//Debug.Log(childTransform.name + ": " + childTransform.childCount);
			if(childTransform.childCount == 0)
			{
				if (childTransform.GetComponent<InventoryCell>().CanTakeItem(i))
				{
					freeCell = childTransform.GetComponent<InventoryCell>();
					break;
				}
			}
		}
		if (position >= 0)
		{
			if (parentOfInventoryCells.GetChild(position).childCount == 0)
			{
				if (parentOfInventoryCells.GetChild(position).gameObject.GetComponent<InventoryCell>().CanTakeItem(i))
				{
					freeCell = parentOfInventoryCells.GetChild(position).gameObject.GetComponent<InventoryCell>();
				}
			}
		}

		if (!freeCell) return false;
		freeCell.item = i;
		i.gameObject.GetComponent<Renderer>().enabled = false;
		i.PickedUp(Player.player);
		return true;
	}

	public Item RemoveItemAt(int cellPosition, bool deleteItem = false)
	{
		if (cellPosition >= parentOfInventoryCells.childCount) return null;
		if (cellPosition < 0) return null;
		InventoryCell cell = parentOfInventoryCells.GetChild(cellPosition).GetComponent<InventoryCell>();
		cell.item.GetComponent<Renderer>().enabled = true;
		Item returnItem = cell.item;
		cell.item = null;
		return returnItem;
	}

	public Item[] GetItems()
	{
		Item[] items = new Item[parentOfInventoryCells.childCount];
		for(int i = 0; i < parentOfInventoryCells.childCount; i++)
		{
			items[i] = parentOfInventoryCells.GetChild(i).GetComponent<InventoryCell>().item;
		}
		return items;
	}
}
