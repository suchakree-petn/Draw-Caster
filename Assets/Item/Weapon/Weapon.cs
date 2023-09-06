using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public ElementalType elementType;
    public float activeRate;

    virtual public void Attack(GameObject attacker){}
    virtual public void HoldAttack(GameObject attacker){}
}