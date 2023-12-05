using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DrawCaster.DataPersistence;
using UnityEngine;

public class RewardTrackerManager : Singleton<RewardTrackerManager>, IDataPersistence
{
    [Header("File Config")]
    public float obtain_gold;
    public List<Spell> obtain_spell = new();

    public void LoadData(GameData data)
    {

    }

    public void SaveData(ref GameData data)
    {
        data.Gold += obtain_gold;
        List<SpellData> all_spells = data.all_spells;
        foreach (Spell spell in obtain_spell)
        {
            all_spells.Add(new SpellData(spell));
        }
        all_spells = all_spells.Distinct().ToList();

        data.all_spells = all_spells;
        Debug.Log("Save Reward Tracker");

        obtain_gold = 0;
        obtain_spell = new();
    }

    protected override void InitAfterAwake()
    {
    }

    public void AddGoldObtain(float gold)
    {
        obtain_gold += gold;
    }

    private void OnEnable()
    {
    }
}
