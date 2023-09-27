using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Charactor Data/Enemy Data")]
public class EnemyData : CharactorData
{
  public ElementalType elementalType;
  public override void Dead(GameObject deadCharactor)
  {
    Destroy(deadCharactor);
  }
  public override void CheckDead(GameObject charactor, Elemental damage)
  {
    //Debug.Log("CheckDead");
    if (charactor.GetComponent<EnemyManager>().currentHp <= 0)
    {
      GameController.OnEnemyDead?.Invoke(charactor);
    }
  }

}
