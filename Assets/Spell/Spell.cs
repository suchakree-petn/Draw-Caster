using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : ScriptableObject
{
    [Header("Infomation")]
    public string _name;
    public string _description;
    public Sprite _icon;
    [Header("Spell Setting")]
    public int _level;
    public SpellType _spellType;
    public ElementalType _elementalType;
    public float _cooldown;
    public float _manaCost;
    public bool _isReadyToCast;
    public float _delayTime;
    public int _amountLevel1;
    public int _amountLevel2;
    public int _amountLevel3;
    public KeyCode _activateKey;

    [Header("Draw Input")]
    public Texture2D _templateImage;
    public float _lowThreshold;
    public float _midThreshold;

    public virtual void Cast1(GameObject player, GameObject target) { }
    public virtual void Cast2(GameObject player, GameObject target) { }
    public virtual void Cast3(GameObject player, GameObject target) { }

    public int GetAmount(int castLevel)
    {
        if (castLevel == 1)
        {
            return _amountLevel1;
        }
        else if (castLevel == 2)
        {
            return _amountLevel2;
        }
        return _amountLevel3;
    }

    public virtual void BeginCooldown(GameObject gameObject)
    {
        
    }
}

public enum SpellType
{
    Default,
    QuickCast,
    Consentrade
}


