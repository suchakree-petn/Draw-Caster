using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour, ICollectable
{
    public Item item;
    private void Start()
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            SpriteRenderer sp = gameObject.AddComponent<SpriteRenderer>();
            sp.sprite = item.icon;
        }
    }
}
