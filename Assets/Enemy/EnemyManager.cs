using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IDamageable
{
    public CharactorData enemyData;
    public Rigidbody2D _playerRb;

    public void TakeDamage(Elemental damage)
    {
        if (enemyData._currentHp > 0)
        {
            enemyData._currentHp -= CalcDamageRecieve(enemyData, damage);
        }else{
            GameController.OnEnemyDead?.Invoke(gameObject);
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
    private void OnEnable() {
        GameController.OnEnemyDead += enemyData.Dead;
    }
    private void OnDisable() {
        GameController.OnEnemyDead -= enemyData.Dead;
    }
}
