using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUIManager : MonoBehaviour
{
    [SerializeField] private Transform BossUI;
    [SerializeField] private CharactorManager<EnemyData> charactorManager;
    private void OnEnable()
    {
        if (BossUI != null && GameObject.FindWithTag("BossUI") == null)
        {
            GameController.OnInstantiateUI += Init;
        }
    }
    private void OnDisable()
    {
        GameController.OnInstantiateUI -= Init;
    }
    private void Init()
    {
        Transform ui = Instantiate(BossUI, transform);
        ui.GetComponentInChildren<UIHPBar_Boss>().charactorManager = charactorManager;
        ui.GetComponentInChildren<StunBar_Boss>().charactorManager = charactorManager;
    }
}
