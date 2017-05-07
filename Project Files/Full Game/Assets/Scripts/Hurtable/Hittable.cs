using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Side))]
public abstract class Hittable : MonoBehaviour
{
    public abstract void Hitted(GameObject objectWhichHittedMe, DamageDealer damageDealer);
}
