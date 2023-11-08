using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Cloak", menuName = "Item/Relic/Cloak")]
public class Cloak : Relic
{
    public float _BonusHp;
    public float _BonusDefense;
    public override void Equip(GameObject player)
    {
        base.Equip(player);
    }
}
