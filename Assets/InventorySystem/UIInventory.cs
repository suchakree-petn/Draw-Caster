using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public abstract class UIInventory : MonoBehaviour
{
    [Header("Item List")]
    public List<GameObject> UISlot;
    public Item _currentSelectItem;



    [Header("Description Panel")]
    [SerializeField] private TextMeshProUGUI _descriptionName;
    [SerializeField] private Image _descriptionIcon;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _priceText;

    [Header("UI Transform")]
    [SerializeField] private Transform _inventoryContentTransform;
    [SerializeField] private Transform _descriptionContentTransform;


    [Header("Prefab")]
    [SerializeField] private GameObject slotPrefab;

    public delegate void SlotActions(Item item);
    public static SlotActions OnSlotClick;

    public delegate void CategoryActions(Item item);
    public static CategoryActions OnCategoryClick;

    [SerializeField] private List<SlotItem> _slotItem = new List<SlotItem>();

    [Header("For sent price to any class")]
    public static int GetpriceNow;


    private void Start()
    {
        _slotItem = InventorySystem.Instance.itemList;
        if (_slotItem.Count > 0)
        {
            _currentSelectItem = _slotItem[0].item;
            OnSlotClick?.Invoke(_currentSelectItem);
        }
        else
        {
            Debug.Log("No item in list");
        }


    }
    private GameObject CreateUISlot(SlotItem slotItem)
    {
        GameObject slot = Instantiate(slotPrefab, _inventoryContentTransform);
        slot.GetComponent<UISlotData>().item = slotItem.item;
        slot.transform.GetChild(0).GetComponent<Image>().sprite = slotItem.item._icon;
        slot.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = slotItem.stackCount.ToString();
        return slot;
    }

    public void RefreshInventoryData(Item item)
    {
        // Is list empty?
        if (_slotItem == null)
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

        // Create new slot
        UISlot = GetItemListByType(item._itemType);
        GetpriceNow = item._price;
       // Debug.Log(item._price);//Price now

        // Enable description
        InitDescriptionUI();

        // Refresh to show current select item info
        Debug.Log("refresh data");
        // RefreshUIInventory(_currentSelectItem);
        //Debug.Log(priceNow);
    }

    public List<GameObject> GetItemListByType(ItemType itemType)
    {
        List<GameObject> _UISlot = new();
        SlotItem firstItemInSlot = null;
        if(_slotItem.Count ==0){
            Debug.Log("slot item: 0");
        }
        for (int i = 0; i < _slotItem.Count; i++)
        {

            if (_slotItem[i].item._itemType == itemType)
            {
                if (firstItemInSlot == null)
                {
                    firstItemInSlot = _slotItem[i];
                }
                GameObject slot = CreateUISlot(_slotItem[i]);
                _UISlot.Add(slot);
            }
        }

        // Init first select slot
        if (firstItemInSlot == null)
        {
            _currentSelectItem = null;
        }
        else
        {
            _currentSelectItem = firstItemInSlot.item;
        }
        
        
        Debug.Log(_UISlot.Count);
        return _UISlot;
    }

    private void InitDescriptionUI()
    {
        if (_slotItem == null || _currentSelectItem == null)
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

    private void RefreshUIInventory(Item currentSelectItem)
    {
        Debug.Log("refresh ui");
        RefreshDescriptionName(currentSelectItem);
        RefreshDescriptionIcon(currentSelectItem);
        RefreshDescriptionText(currentSelectItem);
    }
    private void RefreshDescriptionName(Item item)
    {
        if (item != null)
        {
            _descriptionName.text = item._name;
        }
    }
    private void RefreshDescriptionIcon(Item item)
    {
        if (item != null)
        {
            _descriptionIcon.sprite = item._icon;
        }
    }

    private void RefreshDescriptionText(Item item)
    {
        if (item != null)
        {
            _descriptionText.text = item._description;
        }
    }
    

    public abstract void ShowInventory(Transform canvasTransform);
    private void OnEnable()
    {
        OnSlotClick += RefreshInventoryData;
        OnSlotClick += RefreshUIInventory;

        OnCategoryClick += RefreshInventoryData;
        OnCategoryClick += RefreshUIInventory;

    }
    private void OnDisable()
    {
        OnSlotClick -= RefreshInventoryData;
        OnSlotClick -= RefreshUIInventory;

        OnCategoryClick -= RefreshInventoryData;
        OnCategoryClick -= RefreshUIInventory;
    }
}
