using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DrawCaster.DataPersistence;
using UnityEngine;

public class RewardTrackerManager : Singleton<RewardTrackerManager>, IDataPersistence
{
    [Header("Reward")]
    public double obtain_gold;
    public List<Spell> obtain_spell = new();

    [Header("Dimension detail")]
    public string dimension_id;

    [Header("Play time")]
    public float last_play_time;
    public float best_play_time;


    public void LoadData(GameData data)
    {
        last_play_time = data.last_play_time;
        List<DimensionData> dimensionDatas = data.dimensionData;
        for (int i = 0; i < dimensionDatas.Count; i++)
        {
            int index = i;
            if (dimensionDatas[index].dimension_id == dimension_id)
            {
                best_play_time = data.dimensionData[index].best_play_time;
                break;
            }
        }

    }

    public void SaveData(ref GameData data)
    {
        data.Gold += obtain_gold;
        List<SpellData> all_spells = data.all_spells;
        foreach (Spell spell in obtain_spell)
        {
            SpellData spellData = new(spell);
            if (!all_spells.Contains(spellData))
            {
                all_spells.Add(spellData);
            }
            else
            {
                Debug.Log("Already has spell: " + spell._name);
            }

        }
        all_spells = all_spells.Distinct().ToList();

        data.all_spells = all_spells;
        Debug.Log("Save Reward Tracker");
    }

    protected override void InitAfterAwake()
    {
        dimension_id = DimensionManager.Instance.dimensionID;
    }

    public void AddGoldObtain(float gold)
    {
        obtain_gold += gold;
    }
    public void AddSpellObtain(Spell spell)
    {
        obtain_spell.Add(spell);
        obtain_spell = obtain_spell.Distinct().ToList();
    }

}
