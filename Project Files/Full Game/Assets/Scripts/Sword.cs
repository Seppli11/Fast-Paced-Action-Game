using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    private bool attackAnimationRunning;
    private Vector2 attackingVector = Vector2.zero;
    private float attackDistance = 3;

    public Sword(GameObject owner) : base(owner, 5, Weapons.Sword)
    {
    }

    public override void UpdateWeapon()
    {
        Debug.DrawRay(owner.transform.position, Controls.StaticControls.CurrentDirection * attackDistance, Color.blue, 0.1f, false);
    }

    public override WeaponReturn TryToAttack(Vector2 attackingVector)
    {
        if (attackAnimationRunning)
        {
            Debug.Log("[Sword] Attack is already running!");
            return WeaponReturn.Wait;
        }

        attackingVector.Scale(new Vector2(1, -1));
        this.attackingVector = attackingVector;
        attackAnimationRunning = true;
        TimerManager.STimerManager.CreateTimer(0.5f, timer =>
        {
            attackAnimationRunning = false;
            Debug.Log("[Sword] Set attackAnimationRunning to false");
        });

        Debug.DrawRay(owner.transform.position, attackingVector * attackDistance, Color.cyan, 5f, false);
        RaycastHit2D hit = Physics2D.Raycast(owner.transform.position, attackingVector, attackDistance, 1 << LayerMask.NameToLayer("Hittable"));
        Debug.Log("[Sword] Raycase(" + owner.transform.position + ", " + attackingVector + ", " + attackDistance + ", " + LayerMask.NameToLayer("Hittable") +")");
        if (hit.collider != null)
        {
            GameObject hittedGameObject = hit.transform.gameObject;
            Debug.Log("Hitted " + hittedGameObject.name + ", " + hittedGameObject.GetComponents <MonoBehaviour>().ToString());
            Hittable hittable = hittedGameObject.GetComponent<PlayerHurtable>();
            if (hittable != null)
            {
                Debug.Log("Hitted Hittable");
                hittable.Hitted(owner, this);
            }
        }

        return WeaponReturn.Attacked;
    }
}
