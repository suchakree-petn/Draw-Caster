using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMPBar : MonoBehaviour
{
    [SerializeField] private Slider fillMPBar;
    [SerializeField] private CharactorManager<PlayerData> charactorManager;
    [SerializeField] private TextMeshProUGUI mpValueText;
    public void SetMPDefault(){
        charactorManager = GameObject.Find("Player").GetComponent<CharactorManager<PlayerData>>();
        int maxMp = (int)charactorManager.GetCharactorData().GetMaxMana();
        int currentMp = (int)charactorManager.currentMana;
        fillMPBar.maxValue = maxMp;
        fillMPBar.value = currentMp;
        mpValueText.SetText(currentMp + " / " + maxMp);
    }
    public void ShowMPValue(){
        int maxMp = (int)charactorManager.GetCharactorData().GetMaxMana();
        int currentMp = (int)charactorManager.currentMana;
        fillMPBar.value = currentMp;
        mpValueText.SetText(currentMp + " / " + maxMp);
    }
    private void OnEnable() {
        GameController.OnBeforeStart += SetMPDefault;
        GameController.WhileInGame += ShowMPValue;
    }
    private void OnDisable() {
        GameController.OnBeforeStart -= SetMPDefault;
        GameController.WhileInGame -= ShowMPValue;
    }
}
