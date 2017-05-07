using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedipackItem : Item {
	public int heal;

	public override void UpdateHowerText()
	{
		newLine = "Heilt: " + heal;
		base.UpdateHowerText();
	}

	protected override void Use_Internal()
	{
		Player.player.GetComponent<PlayerHurtable>().Heal(heal);
		InventoryInfo.inventoryInfo.currencyLeft += heal * 5;
	}
}
