using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {
	public static List<SaveState> saveStates = new List<SaveState>();
	public static SaveState currentSaveState;
	public static GameControl gameControl;

	public bool autoSafe = true;
	public float autoSafeIntervall = 60f; //seconds
	private float lastAutoSave;
	public bool shouldLoadScene = false;

	private void Awake()
	{
		if (gameControl == null)
		{
			gameControl = this;
			DontDestroyOnLoad(gameObject);
			SceneManager.sceneLoaded += OnLevelFinishedLoading;
		} else
		{
			Destroy(gameObject);
		}
	}

	private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		if (!shouldLoadScene) return;
		if (currentSaveState != null)
		{
			LoadSaveState(currentSaveState);
			lastAutoSave = Time.time;
			shouldLoadScene = false;
			SaveSaveState(currentSaveState);
		}
	}

	private void Update()
	{
		if (autoSafe)
		{
			if (Time.time - lastAutoSave > autoSafeIntervall)
			{
				lastAutoSave = Time.time;
				if(currentSaveState != null)
					SaveSaveState(currentSaveState);
				Debug.Log("Autosaved");
			}
		}
	}

	public SaveState CreateNewSaveState(string name)
	{
		SaveState ss = new SaveState(name, ("/" + name + ".dat"), new DateTime().ToString());
		for (int i = 0; saveStates.Contains(ss); i++)
		{
			ss = new SaveState((name + "_" + name), ("/" + name + "_" + i + ".dat"), ss.lastSaved);
		}
		saveStates.Add(ss);
		SaveSaveStateList();
		return ss;
	}

	public void DeleteSaveState(SaveState ss)
	{
		File.Delete(Application.persistentDataPath + ss.fileName);
		saveStates.Remove(ss);
		SaveSaveStateList();
	}

	public void SaveSaveStateList()
	{

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/saveStates.dat");

		bf.Serialize(file, saveStates);
		file.Close();
		Debug.Log("Saved SaveStateList with " + saveStates.Count + " entries.");
	}

	public void LoadSaveStateList()
	{
		if (File.Exists(Application.persistentDataPath + "/saveStates.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/saveStates.dat", FileMode.Open);
			saveStates = (List<SaveState>)bf.Deserialize(file);
			file.Close();
		}
		else
		{
			Debug.LogWarning("SaveStateList doesn't exist.");
			saveStates = new List<SaveState>();
		}
		Debug.Log("Loaded SaveStateList with " + saveStates.Count + " entries.");

	}

	public void SaveSaveState(SaveState saveState)
	{
		InventoryData inventoryData = new InventoryData();
		InventoryMenu inventory = InventoryMenu.inventoryMenu;

		inventoryData.quickslot1 = new WeaponData(inventory.weapon1);
		inventoryData.quickslot2 = new WeaponData(inventory.weapon2);
		//Debug.Log("Saved '" + inventory.weapon1.itemName + "' in quickslot1.");
		//Debug.Log("Saved '" + inventory.weapon2.itemName + "' in quickslot2.");**/
		//TODO quickslot 3

		Item[] items = inventory.GetItems();
		ItemData[] itemDatas = new ItemData[items.Length];
		for(int i = 0; i < items.Length; i++) {
			itemDatas[i] = GetItemData(items[i]);
			//Debug.Log("Saved '" + items[i].itemName + "' in inventory slot " + i);
		}
		inventoryData.items = itemDatas;

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + saveState.fileName);

		bf.Serialize(file, inventoryData);
		file.Close();
		saveState.lastSaved = new DateTime().ToString();
		Debug.Log("Saved SaveState '" + saveState.name + "'.");
		SaveSaveStateList();
	}


	public void LoadSaveState(SaveState saveState)
	{
		if(File.Exists(Application.persistentDataPath + saveState.fileName))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + saveState.fileName, FileMode.Open);
			InventoryData inventoryData = (InventoryData)bf.Deserialize(file);
			file.Close();

			InventoryMenu inventory = InventoryMenu.inventoryMenu;
			inventory.weapon1 = RestoreItem(inventoryData.quickslot1) as Weapon;
			inventory.weapon2 = RestoreItem(inventoryData.quickslot2) as Weapon;
			//Debug.Log("Restored " + inventory.weapon1 + " in weapon1");
			//Debug.Log("Restored " + inventory.weapon2 + " in weapon2");
			//inventory.weapon3 = RestoreItem(inventoryData.quickslot3) as Weapon;

			for (int i = 0; i < inventoryData.items.Length; i++)
			{
				ItemData iData = inventoryData.items[i];
				Item item = RestoreItem(iData);
				if (item == null) continue;
				inventory.AddItem(item, i);
				//sDebug.Log("Restored '" + item.itemName + "' item in inventory slot " + i);
			}
		} else
		{
			//InventoryMenu.inventoryMenu.weapon1 = WeaponFactory.weaponFactory.GetSword(Player.player, 5, 1f);
			Debug.LogWarning("SaveState doesn't exist! Values were set to default.");
		}

		Debug.Log("Loaded SaveState '" + currentSaveState.name + "'.");
	}

	private ItemData GetItemData(Item i)
	{
		if (i == null) return null;
		ItemData iData = null;
		switch(i.itemType.generalItemType)
		{
			case GeneralItemType.Weapon:
				iData = new WeaponData(i as Weapon);
				break;
			default:
				Debug.LogError("GeneralItemType '" + i.itemType.generalItemType + "' isn't supported!");
				break;
		}
		return iData;
	}

	private Item RestoreItem(ItemData item)
	{
		if (item == null) return null;
		if (item.itemType == null) return null;
		Item rItem = null;
		switch(item.itemType.generalItemType)
		{
			case GeneralItemType.Weapon:
				//rItem = WeaponFactory.weaponFactory.GetWeapon(item as WeaponData, Player.player);
				break;
			default:
				Debug.LogError("GeneralItemType '" + item.itemType.generalItemType + "' isn't supported!");
				return null;
		}
		return rItem;
	}

	[Serializable]
	public class WeaponData : ItemData
	{
		public Upgrade[] upgrades;

		public int damage = -1;
		public float raycastRange = -1;
		//public float coliderSize = -1;
		public int maxLoadedAmmo = -1;
		public int maxAmmoInMagasin = -1;
		public float attackTime = -1;
		public float reloadTime = -1;

		public WeaponData(Weapon w) : base(w)
		{
			if (w == null) return;
			upgrades = w.avaibleUpgrades;

			damage = w.damage;
			raycastRange = w.raycastLength;
			maxLoadedAmmo = w.maxLoadedAmmo;
			maxAmmoInMagasin = w.maxAmmoInMagasin;
			attackTime = w.attackTime;
			reloadTime = w.reloadTime;
		}

	}

	[Serializable]
	public abstract class ItemData
	{
		public ItemType itemType;

		protected ItemData(Item i)
		{
			if(i != null)
				itemType = i.itemType;

		}
	}

	[Serializable]
	public class InventoryData
	{
		public WeaponData quickslot1, quickslot2;
		public ItemData quickslot3;
		public ItemData[] items;
	}
	
}

[Serializable]
public class SaveState
{
	public string name;
	public string fileName;
	public string lastSaved;

	public SaveState(string name, string fileName, string lastSaved)
	{
		this.name = name;
		this.fileName = fileName;
		this.lastSaved = lastSaved;
	}

	// override object.Equals
	public override bool Equals(object obj)
	{

		if (obj == null || GetType() != obj.GetType())
		{
			return false;
		}

		if(obj is SaveState)
		{
			SaveState ss = obj as SaveState;
			if (ss.name == name && ss.fileName == fileName) return true;
			else return false;
		}

		return base.Equals(obj);
	}

	// override object.GetHashCode
}