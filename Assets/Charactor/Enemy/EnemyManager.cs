using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : CharactorManager<EnemyData>
{
    public EnemyData enemyData;
    [Header("Knockback Setting")]
    public float enemyKnockbackDistance;
    public AnimationClip enemyKnockbackClip;
    public Action<Elemental> OnStartKnockback;
    public Action OnEndKnockback;
    [Header("Dead Setting")]
    public Action<GameObject> OnEnemyDead;
    public Action<GameObject, Elemental> OnEnemyTakeDamage;
    public AnimationClip enemyDeadClip;
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
        OnEnemyTakeDamage?.Invoke(gameObject, damage);
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
        OnEnemyTakeDamage += KnockBackGauge;
        OnEnemyTakeDamage += CheckDead;
        OnEnemyDead += Dead;

    }


    protected override void OnDisable()
    {
        base.OnDisable();
        OnEnemyTakeDamage -= KnockBackGauge;
        OnEnemyTakeDamage -= CheckDead;
        OnEnemyDead -= Dead;

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
        OnStartKnockback?.Invoke(damage);
    }

    public override void EndKnockback()
    {
        OnEndKnockback?.Invoke();
    }
    public override void Dead(GameObject deadCharactor)
    {
        GameController.Instance.RemoveEnemyDead(deadCharactor);
    }
    public override void CheckDead(GameObject charactor, Elemental damage)
    {
        //Debug.Log("CheckDead");
        if (charactor.GetComponent<EnemyManager>().currentHp <= 0)
        {
            OnEnemyDead?.Invoke(charactor);
        }
    }
}
