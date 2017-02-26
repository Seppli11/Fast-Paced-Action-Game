using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private Controls controls;

    private Weapon _weapon1;
    public Weapon Weapon1
    {
        get { return _weapon1; }
        set
        {
            _weapon1 = value;
            _weapon1.enabled = true;
        }
    }
    private Weapon _weapon2;
    public Weapon Weapon2
    {
        get { return _weapon2; }
        set
        {
            _weapon2 = value;
            _weapon2.enabled = true;
        }
    }

    // Use this for initialization
    void Start () {
		controls = Controls.StaticControls;
	}
	
	// Update is called once per frame
	void Update () {
	    if (controls.ShootButton1)
	    {
	        HandlingWeaponReturn(Weapon1.TryToAttack());
	    }
	    if (controls.ShootButton2)
	    {
	        HandlingWeaponReturn(Weapon2.TryToAttack());
	    }
	}


    void HandlingWeaponReturn(WeaponReturn weaponReturn)
    {
        switch (weaponReturn)
        {
            case WeaponReturn.Attacked:
                break;
            case WeaponReturn.OutOfAmo:
                break;
            case WeaponReturn.Wait:
                break;
            default:
                Debug.LogError("Unknown WeaponReturn '" + weaponReturn.ToString() + "'.");
                break;
        }
    }
}

