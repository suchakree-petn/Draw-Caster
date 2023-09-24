using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class CharactorData : ScriptableObject
{
   [Header("Info")]
   public string _name;
   public int _level;
   public LayerMask targetLayer;

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

   public float GetMaxHp(){
      return (_hpBase * (1 + _hpMultiplier)) + _hpBonus;
   }
   public abstract void Dead(GameObject deadCharactor);
   public abstract void CheckDead(GameObject charactor,float damage);
}
