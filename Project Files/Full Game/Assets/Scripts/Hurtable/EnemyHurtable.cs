using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtable : Hurtable {
    void Start()
    {
        base.Start();
        DieEvents += Died;    
    }

    void Died(GameObject attacker, Weapon weapon, Hurtable hurtable)
    {
        GameObject.Destroy(gameObject);
    }
}
