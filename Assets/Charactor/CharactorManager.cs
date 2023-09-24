using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharactorManager<T> : MonoBehaviour, IDamageable
{
    public float currentHp;
    public T charactorData {private get; set; }

    public abstract void TakeDamage(Elemental elementalDamage);

    public abstract T GetCharactorData();
    protected virtual void OnEnable()
    {
        GameController.OnBeforeStart += InitHp;

    }
    protected virtual void OnDisable()
    {
        GameController.OnBeforeStart -= InitHp;
    }

    float CalcDefense(CharactorData target)
    {
        return (target._defenseBase * (1 + target._defenseMultiplier)) + target._defenseBonus;
    }

    float CalcDMGReduction(CharactorData target, Elemental damage)
    {
        float _targetDefense = CalcDefense(target);
        return 1 - (_targetDefense / (_targetDefense + (5 * damage._attacker._level) + 500));
    }
    public float CalcDamageRecieve(CharactorData target, Elemental damage)
    {
        return damage._damage * CalcDMGReduction(target, damage) * (1 - target._bonusDamageReduction);
    }
    public virtual void InitHp(){}
    
}
