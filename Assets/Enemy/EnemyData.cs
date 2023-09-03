using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Charactor Data/Enemy Data")]
public class EnemyData : CharactorData
{
    public override void Dead(GameObject deadCharactor){
        base.Dead(deadCharactor);
        Destroy(deadCharactor);
    }
}
