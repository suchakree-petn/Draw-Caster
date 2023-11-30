using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
    [Header("Info")]
    public int _level;

    [Header("Movement")]
    public float _moveSpeed;

    [Header("Health Point")]
    public float _hpBase;

    [Header("Mana Point")]
    public float _manaBase;

    [Header("Attack Point")]
    public float _attackBase;

    [Header("Defense Point")]
    public float _defenseBase;


    public GameData()
    {
        _level = 1;
        _moveSpeed = 5;
        _hpBase = 2000;
        _manaBase = 300;
        _attackBase = 5;
        _defenseBase = 100;
    }
}
