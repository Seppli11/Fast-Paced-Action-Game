using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtable : Hurtable {

	// Use this for initialization
	void Start ()
	{
	    DieEvents += Died;
	}

    void Died(GameObject attacker, Weapon weapon, Hurtable hurtable)
    {
        Debug.Log("Died");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
