using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Charactor Data/Enemy Level Growth Data")]
public class EnemyLGData : ScriptableObject
{
  [Header("Level Growth Base")]
  public float lgBaseAtk;
  public float lgBaseHp;
  public float lgBaseDef;
  public float lgBaseMoveS;
  public float lgBaseKnckB;
  public float lgBaseGold;
  [Header("Level Growth")]
  public float lgAtk;
  public float lgHp;
  public float lgDef;
  public float lgMoveS;
  public float lgKnckB;
  public float lgGold;

}