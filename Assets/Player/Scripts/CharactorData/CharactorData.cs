using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class CharactorData : ScriptableObject
{
   [Header("Info")]
   public string _name;
   public float _maxHp;
   public int _level;

   [Header("Movement")]
   public float _moveSpeed;

   [Header("Health Point")]
   public float _hpBase;
   public float _hpMultiplier;
   public float _hpBonus;

   [Header("Attack Point")]
   public float _attackBase;
   public float _attackMultiplier;
   public float _attackBonus;
   public float _bonusDamage;


   [Header("Defense Point")]
   public float _defenseBase;
   public float _defenseMultiplier;
   public float _defenseBonus;
   public float _bonusDamageReduction;

   [Header("Elemental Bonus")]
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
   private void OnEnable()
   {
      _maxHp = (_hpBase * (1 + _hpMultiplier)) + _hpBonus;
   }
   public virtual void Dead(GameObject deadCharactor)
   {

   }
   public virtual void CheckDead(GameObject charactor)
   {

   }
}
