using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : ScriptableObject
{
    public string _name;
    public string _description;
    public int _level;
    public float _cooldown;
    public float _manaCost;
    public Sprite _icon;
    public SpellType _spellType;
    public ElementalType _elementalType;
    public KeyCode _activateKey;
    public virtual void Attack(){}
}
public enum SpellType{
    Default,
    QuickCast,
    Consentrade
}


