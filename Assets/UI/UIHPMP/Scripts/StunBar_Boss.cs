using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StunBar_Boss : MonoBehaviour
{
    [SerializeField] private Slider fillStunBar;
    public CharactorManager<EnemyData> charactorManager;
    public void SetStunDefault(){
        float maxStun = charactorManager.GetCharactorData().GetMaxKnockbackGauge();
        float currentStun = charactorManager.curentKnockBackGauge;
        fillStunBar.maxValue = maxStun;
        fillStunBar.value = currentStun;
    }
    public void ShowStunValue(){
        float currentStun = charactorManager.curentKnockBackGauge;
        fillStunBar.value = currentStun;
    }
    private void OnEnable() {
        GameController.OnBeforeStart += SetStunDefault;
        GameController.WhileInGame += ShowStunValue;
    }
    private void OnDisable() {
        GameController.OnBeforeStart -= SetStunDefault;
        GameController.WhileInGame -= ShowStunValue;
    }
}
