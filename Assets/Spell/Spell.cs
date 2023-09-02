using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : ScriptableObject
{
    [Header ("Infomation")]
    public string _name;
    public string _description;
    public Sprite _icon;
    [Header ("Spell Setting")]
    public int _level;
    public SpellType _spellType;
    public ElementalType _elementalType;
    public float _cooldown;
    public float _manaCost;
    public int _amount;
    public KeyCode _activateKey;
    
    [Header ("Draw Input")]
    public float _spellThreshold;
    public Texture2D _templateImage;
    public GameObject _checkInputPrefab;
    
    public virtual void Cast1(GameObject player, GameObject target){}
    public virtual void Cast2(GameObject player, GameObject target){}
    public virtual void Cast3(GameObject player, GameObject target){}

    public void Cast(GameObject player, GameObject target){
        if(_spellThreshold >= 0 && _spellThreshold <= 0.2f){
            Cast1(player, target);
        }else if(_spellThreshold > 0.2f && _spellThreshold <= 0.5f){
            Cast2(player, target);
        }else if(_spellThreshold > 0.5f && _spellThreshold <= 1){
            Cast3(player, target);
        }else{
            Debug.Log("Spell Threshold > 1");
        }
        Debug.Log(player.name);
    }

    // void private void Awake() {
            // _spellThreshold = _checkInputPrefab.GetComponant<Cossim>().Get_spellThreshold;
    // }
}

public enum SpellType{
    Default,
    QuickCast,
    Consentrade
}


