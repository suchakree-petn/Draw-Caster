using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUIManager : MonoBehaviour
{
    [SerializeField] private Transform BossUIPrefab;
    [SerializeField] private Transform BossUI;
    [SerializeField] private CharactorManager<EnemyData> charactorManager;
    private void OnEnable()
    {
        if (BossUIPrefab != null && BossUI == null)
        {
            Debug.LogWarning("Init UI");
            GameController.OnInstantiateUI += Init;
        }
    }
    private void OnDisable()
    {
        GameController.OnInstantiateUI -= Init;
        if (BossUI != null)
        {
            Destroy(BossUI.gameObject);
        }
    }
    private void Init()
    {
        BossUI = Instantiate(BossUIPrefab, transform);
        BossUI.GetComponentInChildren<UIHPBar_Boss>().charactorManager = charactorManager;
        BossUI.GetComponentInChildren<StunBar_Boss>().charactorManager = charactorManager;
    }
}
