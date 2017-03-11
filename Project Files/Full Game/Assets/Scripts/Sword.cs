using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon {
    
    public RuntimeAnimatorController AnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Main Character/Sword/MC_Controller_Sword.controller");

    private bool attacking;
    private Vector2 attackingVector = Vector2.zero;
    private float attackDistance = 3;

    public Sword(GameObject owner) : base(owner, 5)
    {
    }

    void FixedUpdate()
    {
        if (attacking)
        {
            RaycastHit2D hit = Physics2D.Raycast(owner.transform.position, attackingVector, attackDistance, 10);
            if (hit.collider != null)
            {
                GameObject hittedGameObject = hit.transform.gameObject;
                Hittable hittable = hittedGameObject.GetComponent<Hittable>();
                if (hittable != null)
                {
                    hittable.Hitted(owner, this);
                }
            }
        }
    }

    public override WeaponReturn TryToAttack(Vector2 attackingVector)
    {
        if(attacking) return WeaponReturn.Wait;
        this.attackingVector = attackingVector;
        attacking = true;
        TimerManager.STimerManager.CreateTimer(0.5f, timer => attacking = false);
        return WeaponReturn.Attacked;

    }

    public override RuntimeAnimatorController GetAnimatorController()
    {
        return AnimatorController;
    }
}
