using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_LightningStorm", menuName = "Spell/Lightningstorm")]
public class LightningStormObj : SpellObj
{
    [Header("Damage Multiplier")]
    public float _baseSkillDamageMultiplier;
    public float _damageSpellLevelMultiplier1;
    public float _damageSpellLevelMultiplier2;
    public float _damageSpellLevelMultiplier3;

    [Header("Lighning Storm Setting")]
    public float _delayTime;
    public int _amountLevel1;
    public int _amountLevel2;
    public int _amountLevel3;
    public GameObject _lightningStormPrefab;
    


}