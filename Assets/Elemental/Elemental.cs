using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ElementalType{
    Default,
    Fire,
    Thunder,
    Frost,
    Wind
}
public class Elemental
{

    public ElementalType _elementalType;
    public float _damage;
    public CharactorData _attacker ;
    public Elemental(ElementalType type, float damage, CharactorData attacker)
    {
        this._elementalType = type;
        this._damage = damage;
        this._attacker = attacker;
    }
    public static Elemental DamageCalculation(ElementalType type, CharactorData attacker, float _baseSkillDamageMultiplier)
    {
        return new Elemental(type, CalcDamage(attacker,_baseSkillDamageMultiplier,type),attacker);
    }

    static private float CalcAttack(CharactorData attacker)
    {
        return (attacker._attackBase * (1+attacker._attackMultiplier)) + attacker._attackBonus;
    }

    static private float CalcBaseDamage(CharactorData attacker, float _baseSkillDamageMultiplier)
    {
        return CalcAttack(attacker) * _baseSkillDamageMultiplier;
    }

    static private float CalcDamage(CharactorData attacker, float _baseSkillDamageMultiplier, ElementalType type)
    {
        float _elementBonusDamage = 0;
        switch (type)
        {
            case ElementalType.Fire:
                _elementBonusDamage = attacker._fireBonusDamage;
                break;
            case ElementalType.Thunder:
                _elementBonusDamage = attacker._thunderBonusDamage;
                break;
            case ElementalType.Frost:
                _elementBonusDamage = attacker._frostBonusDamage;
                break;
            case ElementalType.Wind:
                _elementBonusDamage = attacker._windBonusDamage;
                break;
            default:
                Debug.Log("Elemental Not Found");
                break;
        }
        return CalcBaseDamage(attacker, _baseSkillDamageMultiplier) * (1+(attacker._bonusDamage + _elementBonusDamage));
    }
}
