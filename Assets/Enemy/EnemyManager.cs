using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IDamageable
{
    public EnemyData EnemyData;
    public Rigidbody2D _playerRb;

    void IntialStats(){
        
    }

    public void TakeDamage(Elemental damage)
    {
        Debug.Log("TakeDamage" + damage._damage);
        if (EnemyData._currentHp > 0)
        {
            EnemyData._currentHp -= CalcDamageRecieve(EnemyData, damage);
        }
    }
    float CalcDefense(CharactorData target)
    {
        return (target._defenseBase * (1 + target._defenseMultiplier)) + target._defenseBonus;
    }

    float CalcDMGReduction(CharactorData target,Elemental damage)
    {
        float _targetDefense = CalcDefense(target);
        return 1-(_targetDefense / (_targetDefense + (5 * damage._attacker._level) +500));
    }
    float CalcDamageRecieve(CharactorData target, Elemental damage)
    {
        return damage._damage * CalcDMGReduction(target, damage) * (1 - target._bonusDamageReduction);
    }
}
