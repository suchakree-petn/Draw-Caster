using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHPBar : MonoBehaviour
{
    [SerializeField] private Slider fillHPBar;
    [SerializeField] private CharactorManager<PlayerData> charactorManager;
    [SerializeField] private TextMeshProUGUI hpValueText;
    public void SetHPDefault(){
        charactorManager = GameObject.Find("Player").GetComponent<CharactorManager<PlayerData>>();
        int maxHp = (int)charactorManager.GetCharactorData().GetMaxHp();
        int currentHp = (int)charactorManager.currentHp;
        fillHPBar.maxValue = maxHp;
        fillHPBar.value = currentHp;
        hpValueText.SetText(currentHp + " / " + maxHp);
    }
    public void ShowHPValue(){
        int maxHp = (int)charactorManager.GetCharactorData().GetMaxHp();
        int currentHp = (int)charactorManager.currentHp;
        fillHPBar.value = currentHp;
        hpValueText.SetText(currentHp + " / " + maxHp);
    }
    private void OnEnable() {
        GameController.OnBeforeStart += SetHPDefault;
        GameController.WhileInGame += ShowHPValue;
    }
    private void OnDisable() {
        GameController.OnBeforeStart -= SetHPDefault;
        GameController.WhileInGame -= ShowHPValue;
    }
}
