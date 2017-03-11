using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private TimerManager timerManager = TimerManager.STimerManager;

    private Animator animator;
    public WeaponAnimatorControllerTable WeaponAnimatorControllerTable;

    private Controls controls;

    private Weapon _weapon1;
    public Weapon Weapon1
    {
        get { return _weapon1; }
        set
        {
            _weapon1 = value;
        }
    }
    private Weapon _weapon2;
    public Weapon Weapon2
    {
        get { return _weapon2; }
        set
        {
            _weapon2 = value;
        }
    }

    private bool attacking = false;
    // Use this for initialization
    void Start ()
    {
        Weapon1 = new Sword(gameObject);
        Weapon2 = new EmptyWeapon(gameObject);

		controls = Controls.StaticControls;
        Debug.Log(controls);
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

	    if (controls.ShootButton1)
	    {
	        HandlingWeaponReturn(Weapon1.TryToAttack(controls.LastDirection));
	        animator.runtimeAnimatorController = WeaponAnimatorControllerTable.GetAnimatorController(Weapon1);
            animator.SetTrigger("attack");
            Debug.Log("Weapon 1 shooted");
	    }
	    if (controls.ShootButton2)
	    {
	        HandlingWeaponReturn(Weapon2.TryToAttack(controls.LastDirection));
            animator.runtimeAnimatorController = WeaponAnimatorControllerTable.GetAnimatorController(Weapon2);
            animator.SetTrigger("attack");
            Debug.Log("Weapon 2 shooted");
        }
	}

    void FixedUpdate()
    {
        Weapon1.UpdateWeapon();
        Weapon2.UpdateWeapon();
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

