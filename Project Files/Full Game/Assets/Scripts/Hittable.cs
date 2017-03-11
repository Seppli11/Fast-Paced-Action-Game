using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hittable : MonoBehaviour
{
    public abstract void Hitted(GameObject objectWhichHittedMe, Weapon weapon);
}
