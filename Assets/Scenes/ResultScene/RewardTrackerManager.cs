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

    [Header("Play time")]
    public float last_play_time;
    public float best_play_time;


    public void LoadData(GameData data)
    { 
        last_play_time = data.last_play_time;
        best_play_time = data.best_play_time;
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
