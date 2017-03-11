using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    private int _damage;
    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    protected TimerManager timerManager = TimerManager.STimerManager;

    protected GameObject owner;

    public Weapon(GameObject owner, int damage = 0)
    {
        this.owner = owner;
        this.Damage = damage;
    }

    public abstract WeaponReturn TryToAttack(Vector2 attackingVectore);

    public abstract RuntimeAnimatorController GetAnimatorController();
}

public enum WeaponReturn
{
    Attacked,
    OutOfAmo,
    Wait
}

public enum Weapons
{
    EmptyHand,
    Sword
}