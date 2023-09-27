using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : CharactorManager<EnemyData>
{
    public EnemyData enemyData;
    public override EnemyData GetCharactorData()
    {
        return enemyData;
    }

    public override void TakeDamage(Elemental damage)
    {
        float damageDeal = 0;
        if (currentHp > 0)
        {
            damageDeal = CalcDamageRecieve(GetCharactorData(), damage);
            currentHp -= damageDeal;
            TextDamageAsset.Instance.CreateTextDamage(transform.position, damageDeal, damage._elementalType);
        }
        GameController.OnEnemyTakeDamage?.Invoke(gameObject, damageDeal);
    }
    public override void InitHp()
    {
        currentHp = GetCharactorData().GetMaxHp();
    }
    public override void InitKnockbackGauge()
    {
        curentKnockBackGauge = GetCharactorData().GetMaxKnockbackGauge();
    }
    public override float ReturnMaxKnockBackGauge(){
        return GetCharactorData().GetMaxKnockbackGauge();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        GameController.OnEnemyDead += GetCharactorData().Dead;
        GameController.OnEnemyTakeDamage += GetCharactorData().CheckDead;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        GameController.OnEnemyDead -= GetCharactorData().Dead;
        GameController.OnEnemyTakeDamage -= GetCharactorData().CheckDead;
    }

    
}
