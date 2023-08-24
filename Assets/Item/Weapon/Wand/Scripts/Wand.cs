using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Wand : Weapon
{
    public float _baseSkillDamageMultiplier;

    public override void Attack(CharactorData attacker)
    {
        Elemental elemental = Elemental.DamageCalculation(elementType, attacker, _baseSkillDamageMultiplier);
        Debug.Log(elemental._damage);
    }

}
