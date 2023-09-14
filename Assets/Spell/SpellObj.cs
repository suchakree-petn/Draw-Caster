using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellObj : ScriptableObject
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


    [Header("Draw Input")]
    public Texture2D _templateImage;
    public Sprite UI_image;
    public float _lowThreshold;
    public float _midThreshold;

    public virtual void Cast1(GameObject player, GameObject target) { }
    public virtual void Cast2(GameObject player, GameObject target) { }
    public virtual void Cast3(GameObject player, GameObject target) { }

    

    public virtual void BeginCooldown(GameObject gameObject)
    {
        
    }
}

public enum SpellType
{
    Default,
    QuickCast,
    Concentrate
}


