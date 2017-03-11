using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    private Weapons _weaponType = Weapons.Unknown;
    public Weapons WeaponType
    {
        get { return _weaponType; }
    }

    private int _damage;
    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    protected TimerManager timerManager = TimerManager.STimerManager;

    protected GameObject owner;

    public Weapon(GameObject owner, int damage = 0, Weapons weaponType = Weapons.Unknown)
    {
        this.owner = owner;
        this.Damage = damage;
        this._weaponType = weaponType;
    }


    public abstract void UpdateWeapon();

    public abstract WeaponReturn TryToAttack(Vector2 attackingVectore);
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
    Sword,
    Unknown
}