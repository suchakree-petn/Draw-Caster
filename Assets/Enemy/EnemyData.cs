using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Charactor Data/Enemy Data")]
public class EnemyData : CharactorData
{
  public override void Dead(GameObject deadCharactor)
  {
    base.Dead(deadCharactor);
    Destroy(deadCharactor);
  }
  public override void CheckDead(GameObject charactor)
  {
    base.CheckDead(charactor);
    Debug.Log("CheckDead");
    if (_maxHp <= 0)
    {
      GameController.OnEnemyDead?.Invoke(charactor);
    }
  }

}
