using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimatorControllerTable : MonoBehaviour
{
    public RuntimeAnimatorController Default;
    public RuntimeAnimatorController EmptyHand;
    public RuntimeAnimatorController Sword;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public RuntimeAnimatorController GetAnimatorController(Weapon wepon)
    {
        switch (wepon.WeaponType)
        {
            case Weapons.EmptyHand:
                return EmptyHand;
            case Weapons.Sword:
                return Sword;
            case Weapons.Unknown:
                return Default;
            default:
                return Default;
        }
    }
}
