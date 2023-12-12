using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharactorManager<T> : MonoBehaviour, IDamageable
{
    public float currentHp;
    public float currentMana;

    public T charactorData { private get; set; }
    public float maxKnockBackGauge;
    public float curentKnockBackGauge;
    public float restoreGaugeDelayTime;
    [SerializeField] private float gaugeRegen;
    public bool hited = false;
    public bool isKnockback;
    public Coroutine restoreCoroutine;

    private void Awake()
    {
        // InitHp();
        // InitMana();
    }
    public abstract void TakeDamage(Elemental elementalDamage);
    public abstract void StartKnockback(Elemental damage);
    public abstract void EndKnockback();
    public abstract void KnockBackGauge(GameObject charactor, Elemental damage);
    public void RestoreKnockbackGauge()
    {
        if (!hited && curentKnockBackGauge < maxKnockBackGauge)
        {
            curentKnockBackGauge += gaugeRegen * Time.deltaTime;
        }
        else if (!hited && curentKnockBackGauge >= maxKnockBackGauge)
        {
            curentKnockBackGauge = maxKnockBackGauge;
        }
    }
    public IEnumerator DelayKnockback(float time)
    {
        yield return new WaitForSeconds(time);
        EndKnockback();
        isKnockback = false;
    }
    public IEnumerator DelayRestore(float time)
    {
        yield return new WaitForSeconds(time);
        hited = false;
    }

    public abstract T GetCharactorData();
    protected virtual void OnEnable()
    {
        GameController.OnStart += InitHp;
        GameController.OnStart += InitMana;
        GameController.OnStart += InitKnockbackGauge;
        GameController.WhileInGame += RestoreKnockbackGauge;

    }
    protected virtual void OnDisable()
    {
        GameController.OnStart -= InitKnockbackGauge;
        GameController.WhileInGame -= RestoreKnockbackGauge;
    }

    float CalcDefense(CharactorData target)
    {
        return (target._defenseBase * (1 + target._defenseMultiplier)) + target._defenseBonus;
    }

    float CalcDMGReduction(CharactorData target, Elemental damage)
    {
        float _targetDefense = CalcDefense(target);
        return 1 - (_targetDefense / (_targetDefense + (5 * damage._attackerData._level) + 500));
    }
    public float CalcDamageRecieve(CharactorData target, Elemental damage)
    {
        return damage._damage * CalcDMGReduction(target, damage) * (1 - target._bonusDamageReduction);
    }
    public virtual void InitHp() { }
    public virtual void InitMana() { }
    public virtual void InitKnockbackGauge() { }

    public abstract void Dead(GameObject deadCharactor);
    public abstract void CheckDead(GameObject charactor, Elemental damage);
}
