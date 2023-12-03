using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DrawCaster.DataPersistence;
using System;


public class SpellSelectUI_Vertical : MonoBehaviour
{
    [Header("Spell List")]
    public List<GameObject> UISlot;
    public SpellData _currentSelectSpell;



    [Header("Description Panel")]
    [SerializeField] private TextMeshProUGUI _descriptionText;


    [Header("UI Transform")]
    [SerializeField] private Transform _inventoryContentTransform;
    [SerializeField] private Transform _descriptionContentTransform;


    [Header("Prefab")]
    [SerializeField] private GameObject slotPrefab;

    public delegate void SlotActions(SpellData spell);
    public static SlotActions OnSlotClick;
    public static Action<SpellData, GameObject> OnSpellSlotClick;


    [SerializeField] private List<SpellData> _player_spells;

    private void Start()
    {

    }
    private GameObject CreateUISlot(SpellData spell)
    {
        GameObject slot = Instantiate(slotPrefab, _inventoryContentTransform);
        slot.GetComponent<UISlotData>().spellData = spell;
        slot.transform.GetChild(1).GetComponent<Image>().sprite = DataPersistenceManager.Instance.dataHandler.LoadSpriteFromFile(spell.spritePath);
        slot.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = spell.Name;
        return slot;
    }

    public void RefreshInventoryData(SpellData spell)
    {
        // Is list empty?
        if (_player_spells == null)
        {
            return;
        }

        // Clear inventory
        foreach (Transform child in _inventoryContentTransform)
        {
            Destroy(child.gameObject);
        }

        // Clear list
        UISlot.Clear();


        // Enable description
        InitDescriptionUI();

        // Refresh to show current select item info
        Debug.Log("refresh data");
        // RefreshUIInventory(_currentSelectItem);
    }


    private void InitDescriptionUI()
    {
        if (_player_spells == null)
        {
            _inventoryContentTransform.gameObject.SetActive(false);
            _descriptionContentTransform.gameObject.SetActive(false);
        }
        else
        {
            _inventoryContentTransform.gameObject.SetActive(true);
            _descriptionContentTransform.gameObject.SetActive(true);
        }
    }

    private void RefreshUIInventory(SpellData currentSelectSpell)
    {
        Debug.Log("refresh ui");
        RefreshDescriptionText(currentSelectSpell);
    }
    private void RefreshSelectSpellIcon(SpellData spellData, GameObject slot)
    {
        if (spellData.spritePath == "") return;

        slot.GetComponentInChildren<Image>().sprite =
            DataPersistenceManager.Instance.dataHandler.LoadSpriteFromFile(spellData.spritePath);
        Debug.Log("Refresh Icon");
    }

    private void RefreshDescriptionText(SpellData spell)
    {
        _descriptionText.text = spell.Desc;
    }

    private void InitSpellDataList()
    {
        _player_spells = SpellSelect.Instance.all_spells_data;

        foreach (SpellData spellData in _player_spells)
        {
            UISlot.Add(CreateUISlot(spellData));
        }

        if (_player_spells.Count > 0)
        {
            _currentSelectSpell = _player_spells[0];
            OnSlotClick?.Invoke(_currentSelectSpell);
        }
        else
        {
            Debug.Log("No spell in list");
        }
    }
    private void OnEnable()
    {
        DataPersistenceManager.OnLoadSuccess += InitSpellDataList;

        OnSlotClick += RefreshUIInventory;
        OnSpellSlotClick += RefreshSelectSpellIcon;
    }
    private void OnDisable()
    {
        DataPersistenceManager.OnLoadSuccess -= InitSpellDataList;

        OnSlotClick -= RefreshUIInventory;
        OnSpellSlotClick -= RefreshSelectSpellIcon;


    }


}
