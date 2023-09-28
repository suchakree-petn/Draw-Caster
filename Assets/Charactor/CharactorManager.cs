using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharactorManager<T> : MonoBehaviour, IDamageable
{
    public float currentHp;
    public float currentMana;
    public T charactorData {private get; set; }

    public float curentKnockBackGauge;
    [SerializeField] private float maxKnockBackGauge;
    [SerializeField] private float damagaGauge;
    [SerializeField] private float restoreGaugeDelayTime;
    [SerializeField] private float gaugeRegen;
    private bool hited = false;
    public bool isKnockback;
    private Coroutine restoreCoroutine;

    public abstract void TakeDamage(Elemental elementalDamage);
    public abstract void StartKnockback();
    public abstract void EndKnockback();
    public void KnockBackGauge(GameObject charactor, Elemental damage){
        maxKnockBackGauge = ReturnMaxKnockBackGauge();
        damagaGauge = damage.knockbackGaugeDeal;
        hited = true;
        if(curentKnockBackGauge - damagaGauge > 0 && !isKnockback){
            curentKnockBackGauge -= damagaGauge;
        }else if(!isKnockback){
            isKnockback = true;
            curentKnockBackGauge = 0;
            curentKnockBackGauge = maxKnockBackGauge;
            StartKnockback();
        }

        if(restoreCoroutine != null)StopCoroutine(restoreCoroutine);
        restoreCoroutine = StartCoroutine(DelayRestore(restoreGaugeDelayTime));
    }
    public void RestoreKnockbackGauge(){
        maxKnockBackGauge = ReturnMaxKnockBackGauge();
        if(!hited && curentKnockBackGauge < maxKnockBackGauge){
            curentKnockBackGauge += gaugeRegen * Time.deltaTime;
        }else if(!hited && curentKnockBackGauge >= maxKnockBackGauge){
            curentKnockBackGauge = maxKnockBackGauge;
        }
    }
    public IEnumerator DelayKnockback(float time){
        yield return new WaitForSeconds(time);
        EndKnockback();
        isKnockback = false;
    }
    IEnumerator DelayRestore(float time){
        yield return new WaitForSeconds(time);
        hited = false;
    }

    public abstract T GetCharactorData();
    protected virtual void OnEnable()
    {
        GameController.OnBeforeStart += InitHp;
        GameController.OnBeforeStart += InitMana;
        GameController.OnBeforeStart += InitKnockbackGauge;
        GameController.WhileInGame += RestoreKnockbackGauge;
        GameController.OnPlayerTakeDamage += KnockBackGauge;

    }
    protected virtual void OnDisable()
    {
        GameController.OnBeforeStart -= InitHp;
        GameController.OnBeforeStart -= InitMana;
        GameController.OnBeforeStart -= InitKnockbackGauge;
        GameController.WhileInGame -= RestoreKnockbackGauge;
        GameController.OnPlayerTakeDamage -= KnockBackGauge;
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
    public float CalcDamageRecieve(CharactorData target, Elemental damage)
    {
        return damage._damage * CalcDMGReduction(target, damage) * (1 - target._bonusDamageReduction);
    }
    public virtual void InitHp(){}
    public virtual void InitMana(){}
    public virtual void InitKnockbackGauge(){}
    public virtual float ReturnMaxKnockBackGauge(){return 0;}
}
