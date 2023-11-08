using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ring", menuName = "Item/Relic/Ring")]
public class Ring : Relic
{
    public float _BonusMana;
        public override void Equip(GameObject player)
    {
        base.Equip(player);
    }
}
