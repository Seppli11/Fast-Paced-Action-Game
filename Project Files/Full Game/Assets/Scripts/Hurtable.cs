using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Died(GameObject attacker, Weapon weapon, Hurtable hurtable);

public class Hurtable : Hittable
{
    public event Died DieEvents;

    public int MaxLifes;
    public int Lifes;


    public void Heal(int healedLifes)
    {
        Lifes += healedLifes;
        if (Lifes > MaxLifes) Lifes = MaxLifes;
    }

    public void Damage(int damagedLifes, GameObject attacker = null, Weapon weapon = null)
    {
        Lifes -= damagedLifes;
        if (Lifes <= 0)
        {
            Die(attacker, weapon);
        }
    }

    public void Die(GameObject attacker, Weapon weapon)
    {
        Lifes = 0;
        DieEvents(attacker, weapon, this);
    }

    public override void Hitted(GameObject objectWhichHittedMe, Weapon weapon)
    {
        Damage(weapon.Damage, objectWhichHittedMe, weapon);
    }
}
