using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCooldown : MonoBehaviour
{
    [Header("spellQ")]
    [SerializeField] private SpellHolder_Q spellQ;
    [SerializeField] private Image iconQ;


    [Header("spellE")]
    [SerializeField] private SpellHolder_E spellE;
    [SerializeField] private Image iconE;


    [Header("spellR")]
    [SerializeField] private SpellHolder_R spellR;
    [SerializeField] private Image iconR;


    [Header("spellShift")]
    [SerializeField] private SpellHolder_Shift spellShift;
    [SerializeField] private Image iconShift;

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
        if (spellQ.spell._icon != null) iconQ.sprite = spellQ.spell._icon;
        if (spellE.spell._icon != null) iconE.sprite = spellE.spell._icon;
        if (spellR.spell._icon != null) iconR.sprite = spellR.spell._icon;
        if (spellShift.spell._icon != null) iconShift.sprite = spellShift.spell._icon;
    }
    void SpellQCooldown()
    {
        iconQ.fillAmount = 1 - spellQ.cooldown / spellQ.spell._cooldown;
    }
    void SpellECooldown()
    {
        iconE.fillAmount = 1 - spellE.cooldown / spellE.spell._cooldown;
    }
    void SpellRCooldown()
    {
        iconR.fillAmount = 1 - spellR.cooldown / spellR.spell._cooldown;
    }
    void SpellShiftCooldown()
    {
        iconShift.fillAmount = 1 - spellShift.cooldown / spellShift.spell._cooldown;
    }
}
