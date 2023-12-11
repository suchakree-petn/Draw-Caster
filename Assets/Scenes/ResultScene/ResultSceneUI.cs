using System;
using System.Collections.Generic;
using DrawCaster.DataPersistence;
using DrawCaster.ResultManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultSceneUI : MonoBehaviour
{
    private ResultSceneManager resultManager => ResultSceneManager.Instance;

    public Action OnUpdateRewardIcon;

    [Header("Ref Component")]
    [SerializeField] private Transform content_transform;
    [SerializeField] private GameObject rewardIcon_prf;
    [SerializeField] private TextMeshProUGUI last_play_time_text;
    [SerializeField] private TextMeshProUGUI best_play_time_text;
    [SerializeField] private Sprite gold_icon;

    private void UpdateRewardIcon()
    {
        if (resultManager == null) return;

        OnUpdateRewardIcon?.Invoke();
        Debug.Log("Update reward icon");

    }

    private void UpdateSpellIcon()
    {
        List<Spell> obtain_spell = resultManager.obtain_spells;
        for (int i = 0; i < obtain_spell.Count; i++)
        {
            SpellData spell = new(obtain_spell[i]);
            GameObject ui_GO = CreateRewardIconSpell(spell, content_transform);
            Sprite sprite = DataPersistenceManager.Instance.dataHandler.LoadSpriteFromFile(spell.spritePath);
            ui_GO.transform.GetChild(1).GetComponent<Image>().sprite = sprite;
        }
        Debug.Log("Update spell icon");

    }
    private void UpdateGoldIcon()
    {
        double obtain_gold = resultManager.obtain_gold;
        if (obtain_gold <= 0) return;

        GameObject ui_GO = CreateRewardIconGold(obtain_gold, content_transform);

        ui_GO.transform.GetChild(1).GetComponent<Image>().sprite = gold_icon;

        Debug.Log("Update gold icon");


    }

    private GameObject CreateRewardIconSpell(SpellData reward, Transform parent)
    {
        GameObject go = Instantiate(rewardIcon_prf, parent);
        RewardUIData uiData = go.GetComponent<RewardUIData>();
        if (uiData == null)
        {
            go.AddComponent(typeof(RewardUIData));
        }
        uiData.spell = reward;
        return go;
    }
    private GameObject CreateRewardIconGold(double reward, Transform parent)
    {
        GameObject go = Instantiate(rewardIcon_prf, parent);
        RewardUIData uiData = go.GetComponent<RewardUIData>();
        if (uiData == null)
        {
            go.AddComponent(typeof(RewardUIData));
        }
        uiData.gold = reward;
        go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = reward.ToString();
        return go;
    }

    private void UpdatePlayTime()
    {
        float best_play_time = resultManager.best_play_time;
        TimeSpan best_play_time_span = TimeSpan.FromSeconds(best_play_time);
        best_play_time_text.text = string.Format("{0:D2}:{1:D2}", best_play_time_span.Minutes, best_play_time_span.Seconds);

        float last_play_time = resultManager.last_play_time;
        TimeSpan last_play_time_span = TimeSpan.FromSeconds(last_play_time);
        last_play_time_text.text = string.Format("{0:D2}:{1:D2}", last_play_time_span.Minutes, last_play_time_span.Seconds);
    }

    private void OnEnable()
    {
        ResultSceneManager.OnInitRewardSuccess += UpdateRewardIcon;
        OnUpdateRewardIcon += UpdateSpellIcon;
        OnUpdateRewardIcon += UpdateGoldIcon;

        ResultSceneManager.OnInitPlayTimeSuccess += UpdatePlayTime;

    }
    private void OnDisable()
    {
        ResultSceneManager.OnInitRewardSuccess -= UpdateRewardIcon;
        OnUpdateRewardIcon -= UpdateSpellIcon;
        OnUpdateRewardIcon -= UpdateGoldIcon;

        ResultSceneManager.OnInitPlayTimeSuccess -= UpdatePlayTime;

    }
}