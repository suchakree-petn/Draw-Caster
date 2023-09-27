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
    private Coroutine restoreCoroutine;

    public abstract void TakeDamage(Elemental elementalDamage);
    public void KnockBackGauge(GameObject attacker, float damage){
        damagaGauge = GetDealKnockBackGauge(attacker);
        hited = true;
        if(curentKnockBackGauge - damagaGauge > 0){
            curentKnockBackGauge -= damagaGauge;
        }else{
            
        }

        if(restoreCoroutine != null){
            StopCoroutine(restoreCoroutine);
        }
        restoreCoroutine = StartCoroutine(DelayRestore(restoreGaugeDelayTime));
    }
    public float GetDealKnockBackGauge(GameObject attacker){
        EnemyData attackData = attacker.GetComponent<EnemyData>();
        return 0;
    }
    void RestoreGauge(){
        maxKnockBackGauge = ReturnMaxKnockBackGauge();
        if(!hited && curentKnockBackGauge < maxKnockBackGauge){
            curentKnockBackGauge += gaugeRegen * Time.deltaTime;
        }else if(!hited && curentKnockBackGauge >= maxKnockBackGauge){
            curentKnockBackGauge = maxKnockBackGauge;
        }
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
        GameController.WhileInGame += RestoreGauge;
        GameController.OnPlayerTakeDamage += KnockBackGauge;

    }
    protected virtual void OnDisable()
    {
        GameController.OnBeforeStart -= InitHp;
        GameController.OnBeforeStart -= InitMana;
        GameController.OnBeforeStart -= InitKnockbackGauge;
        GameController.WhileInGame -= RestoreGauge;
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
