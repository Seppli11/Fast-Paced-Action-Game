using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtable : Hurtable {
	public int pointsForPlayer;
    new void Start()
    {
        base.Start();
        DieEvents += Died;    
    }

    void Died(GameObject attacker, Weapon weapon, Hurtable hurtable)
    {
        GameObject.Destroy(gameObject);
		InventoryInfo.inventoryInfo.currencyLeft += pointsForPlayer;
    }
}
