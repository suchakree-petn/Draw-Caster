using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHPBar_Boss : MonoBehaviour
{
    [SerializeField] private Slider fillHPBar;
    public CharactorManager<EnemyData> charactorManager;
    [SerializeField] private TextMeshProUGUI hpValueText;
    public void SetHPDefault()
    {
        float maxHp = charactorManager.GetCharactorData().GetMaxHp();
        float currentHp = charactorManager.currentHp;
        fillHPBar.maxValue = maxHp;
        fillHPBar.value = currentHp;
        hpValueText.SetText((currentHp * 100 / maxHp).ToString("F0") + "%");
    }
    public void ShowHPValue()
    {
        float maxHp = charactorManager.GetCharactorData().GetMaxHp();
        float currentHp = charactorManager.currentHp;
        fillHPBar.value = currentHp;
        hpValueText.SetText((currentHp * 100 / maxHp).ToString("F0") + "%");
    }
    private void OnEnable()
    {
        GameController.OnBeforeStart += SetHPDefault;
        GameController.WhileInGame += ShowHPValue;
    }
    private void OnDisable()
    {
        GameController.OnBeforeStart -= SetHPDefault;
        GameController.WhileInGame -= ShowHPValue;
    }
}
