using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Charactor Data/Enemy Data")]
public class EnemyData : CharactorData
{
  public EnemyLGData enemyLGData;
  public ElementalType elementalType;
  public ElementalType elementalResistant;
  public float goldDrop = 1;
  public Spell spellDrop;

}
