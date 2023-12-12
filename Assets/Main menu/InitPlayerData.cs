using System.Collections;
using System.Collections.Generic;
using DrawCaster.DataPersistence;
using UnityEngine;

public class InitPlayerData : MonoBehaviour, IDataPersistence
{
    public List<Spell> defaultSpell = new();

    public void LoadData(GameData data)
    {

    }

    public void SaveData(ref GameData data)
    {
        if (data.all_spells.Count == 0)
        {
            List<SpellData> spellDatas = new();
            foreach (Spell spell in defaultSpell)
            {
                SpellData spellData = new(spell);
                spellDatas.Add(spellData);
                data.player_equiped_spells.Add(spellData.Name);
            }
            data.all_spells = spellDatas;
        }
    }

    void Start()
    {
        DataPersistenceManager.Instance.LoadGame();
        DataPersistenceManager.Instance.SaveGame();
    }
}
