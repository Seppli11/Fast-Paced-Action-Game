using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtable : Hurtable {
	public int pointsForPlayerMultiplier;

	[HideInInspector]
	public int pointsForPlayer;
	public ThrowableItemEntry[] throwableItems;
    new void Start()
    {
        base.Start();
        DieEvents += Died;
		DamagedEvents += EnemyHurtable_DamagedEvents;
		pointsForPlayer = Lifes * pointsForPlayerMultiplier;
    }

	private void EnemyHurtable_DamagedEvents(int health, int damage)
	{

	}

	void Died(GameObject attacker, Weapon weapon, Hurtable hurtable)
    {
		InventoryInfo.inventoryInfo.currencyLeft += pointsForPlayer;
		foreach(var i in throwableItems)
		{
			float r = UnityEngine.Random.value;
			if(r <= i.probability)
			{
				Item item = ItemFactory.itemFactory.SpawnItem(i.item);
				item.transform.position = transform.position;
			}
		}
		ProgressManager.progressManager.killedEnemies++;
		GameObject.Destroy(gameObject);

	}
}

[Serializable]
public struct ThrowableItemEntry
{
	public ItemType item;
	public float probability;
}
