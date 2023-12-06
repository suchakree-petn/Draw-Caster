using System;
using System.Collections.Generic;
using DrawCaster.DataPersistence;
using DrawCaster.ResultManager;
using UnityEngine;
using UnityEngine.UI;

public class ResultSceneUI : MonoBehaviour
{
    private ResultSceneManager resultManager => ResultSceneManager.Instance;

    public Action OnUpdateRewardIcon;

    [Header("Ref Component")]
    [SerializeField] private Transform content_transform;
    [SerializeField] private GameObject rewardIcon_prf;
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
            ui_GO.GetComponent<Image>().sprite = sprite;
        }
        Debug.Log("Update spell icon");

    }
    private void UpdateGoldIcon()
    {
        double obtain_gold = resultManager.obtain_gold;
        if (obtain_gold <= 0) return;
        
        GameObject ui_GO = CreateRewardIconGold(obtain_gold, content_transform);

        // To do, Show gold Icon
        // Sprite sprite = DataPersistenceManager.Instance.dataHandler.LoadSpriteFromFile("Gold/GoldIcon.png");
        // ui_GO.GetComponent<Image>().sprite = sprite;
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
        return go;
    }

    private void OnEnable()
    {
        ResultSceneManager.OnInitRewardSuccess += UpdateRewardIcon;
        OnUpdateRewardIcon += UpdateSpellIcon;
        OnUpdateRewardIcon += UpdateGoldIcon;
    }
    private void OnDisable()
    {
        ResultSceneManager.OnInitRewardSuccess -= UpdateRewardIcon;
        OnUpdateRewardIcon -= UpdateSpellIcon;
        OnUpdateRewardIcon -= UpdateGoldIcon;
    }
}