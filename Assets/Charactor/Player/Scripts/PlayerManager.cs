using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : CharactorManager<PlayerData>
{
    [SerializeField] private PlayerAction _playerAction;
    [SerializeField] private PlayerData playerData;

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
        GameController.OnPlayerTakeDamage?.Invoke(gameObject, damageDeal);
    }
    public override void InitHp()
    {
        currentHp = GetCharactorData().GetMaxHp();
    }
    public override void InitMana()
    {
        currentMana = GetCharactorData().GetMaxMana();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        GameController.OnPlayerDead += GetCharactorData().Dead;
        GameController.OnPlayerTakeDamage += GetCharactorData().CheckDead;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        GameController.OnPlayerDead -= GetCharactorData().Dead;
        GameController.OnPlayerTakeDamage -= GetCharactorData().CheckDead;
    }

    
}
