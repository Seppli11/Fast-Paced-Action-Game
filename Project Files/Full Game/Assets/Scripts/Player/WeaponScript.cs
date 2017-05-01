using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private Animator animator;

    private Controls controls;

    private bool attacking = false;
    // Use this for initialization
    void Start ()
	{ 
		controls = Controls.StaticControls;
        animator = GetComponent<Animator>();
    }
	
	void Update () {

	    if (controls.ShootButton1)
	    {
			if(InventoryMenu.inventoryMenu.weapon1 != null)
				HandlingAttackReturn(InventoryMenu.inventoryMenu.weapon1.Attack(), InventoryMenu.inventoryMenu.weaponPanel1, InventoryMenu.inventoryMenu.weapon1);
	    }
	    if (controls.ShootButton2)
	    {
			if (InventoryMenu.inventoryMenu.weapon2 != null)
				HandlingAttackReturn(InventoryMenu.inventoryMenu.weapon2.Attack(), InventoryMenu.inventoryMenu.weaponPanel2, InventoryMenu.inventoryMenu.weapon2);
		}
	}


    void HandlingAttackReturn(AttackReturn weaponReturn, WeaponPanel panel, Weapon weapon)
    {
		Debug.Log(weaponReturn);
        switch (weaponReturn)
        {
           case AttackReturn.Attacked:
				StartCoroutine(WeaponPanelProgressBarAnimation(panel, weapon));
				break;
            case AttackReturn.NeedToReload:
                break;
            case AttackReturn.Waiting:
                break;
			case AttackReturn.Attacking:
				break;
            default:
                Debug.LogError("Unknown WeaponReturn '" + weaponReturn.ToString() + "'.");
                break;
        }
    }

	private IEnumerator WeaponPanelProgressBarAnimation(WeaponPanel panel, Weapon weapon)
	{
		float waitTime = weapon.attackWaitTime / 11;
		float startTime = Time.time;
		for(int i = 1; i <= 100; i+=10)
		{
			panel.progressBar.progression = i;
			yield return new WaitForSeconds(waitTime);
		}
		panel.progressBar.progression = 100;
		float timeNeeded = Time.time - startTime;
		//Debug.Log("TimeNeeded: " + timeNeeded + ", waitTime: " + weapon.attackWaitTime);
	}
}

