using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DrawCaster.Util;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : CharactorManager<PlayerData>
{
    [SerializeField] private PlayerAction _playerAction;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip knockbackClip;
    [SerializeField] private float playerKnockbackDistance;


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
            TextDamageAsset.Instance.CreateTextDamage(transform.position, damageDeal, damage._elementalType);
        }
        GameController.OnPlayerTakeDamage?.Invoke(gameObject, damage);
    }
    public override void StartKnockback(Elemental damage){
        animator.SetTrigger("Knockback");
        StartCoroutine(DelayKnockback(knockbackClip.length));
        _playerAction.Player.Movement.Disable();
        Transform playerLowerTransform = DrawCasterUtil.GetLowerTransformOf(transform);
        Transform enemyLowerTransform = DrawCasterUtil.GetLowerTransformOf(damage.attacker.transform);
        Vector2 direction = playerLowerTransform.position - enemyLowerTransform.position;
        transform.DOMove(transform.position + (Vector3)(direction.normalized * playerKnockbackDistance), knockbackClip.length);
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
        GameController.OnPlayerDead += GetCharactorData().Dead;
        GameController.OnPlayerTakeDamage += GetCharactorData().CheckDead;
        _playerAction = PlayerInputSystem.Instance.playerAction;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        GameController.OnPlayerDead -= GetCharactorData().Dead;
        GameController.OnPlayerTakeDamage -= GetCharactorData().CheckDead;
    }

    
}
