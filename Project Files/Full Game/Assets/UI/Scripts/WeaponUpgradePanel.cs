using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class WeaponUpgradePanel : MonoBehaviour {
	public static WeaponUpgradePanel weaponUpgradePanel;

	public GameObject upgradePrefab;

	private Weapon _currentWeapon;
	public Weapon currentWeapon
	{
		get { return _currentWeapon; }
		set
		{
			_currentWeapon = value;
			if(currentWeapon == null)
			{
				ResetToDefault();
				return;
			}


			for(int i = 0; i < transform.childCount; i++) {
				Destroy(transform.GetChild(i).gameObject);
			}

			foreach(Upgrade u in currentWeapon.avaibleUpgrades)
			{
				var upgarde = u;
				currentWeapon.SetUpgrade(ref upgarde);
				GameObject go = Instantiate(upgradePrefab, transform);
				go.GetComponent<UpgradeUi>().upgrade = upgarde;
			}
		}
	}

	internal void Buy(ref Upgrade upgrade)
	{
		currentWeapon.Buy(ref upgrade);
		ItemPanel.itemPanel.UpdateItemPanel();
	}

	private void ResetToDefault()
	{
		
	}

	private void Awake()
	{
		weaponUpgradePanel = this;
	}

	void Start () {
		
	}
	
	void Update () {
		
	}

	
}

