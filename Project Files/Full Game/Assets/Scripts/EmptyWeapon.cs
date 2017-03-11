using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyWeapon : Weapon
{

    public RuntimeAnimatorController AnimatorController;
	// Use this for initialization
    public EmptyWeapon(GameObject owner) : base(owner)
    {
        AnimatorController = Resources.Load<RuntimeAnimatorController>("Assets/Animation/Main Character/EmptyHand/MC_Controller_Empty");
        if (AnimatorController == null) throw new Exception("AnimatorController not initialized!");
    }



    public override RuntimeAnimatorController GetAnimatorController()
    {
        return AnimatorController;
    }

    public override WeaponReturn TryToAttack(Vector2 attackingVectore)
    {
        Debug.Log("EmptyWeapon Shoots");
        return WeaponReturn.Attacked;
    }
}
