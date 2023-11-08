using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour, ICollectable
{

    public Item item;
    public int amount;

    public void Collect()
    {
        InventorySystem inventorySystem = InventorySystem.Instance;
        inventorySystem.Add(item, ref amount);
        if (amount == 0)
        {
            Destroy(gameObject);
        }
    }
}

