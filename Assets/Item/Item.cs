using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Default,
    Weapon,
    Consumable,
    Matterial
}
public class Item : ScriptableObject
{
    public string Name;
    public ItemType itemType;
    public bool Is_Stackable;
    public Sprite icon;
    public GameObject itemPrefab;
    void Awake()
    {
        itemType = ItemType.Default;
    }

    public virtual void PrepareItem(GameObject itemObj) { }
    public virtual void UseItem(GameObject parent) { }

}
