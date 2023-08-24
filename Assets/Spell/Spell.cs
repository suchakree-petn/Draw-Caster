using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : ScriptableObject
{
    public string _name;
    public string _description;
    public SpellType _spellType;
    public Sprite _icon;
    public int _level;
    public float _cooldown;
    public float _manaCost;
    public SpellElement _spellElement;
    public KeyCode _activateKey;
}
public enum SpellType{
    QuickCast,
    Consentrade
}

public enum SpellElement{
    Fire,
    Thunder,
    Frost,
    Wind

}