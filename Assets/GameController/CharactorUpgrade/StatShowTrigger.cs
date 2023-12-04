using System;
using System.Collections;
using System.Collections.Generic;
using DrawCaster.DataPersistence;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatShowTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject stat_obj;
    [SerializeField] private List<TextMeshProUGUI> stat_ui;
    private List<string> stat_text = new();
    [SerializeField] private bool isDataLoaded = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isDataLoaded)
        {
            InitStat();
            stat_obj.SetActive(true);
        }
        else
        {
            Debug.Log("Data was not loaded");
        }

    }

    private void InitStat()
    {
        PlayerStat player_stats = CharactorUpgradeManager.Instance.playerStat;
        stat_ui[0].text = stat_text[0] + player_stats._level.ToString();
        stat_ui[1].text = stat_text[1] + player_stats._hpBase.ToString();
        stat_ui[2].text = stat_text[2] + player_stats._attackBase.ToString();
        stat_ui[3].text = stat_text[3] + player_stats._defenseBase.ToString();
        stat_ui[4].text = stat_text[4] + player_stats._manaBase.ToString();
        stat_ui[5].text = stat_text[5] + player_stats._moveSpeed.ToString();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (stat_obj == null) return;

        stat_obj.SetActive(false);
    }

    private void CheckLoadData()
    {
        isDataLoaded = DataPersistenceManager.Instance.IsLoaded;

        if (isDataLoaded)
        {
            for (int i = 0; i < stat_ui.Count; i++)
            {
                stat_text.Add(stat_ui[i].text);
            }
        }
    }

    private void OnEnable()
    {
        DataPersistenceManager.OnLoadSuccess += CheckLoadData;
    }

    private void OnDisable()
    {
        DataPersistenceManager.OnLoadSuccess -= CheckLoadData;
    }
}
