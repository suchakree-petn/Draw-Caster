using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Exp Scroll", menuName = "Item/Consumable/Exp Scroll")]
public class ExpScroll : Consumable
{
    [SerializeField] private float _expAmount;

    public override void Consume(GameObject consumer)
    {
        base.Consume(consumer);

        // Add Exp to consumer
    }

    private void Awake() {
        _itemType = ItemType.Consumable;
    }
}
