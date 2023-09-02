using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamageable
{

    public PlayerData playerData;
    public Rigidbody2D _playerRb;

    void IntialStats(){
        
    }

    public void TakeDamage(Elemental damage)
    {
        if (playerData._currentHp > 0)
        {
            playerData._currentHp -= CalcDamageRecieve(playerData, damage);
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
