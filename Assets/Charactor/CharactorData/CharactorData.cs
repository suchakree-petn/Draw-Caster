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

   [Header("Mana Point")]
   public float _manaBase;
   public float _manaMultiplier;
   public float _manaBonus;

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

   [Header("Knockback Point")]
   public float _knockbackBase;
   public float _knockbackMultiplier;
   public float _knockbackBonus;

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
   public float GetMaxMana(){
      return (_manaBase * (1 + _manaMultiplier)) + _manaBonus;
   }
   public float GetMaxKnockbackGauge(){
      return (_knockbackBase * (1 + _knockbackMultiplier)) + _knockbackBonus;
   }
}
