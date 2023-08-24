using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elemental
{

    public SpellElement _spellElement;
    public float _damage;
    public CharactorData _attacker ;
    public Elemental(SpellElement type, float damage, CharactorData attacker)
    {
        this._spellElement = type;
        this._damage = damage;
        this._attacker = attacker;
    }
    public static Elemental DamageCalculation(SpellElement type, CharactorData attacker, float _baseSkillDamageMultiplier)
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

    static private float CalcDamage(CharactorData attacker, float _baseSkillDamageMultiplier, SpellElement type)
    {
        float _elementBonusDamage = 0;
        switch (type)
        {
            case SpellElement.Fire:
                _elementBonusDamage = attacker._fireBonusDamage;
                break;
            case SpellElement.Thunder:
                _elementBonusDamage = attacker._thunderBonusDamage;
                break;
            case SpellElement.Frost:
                _elementBonusDamage = attacker._frostBonusDamage;
                break;
            case SpellElement.Wind:
                _elementBonusDamage = attacker._windBonusDamage;
                break;
            default:
                Debug.Log("Elemental Not Found");
                break;
        }
        return CalcBaseDamage(attacker, _baseSkillDamageMultiplier) * (1+(attacker._bonusDamage + _elementBonusDamage));
    }
}
