using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Default,
    Wand
}
public enum ItemRarity
{
    Common,
    Rare,
    Epic,
    Legendary
}
public class Item : ScriptableObject
{
    [Header("Info")]
    public string _name;
    public string _description;
    public Sprite icon;
    public ItemType _itemType;
    public ItemRarity _itemRarity;
    public bool _isStackable;

    [Header("Prefab")]
    public GameObject itemPrefab;

    void Awake()
    {
        _itemType = ItemType.Default;
    }
}
