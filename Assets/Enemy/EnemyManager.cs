using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IDamageable
{
    public EnemyData enemyData;
    private DamagePopup damagePopup;
    public float currentHp;

    private void Start()
    {
        currentHp = enemyData._maxHp;
    }

    public void TakeDamage(Elemental damage)
    {
        if (currentHp > 0)
        {
            float damageDeal = CalcDamageRecieve(enemyData, damage);
            currentHp -= damageDeal;
            DamagePopup.CreateTextDamage(transform.position, damageDeal, damage._elementalType);
        }
        GameController.OnEnemyTakeDamage?.Invoke(gameObject);
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
    float CalcDamageRecieve(CharactorData target, Elemental damage)
    {
        return damage._damage * CalcDMGReduction(target, damage) * (1 - target._bonusDamageReduction);
    }
    public void CalcMaxHp()
    {
        currentHp = enemyData._maxHp;
    }
    private void OnEnable()
    {
        GameController.OnBeforeStart += CalcMaxHp;
        GameController.OnEnemyDead += enemyData.Dead;
        GameController.OnEnemyTakeDamage += enemyData.CheckDead;
        
    }
    private void OnDisable()
    {
        GameController.OnBeforeStart -= CalcMaxHp;
        GameController.OnEnemyDead -= enemyData.Dead;
        GameController.OnEnemyTakeDamage -= enemyData.CheckDead;
    }


}
