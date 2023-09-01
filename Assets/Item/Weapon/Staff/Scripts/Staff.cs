using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Staff : Weapon
{
    public float _baseSkillDamageMultiplier;
    public override void Attack(GameObject attacker)
    {
        Elemental elemental = Elemental.DamageCalculation(elementType, attacker.GetComponent<PlayerManager>().playerData, _baseSkillDamageMultiplier);
        Debug.Log(elemental._damage);
    }

}
