using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
	public static ItemFactory itemFactory
	{
		get;
		private set;
	}


	public GameObject medipackPrefab;
	// Use this for initialization

	private void Awake()
	{
		itemFactory = this;
	}

	public Item SpawnItem(ItemType type)
	{
		if(type.generalItemType != GeneralItemType.Item)
		{
			Debug.LogError("ItemType has to be a item, not a '" + type.generalItemType + "'.");
			return null;
		}

		Item i = null;
		switch(type.specificItemType)
		{
			case SpecificItemType.Medipack:
				i = Instantiate(medipackPrefab).GetComponent<Item>();
				break;
			default:
				Debug.LogError("SpecificItemType '" + type.specificItemType + "' isn't supported in the ItemFactory!");
				return null;
		}
		i.laysOnGround = true;
		return i;
	}
}

