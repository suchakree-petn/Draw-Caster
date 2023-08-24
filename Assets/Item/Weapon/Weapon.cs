using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public SpellElement elementType;

    virtual public void Attack(CharactorData attacker){}
}
