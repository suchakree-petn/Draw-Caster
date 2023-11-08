using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Necklace", menuName = "Item/Relic/Necklace")]
public class Necklace : Relic
{
    public float _elementalBonusDamage;
    public ElementalType _elementalBonusType;

    public override void Equip(GameObject player)
    {
        base.Equip(player);
    }
}
