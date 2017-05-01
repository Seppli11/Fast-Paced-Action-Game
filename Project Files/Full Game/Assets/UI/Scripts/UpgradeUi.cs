using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUi : MonoBehaviour {
	public Text upgradeText;
	public Button upgradeButton;

	private Upgrade _upgrade;
	public Upgrade upgrade
	{
		get { return _upgrade; }
		set
		{
			_upgrade = value;
			upgradeText.text = upgrade.upgradeName + ": " + upgrade.currentValue;
		}
	}
	// Use this for initialization
	void Start () {
		if (InventoryInfo.inventoryInfo.currencyLeft < upgrade.currentCost | upgrade.currentStep >= upgrade.maxSteps)
		{
			upgradeButton.interactable = false;
		}
		else
		{
			upgradeButton.interactable = true;
		}
		upgradeButton.GetComponentInChildren<Text>().text = upgrade.currentCost.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		if (InventoryInfo.inventoryInfo.currencyLeft < upgrade.currentCost)
		{
			upgradeButton.interactable = false;
		}
		else
		{
			upgradeButton.interactable = true;
		}
	}

	public void Buy()
	{
		InventoryInfo.inventoryInfo.currencyLeft -= upgrade.currentCost;

		WeaponUpgradePanel.weaponUpgradePanel.Buy(ref _upgrade);
		UpdateUpgrade();
	}

	void UpdateUpgrade()
	{
		upgradeText.text = upgrade.upgradeName + ": " + upgrade.currentValue;
		if (upgrade.currentStep < upgrade.maxSteps)
			upgradeButton.GetComponentInChildren<Text>().text = upgrade.currentCost.ToString();
		else
			upgradeButton.GetComponentInChildren<Text>().text = "Max";
	}
}
