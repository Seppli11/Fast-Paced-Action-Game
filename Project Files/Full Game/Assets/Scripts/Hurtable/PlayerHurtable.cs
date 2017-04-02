using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtable : Hurtable {

	// Use this for initialization
	void Start ()
	{
        base.Start();
	    DieEvents += Died;
	}

    void Died(GameObject attacker, Weapon weapon, Hurtable hurtable)
    {
        
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
