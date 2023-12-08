using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellCooldown : MonoBehaviour
{
    [Header("spellQ")]
    [SerializeField] private SpellHolder_Q spellQ;
    [SerializeField] private TextMeshProUGUI textQ;
    [SerializeField] private Image iconQ;
    [SerializeField] private Image bgQ;


    [Header("spellE")]
    [SerializeField] private SpellHolder_E spellE;
    [SerializeField] private TextMeshProUGUI textE;
    [SerializeField] private Image iconE;
    [SerializeField] private Image bgE;


    [Header("spellR")]
    [SerializeField] private SpellHolder_R spellR;
    [SerializeField] private TextMeshProUGUI textR;
    [SerializeField] private Image iconR;
    [SerializeField] private Image bgR;


    [Header("spellShift")]
    [SerializeField] private SpellHolder_Shift spellShift;
    [SerializeField] private TextMeshProUGUI textShift;
    [SerializeField] private Image iconShift;
    [SerializeField] private Image bgShift;

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
        iconQ.fillAmount = 1;
        iconE.fillAmount = 1;
        iconR.fillAmount = 1;
        iconShift.fillAmount = 1;
        spellQ = SpellHolder_Q.Instance;
        spellE = SpellHolder_E.Instance;
        spellR = SpellHolder_R.Instance;
        spellShift = SpellHolder_Shift.Instance;
        if (spellQ.spell != null)
            if (spellQ.spell._icon != null) {
                iconQ.sprite = spellQ.spell._icon;
                bgQ.sprite = spellQ.spell._icon;
            }
        if (spellE.spell != null)
            if (spellE.spell._icon != null){
                iconE.sprite = spellE.spell._icon;
                bgE.sprite = spellE.spell._icon;
            }
                

        if (spellR.spell != null)
            if (spellR.spell._icon != null) {
                iconR.sprite = spellR.spell._icon;
                bgR.sprite = spellR.spell._icon;
            }

        if (spellShift.spell != null)
            if (spellShift.spell._icon != null) {
                iconShift.sprite = spellShift.spell._icon;
                bgShift.sprite = spellShift.spell._icon;
            }
    }
    void SpellQCooldown()
    {
        
        if (spellQ.spell != null){
            iconQ.fillAmount = 1 - spellQ.cooldown / spellQ.spell._cooldown;
            ShowTextCooldownQ();
        }
    }
    void SpellECooldown()
    {
        if (spellE.spell != null){
            iconE.fillAmount = 1 - spellE.cooldown / spellE.spell._cooldown;
            ShowTextCooldownE();
        }
    }
    void SpellRCooldown()
    {
        if (spellR.spell != null){
            iconR.fillAmount = 1 - spellR.cooldown / spellR.spell._cooldown;
            ShowTextCooldownR();
        }
    }
    void SpellShiftCooldown()
    {
        if (spellShift.spell != null){
            iconShift.fillAmount = 1 - spellShift.cooldown / spellShift.spell._cooldown;
            ShowTextCooldownShift();
        }
    }
    void ShowTextCooldownQ(){
        if(spellQ.cooldown >= 1 && !textQ.enabled){
            textQ.enabled = true;
        }else if(textQ.text == "0"){
            textQ.enabled = false;
        }
        textQ.text =  spellQ.cooldown.ToString("0");
    }
    void ShowTextCooldownE(){
        if(spellE.cooldown >= 1 && !textE.enabled){
            textE.enabled = true;
        }else if(textE.text == "0"){
            textE.enabled = false;
        }
        textE.text =  spellE.cooldown.ToString("0");
    }
    void ShowTextCooldownR(){
        if(spellR.cooldown >= 1 && !textR.enabled){
            textR.enabled = true;
        }else if(textR.text == "0"){
            textR.enabled = false;
        }
        textR.text =  spellR.cooldown.ToString("0");
    }
    void ShowTextCooldownShift(){
        if(spellShift.cooldown >= 1 && !textShift.enabled){
            textShift.enabled = true;
        }else if(textShift.text == "0"){
            textShift.enabled = false;
        }
        textShift.text =  spellShift.cooldown.ToString("0");
    }
}
