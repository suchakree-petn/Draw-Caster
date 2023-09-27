using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public ElementalType elementType;
    public float activeRate;
    public LayerMask targetLayer;
    public float knockbackGaugeDeal;
    virtual public void Attack(GameObject attacker){}
    virtual public void HoldAttack(GameObject attacker){}
}
