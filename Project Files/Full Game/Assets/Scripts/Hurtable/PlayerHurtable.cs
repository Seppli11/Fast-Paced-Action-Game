using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtable : Hurtable {

	// Use this for initialization
	new void Start ()
	{
        base.Start();
	    DieEvents += Died;
	}

    void Died(GameObject attacker, Weapon weapon, Hurtable hurtable)
    {
		MenuManager.sMainMenu.ShowMenu(Menu.GameOverMenu, 0);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
