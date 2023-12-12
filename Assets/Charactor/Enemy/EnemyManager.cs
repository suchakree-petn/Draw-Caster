using System;
using System.Collections;
using System.Collections.Generic;
using DrawCaster.Util;
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
            EnemyData enemyData = GetCharactorData();
            damageDeal = CalcDamageRecieve(enemyData, damage);
            if (enemyData.elementalResistant == damage._elementalType) damageDeal = damageDeal * 0.7f;
            currentHp -= damageDeal;
            TextDamageAsset.Instance.CreateTextDamage(DrawCasterUtil.GetMidTransformOf(transform).position, damageDeal, damage._elementalType);
        }
        OnEnemyTakeDamage?.Invoke(gameObject, damage);
    }
    public override void InitHp()
    {
        currentHp = GetCharactorData().GetMaxHp();
    }
    public override void InitKnockbackGauge()
    {
        maxKnockBackGauge = GetCharactorData().GetMaxKnockbackGauge();
        curentKnockBackGauge = maxKnockBackGauge;
    }

    private void AddRewardToRewardTracker(GameObject enemy)
    {
        RewardTrackerManager.Instance.AddGoldObtain(enemyData.goldDrop);
        RewardTrackerManager.Instance.AddSpellObtain(enemyData.spellDrop);
    }
    private void SetEnemyStat()
    {
        EnemyData enemyData = GetCharactorData();

        enemyData._attackBase = enemyData.enemyLGData.lgAtk * enemyData._level + enemyData.enemyLGData.lgBaseAtk;
        enemyData._defenseBase = enemyData.enemyLGData.lgDef * enemyData._level + enemyData.enemyLGData.lgBaseDef;
        if (enemyData._level == 0)
        {
            enemyData._hpBase = 150;
        }else if(enemyData._level == 1){
            enemyData._hpBase = 300;
        }
        else
        {
            enemyData._hpBase = enemyData.enemyLGData.lgHp * enemyData._level + enemyData.enemyLGData.lgBaseHp;
        }
        enemyData._knockbackBase = enemyData.enemyLGData.lgKnckB * enemyData._level + enemyData.enemyLGData.lgBaseKnckB;
        enemyData.goldDrop = enemyData.enemyLGData.lgGold * enemyData._level + enemyData.enemyLGData.lgBaseGold;

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        GameController.OnStart += InitHp;

        OnEnemyTakeDamage += KnockBackGauge;
        OnEnemyTakeDamage += CheckDead;
        OnEnemyDead += Dead;
        OnEnemyDead += AddRewardToRewardTracker;
        GameController.OnBeforeStart += SetEnemyStat;
    }


    protected override void OnDisable()
    {
        base.OnDisable();
        GameController.OnStart -= InitHp;

        OnEnemyTakeDamage -= KnockBackGauge;
        OnEnemyTakeDamage -= CheckDead;
        OnEnemyDead -= Dead;
        OnEnemyDead -= AddRewardToRewardTracker;
        GameController.OnBeforeStart -= SetEnemyStat;
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
