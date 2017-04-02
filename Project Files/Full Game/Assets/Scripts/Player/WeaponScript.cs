using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private TimerManager timerManager = TimerManager.STimerManager;

    private Animator animator;

    private Controls controls;

	private Weapon _weapon1;
    public Weapon Weapon1
    {
        get { return _weapon1; }
        set
        {
			if(Weapon1 != null)
				_weapon1.Destroy();
			_weapon1 = value;
			weaponPanel1.weaponSprite = value.WeaponSprite;
        }
    }
	private Weapon _weapon2;
    public Weapon Weapon2
    {
        get { return _weapon2; }
        set
        {
			if(Weapon2 != null)
				_weapon2.Destroy();
			_weapon2 = value;
			weaponPanel2.weaponSprite = value.WeaponSprite;
        }
    }

	public WeaponPanel weaponPanel1, weaponPanel2;

    private bool attacking = false;
    // Use this for initialization
    void Start ()
    {
		Weapon1 = WeaponFactory.weaponFactory.GetEmptyHand(gameObject);
		Weapon2 = WeaponFactory.weaponFactory.GetSword(gameObject, 10, 2);

		controls = Controls.StaticControls;
        Debug.Log(controls);
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

	    if (controls.ShootButton1)
	    {
			HandlingAttackReturn(Weapon1.Attack(), weaponPanel1, Weapon1);
			
	    }
	    if (controls.ShootButton2)
	    {
			HandlingAttackReturn(Weapon2.Attack(), weaponPanel2, Weapon2);
		}
	}


    void HandlingAttackReturn(AttackReturn weaponReturn, WeaponPanel panel, Weapon weapon)
    {
		//Debug.Log("Attack from " + weapon.name + " returned " + weaponReturn);
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
		Debug.Log("TimeNeeded: " + timeNeeded + ", waitTime: " + weapon.attackWaitTime);
	}
}

