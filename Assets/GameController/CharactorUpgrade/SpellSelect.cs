using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DrawCaster.DataPersistence;

public class SpellSelect : Singleton<SpellSelect>, IDataPersistence
{
    public List<SpellData> player_spells = new();
    [SerializeField] private List<Spell> spells = new();
    public int _maxSlot;



    protected override void InitAfterAwake()
    {
    }

    public void LoadData(GameData data)
    {
        this.player_spells = data.player_spells;
    }

    public void SaveData(ref GameData data)
    {
        List<SpellData> new_spell_list = new();
        foreach (Spell spell in spells)
        {
            SpellData spellData = new(spell);
            new_spell_list.Add(spellData);
        }
        data.player_spells = new_spell_list;
    }

    // public void Add(Spell spell, ref int amount)
    // {
    //     for (int i = 0; i < itemList.Count; i++)
    //     {
    //         if (itemList[i].item._name == spell._name && itemList[i].stackCount + amount <= itemList[i].maxStackCount)
    //         {
    //             itemList[i].stackCount += amount;
    //             amount = 0;
    //             return;
    //         }
    //         else if (itemList[i].item._name == spell._name && itemList[i].stackCount + amount > itemList[i].maxStackCount)
    //         {
    //             int deficit = itemList[i].stackCount + amount - spell._maxStackCount;
    //             itemList[i].stackCount += amount - deficit;
    //             amount = deficit;
    //             return;
    //         }
    //     }

    //     if (itemList.Count < _maxSlot)
    //     {
    //         itemList.Add(new SlotItem(spell, amount, spell._maxStackCount));
    //         amount = 0;
    //     }
    // }
    // public void Remove(Spell spell, int amount)
    // {
    //     foreach (SlotItem slot in itemList)
    //     {
    //         if (slot.item == spell)
    //         {
    //             if (slot.maxStackCount > amount)
    //             {
    //                 slot.stackCount -= amount;
    //             }
    //             else
    //             {
    //                 itemList.Remove(slot);
    //                 return;
    //             }
    //         }
    //     }
    // }
}
// [System.Serializable]
// public class SlotSpell
// {
//     public Item item;
//     public int stackCount;
//     public int maxStackCount;
//     public SlotSpell(Spell spell)
//     {
//         this.item = spell;
//         this.stackCount = amount;
//         this.maxStackCount = maxStackCount;
//     }
// }



