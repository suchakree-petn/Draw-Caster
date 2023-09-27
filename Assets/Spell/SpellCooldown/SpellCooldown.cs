using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCooldown : MonoBehaviour
{
    [Header("spellQ")]
    [SerializeField] private SpellHolder_Q spellQ;
    [SerializeField] private Image iconQ;
    private float cooldownQ => spellQ.cooldown;
    private bool isReadyToCastQ => spellQ._isReadyToCast;
    private bool isCooldownQ = false;


    [Header("spellE")]
    [SerializeField] private SpellHolder_E spellE;
    [SerializeField] private Image iconE;
    private float cooldownE => spellE.cooldown;
    private bool isReadyToCastE => spellE._isReadyToCast;
    private bool isCooldownE = false;


    [Header("spellR")]
    [SerializeField] private SpellHolder_R spellR;
    [SerializeField] private Image iconR;
    private float cooldownR => spellR.cooldown;
    private bool isReadyToCastR => spellR._isReadyToCast;
    private bool isCooldownR = false;


    [Header("spellShift")]
    [SerializeField] private SpellHolder_Shift spellShift;
    [SerializeField] private Image iconShift;
    private float cooldownShift => spellShift.cooldown;
    private bool isReadyToCastShift => spellShift._isReadyToCast;
    private bool isCooldownShift = false;

    void OnEnable()
    {
        GameController.OnBeforeStart += SetUpDefault;
        GameController.WhileInGame += SpellQCooldown;
        GameController.WhileInGame += SpellECooldown;
        GameController.WhileInGame += SpellRCooldown;
        GameController.WhileInGame += SpellShiftCooldown;
    }
    private void OnDisable()
    {
        GameController.OnBeforeStart -= SetUpDefault;
        GameController.WhileInGame -= SpellQCooldown;
        GameController.WhileInGame -= SpellECooldown;
        GameController.WhileInGame -= SpellRCooldown;
        GameController.WhileInGame -= SpellShiftCooldown;
    }
    void SetUpDefault()
    {
        // spellQ = GameObject.Find("SpellHolder_Q").GetComponent<SpellHolder_Q>();
        // spellE = GameObject.Find("SpellHolder_E").GetComponent<SpellHolder_E>();
        // spellR = GameObject.Find("SpellHolder_R").GetComponent<SpellHolder_R>();
        // spellShift = GameObject.Find("SpellHolder_Shift").GetComponent<SpellHolder_Shift>();
        iconQ.fillAmount = 1;
        iconE.fillAmount = 1;
        iconR.fillAmount = 1;
        iconShift.fillAmount = 1;
        if (spellQ.spell._icon != null) iconQ.sprite = spellQ.spell._icon;
        if (spellE.spell._icon != null) iconE.sprite = spellE.spell._icon;
        if (spellR.spell._icon != null) iconR.sprite = spellR.spell._icon;
        if (spellShift.spell._icon != null) iconShift.sprite = spellShift.spell._icon;
    }
    void SpellQCooldown()
    {
        // if(!isReadyToCastQ && !isCooldownQ){
        //     iconQ.fillAmount = 0;
        //     isCooldownQ = true;

        // }
        // if(isCooldownQ){
        //     iconQ.fillAmount += 1 / cooldownQ * Time.deltaTime;

        //     if(iconQ.fillAmount >= 1){
        //         iconQ.fillAmount = 1;
        //         isCooldownQ = false;
        //     }
        // }
        iconQ.fillAmount = 1 - spellQ.cooldown / spellQ.spell._cooldown;
    }
    void SpellECooldown()
    {
        // if(!isReadyToCastE && !isCooldownE){
        //     iconE.fillAmount = 0;
        //     isCooldownE = true;

        // }
        // if(isCooldownE){
        //     iconE.fillAmount += 1 / cooldownE * Time.deltaTime;

        //     if(iconE.fillAmount >= 1){
        //         iconE.fillAmount = 1;
        //         isCooldownE = false;
        //     }
        // }
        iconE.fillAmount = 1 - spellE.cooldown / spellE.spell._cooldown;
    }
    void SpellRCooldown()
    {
        // if(!isReadyToCastR && !isCooldownR){
        //     iconR.fillAmount = 0;
        //     isCooldownR = true;

        // }
        // if(isCooldownR){
        //     iconR.fillAmount += 1 / cooldownR * Time.deltaTime;

        //     if(iconR.fillAmount >= 1){
        //         iconR.fillAmount = 1;
        //         isCooldownR = false;
        //     }
        // }
        iconR.fillAmount = 1 - spellR.cooldown / spellR.spell._cooldown;
    }
    void SpellShiftCooldown()
    {
        // if(!isReadyToCastShift && !isCooldownShift){
        //     iconShift.fillAmount = 0;
        //     isCooldownShift = true;

        // }
        // if(isCooldownShift){
        //     iconShift.fillAmount += 1 / cooldownShift * Time.deltaTime;

        //     if(iconShift.fillAmount >= 1){
        //         iconShift.fillAmount = 1;
        //         isCooldownShift = false;
        //     }
        // }
        iconShift.fillAmount = 1 - spellShift.cooldown / spellShift.spell._cooldown;
    }
}
