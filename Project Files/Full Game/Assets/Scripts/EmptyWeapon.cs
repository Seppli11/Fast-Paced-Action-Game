using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyWeapon : Weapon
{

	// Use this for initialization
    public EmptyWeapon(GameObject owner) : base(owner, 0, Weapons.EmptyHand)
    {
    }


    public override WeaponReturn TryToAttack(Vector2 attackingVectore)
    {
        Debug.Log("EmptyWeapon Shoots");
        return WeaponReturn.Attacked;
    }

    public override void UpdateWeapon()
    {
        
        
    }
}
