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
        GameController.OnEnemyTakeDamage?.Invoke(gameObject, damage);
    }
    public override void InitHp()
    {
        currentHp = GetCharactorData().GetMaxHp();
    }
    public override void InitKnockbackGauge()
    {
        curentKnockBackGauge = GetCharactorData().GetMaxKnockbackGauge();
        maxKnockBackGauge = GetCharactorData().GetMaxKnockbackGauge();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        GameController.OnEnemyTakeDamage += KnockBackGauge;
        GameController.OnEnemyDead += GetCharactorData().Dead;
        GameController.OnEnemyTakeDamage += GetCharactorData().CheckDead;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        GameController.OnEnemyTakeDamage -= KnockBackGauge;
        GameController.OnEnemyDead -= GetCharactorData().Dead;
        GameController.OnEnemyTakeDamage -= GetCharactorData().CheckDead;
    }
    public override void KnockBackGauge(GameObject charactor, Elemental damage)
    {
        float knockbackGaugeDeal = damage.knockbackGaugeDeal;
        hited = true;
        if (curentKnockBackGauge - knockbackGaugeDeal > 0 && !isKnockback)
        {
            curentKnockBackGauge -= knockbackGaugeDeal;
        }
        else if (!isKnockback)
        {
            isKnockback = true;
            curentKnockBackGauge = 0;
            curentKnockBackGauge = maxKnockBackGauge;
            StartKnockback(damage);
        }

        if (restoreCoroutine != null) StopCoroutine(restoreCoroutine);
        restoreCoroutine = StartCoroutine(DelayRestore(restoreGaugeDelayTime));
    }
    public override void StartKnockback(Elemental damage)
    {

    }

    public override void EndKnockback()
    {

    }
}
