using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Default,
    Weapon,
    Consumable,
    Relic
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
    [TextArea(4,6)]
    public string _description;
    public Sprite _icon;
    public ItemType _itemType;
    public ItemRarity _itemRarity;
    public int _maxStackCount;
    public int _price;

    [Header("Prefab")]
    public GameObject itemPrefab;

}
