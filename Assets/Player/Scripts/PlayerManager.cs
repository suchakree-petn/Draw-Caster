using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour, IDamageable
{
    public PlayerData playerData;
    public float currentHp;
    [SerializeField] private PlayerAction _playerAction;

    private void Start()
    {
        currentHp = playerData._maxHp;
    }
    public void TakeDamage(Elemental damage)
    {
        if (currentHp > 0)
        {
            float damageDeal = CalcDamageRecieve(playerData, damage);
            currentHp -= damageDeal;
            DamagePopup.CreateTextDamage(transform.position, damageDeal, damage._elementalType);
        }
        GameController.OnPlayerTakeDamage?.Invoke(gameObject);
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
        currentHp = playerData._maxHp;
    }

    private void OnEnable()
    {
        GameController.OnBeforeStart += CalcMaxHp;
    }
    private void OnDisable()
    {
        GameController.OnBeforeStart -= CalcMaxHp;
    }
}
