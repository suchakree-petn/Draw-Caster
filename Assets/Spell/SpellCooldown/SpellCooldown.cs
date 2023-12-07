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
        spellQ = SpellHolder_Q.Instance;
        spellE = SpellHolder_E.Instance;
        spellR = SpellHolder_R.Instance;
        spellShift = SpellHolder_Shift.Instance;
        if (spellQ.spell != null)
            if (spellQ.spell._icon != null) iconQ.sprite = spellQ.spell._icon;

        if (spellE.spell != null)
            if (spellE.spell._icon != null) iconE.sprite = spellE.spell._icon;

        if (spellR.spell != null)
            if (spellR.spell._icon != null) iconR.sprite = spellR.spell._icon;

        if (spellShift.spell != null)
            if (spellShift.spell._icon != null) iconShift.sprite = spellShift.spell._icon;
    }
    void SpellQCooldown()
    {
        if (spellQ.spell != null)
            iconQ.fillAmount = 1 - spellQ.cooldown / spellQ.spell._cooldown;
    }
    void SpellECooldown()
    {
        if (spellE.spell != null)
            iconE.fillAmount = 1 - spellE.cooldown / spellE.spell._cooldown;
    }
    void SpellRCooldown()
    {
        if (spellR.spell != null)
            iconR.fillAmount = 1 - spellR.cooldown / spellR.spell._cooldown;
    }
    void SpellShiftCooldown()
    {
        if (spellShift.spell != null)
            iconShift.fillAmount = 1 - spellShift.cooldown / spellShift.spell._cooldown;
    }
}
