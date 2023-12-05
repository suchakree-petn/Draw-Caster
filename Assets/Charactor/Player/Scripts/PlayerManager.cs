using System;
using DG.Tweening;
using DrawCaster.DataPersistence;
using DrawCaster.Util;
using UnityEngine;

public class PlayerManager : CharactorManager<PlayerData>, IDataPersistence
{
    [SerializeField] private PlayerAction _playerAction;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip knockbackClip;
    [SerializeField] private float playerKnockbackDistance;
    [SerializeField] private Rigidbody2D playerRB;
    public Action OnPlayerKnockback;

    public override PlayerData GetCharactorData()
    {
        return playerData;
    }
    public override void TakeDamage(Elemental damage)
    {
        float damageDeal = 0;
        if (currentHp > 0)
        {
            damageDeal = CalcDamageRecieve(GetCharactorData(), damage);
            currentHp -= damageDeal;
            TextDamageAsset.Instance.CreateTextDamage(DrawCasterUtil.GetMidTransformOf(transform).position, damageDeal, damage._elementalType);
        }
        GameController.OnPlayerTakeDamage?.Invoke(gameObject, damage);
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
        OnPlayerKnockback?.Invoke();
        animator.SetTrigger("Knockback");
        StartCoroutine(DelayKnockback(knockbackClip.length));
        _playerAction.Player.Movement.Disable();
        Transform playerLowerTransform = DrawCasterUtil.GetLowerTransformOf(transform);
        Transform enemyLowerTransform = DrawCasterUtil.GetLowerTransformOf(damage.attacker.transform);
        Vector2 direction = playerLowerTransform.position - enemyLowerTransform.position;
        transform.DOMove(playerRB.position + (direction.normalized * playerKnockbackDistance), knockbackClip.length);
    }
    public override void EndKnockback()
    {
        _playerAction.Player.Movement.Enable();

    }
    public override void InitHp()
    {
        currentHp = GetCharactorData().GetMaxHp();
    }
    public override void InitMana()
    {
        currentMana = GetCharactorData().GetMaxMana();
    }
    public override void InitKnockbackGauge()
    {
        curentKnockBackGauge = GetCharactorData().GetMaxKnockbackGauge();
        maxKnockBackGauge = GetCharactorData().GetMaxKnockbackGauge();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        GameController.OnPlayerTakeDamage += KnockBackGauge;
        GameController.OnPlayerDead += Dead;
        GameController.OnPlayerTakeDamage += CheckDead;
        _playerAction = PlayerInputSystem.Instance.playerAction;

    }
    protected override void OnDisable()
    {
        base.OnDisable();
        GameController.OnPlayerTakeDamage -= KnockBackGauge;
        GameController.OnPlayerDead -= Dead;
        GameController.OnPlayerTakeDamage -= CheckDead;
    }
    public override void CheckDead(GameObject charactor, Elemental damage)
    {
        if (charactor.GetComponent<PlayerManager>().currentHp <= 0)
        {
            GameController.OnPlayerDead?.Invoke(charactor);
        }
    }

    public override void Dead(GameObject deadCharactor)
    {
        Destroy(deadCharactor);
        GameController.Instance.ToResultScene();
    }
    public void GainMana(float amount)
    {
        float maxMana = GetCharactorData().GetMaxMana();
        if (currentMana + amount > maxMana)
        {
            currentMana = maxMana;
        }
        else
        {
            currentMana += amount;
        }
    }

    public void LoadData(GameData data)
    {
        this.playerData._level = data.playerStat._level;
        this.playerData._moveSpeed = data.playerStat._moveSpeed;
        this.playerData._hpBase = data.playerStat._hpBase;
        this.playerData._manaBase = data.playerStat._manaBase;
        this.playerData._attackBase = data.playerStat._attackBase;
        this.playerData._defenseBase = data.playerStat._defenseBase;
    }

    public void SaveData(ref GameData data)
    {
    }
}
