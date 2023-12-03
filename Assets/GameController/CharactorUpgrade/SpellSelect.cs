using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DrawCaster.DataPersistence;
using System;

public class SpellSelect : Singleton<SpellSelect>, IDataPersistence
{
    public List<SpellData> all_spells_data = new();
    [SerializeField] private List<Spell> all_spells = new();
    [SerializeField] private List<Spell> equip_spells = new(4);

    [Header("Spell Slot")]
    [SerializeField] private List<UISlotData> spell_slot = new(4);
    public int _maxSlot;

    public static Action OnUpdateSpellSelectSlotSuccess;

    protected override void InitAfterAwake()
    {
    }

    public void LoadData(GameData data)
    {
        this.all_spells_data = data.all_spells;

        List<Spell> allSpell = new();
        foreach (SpellData spell in data.all_spells)
        {
            Spell spell_temp = Resources.Load<Spell>(spell.Obj_Name);
            allSpell.Add(spell_temp);
            Debug.Log(spell_temp + "  Loadedd");
        }
        this.all_spells = allSpell;


        List<Spell> equipSpell = new();
        foreach (string spell in data.player_equiped_spells)
        {
            equipSpell.Add(Resources.Load<Spell>(spell));
        }
        this.equip_spells = equipSpell;
    }

    public void SaveData(ref GameData data)
    {
        List<SpellData> allSpell = new();
        foreach (Spell spell in all_spells)
        {
            allSpell.Add(new SpellData(spell));
        }
        data.all_spells = allSpell;

        List<string> new_equip_spell_list = new();
        foreach (Spell spell in equip_spells)
        {
            new_equip_spell_list.Add(spell.name);
        }
        data.player_equiped_spells = new_equip_spell_list;
    }

    private void UpdateSpellSelectSlot()
    {
        for (int i = 0; i < equip_spells.Count; i++)
        {
            Spell equipSpell = equip_spells[i];
            if (equip_spells != null)
            {
                spell_slot[i].spellData = new SpellData(equipSpell);
            }
        }
        OnUpdateSpellSelectSlotSuccess?.Invoke();
    }

    private void OnEnable()
    {
        DataPersistenceManager.OnLoadSuccess += UpdateSpellSelectSlot;
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




