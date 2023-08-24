using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorData : ScriptableObject
{


   [Header("Info")]
   public string _name;

   [Header("Movement")]
   public float _moveSpeed;

   [Header("Status")]
   public float _attackBase;
   public float _attackMultiplier;
   public float _attackBonus;
   public float _bonusDamage;
   public float _defenseBase;
   public float _defenseMultiplier;
   public float _defenseBonus;
   public float _bonusDamageReduction;
   public float _fireBonusDamage;
   public float _thunderBonusDamage;
   public float _frostBonusDamage;
   public float _windBonusDamage;

   [Header("Prefab")]
   public GameObject CharactorPrefab;

   private void Awake()
   {
      _name = "Default Charactor";

   }

}
