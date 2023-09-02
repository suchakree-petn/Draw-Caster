using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_LightningStorm", menuName = "Spell/Lightningstorm")]
public class LightningStorm : Spell
{
    [SerializeField] private float _baseSkillDamageMultiplier;
    [SerializeField] private GameObject _gameObjectSprite;
    [SerializeField] private float _delayTime;
    [SerializeField] private float _range;
    

    
    
    public override void Cast1(GameObject player, GameObject target)
    {
        base.Cast1(player,target);
        // Collider2D lightningHit = _gameObjectSprite.transform.GetComponentInChildren<Collider2D>(); State Machine
        // Animate
            Instantiate(_gameObjectSprite,target.transform.position,Quaternion.identity);
        // เล่นsound
    }
    public override void Cast2(GameObject player, GameObject target)
    {
        // เล่นsound
    }
    public override void Cast3(GameObject player, GameObject target)
    {
        // เล่นsound
    }
    public float GetRange(){
        return _range;
    }
    public float GetDelayTime(){
        return _delayTime;
    }
    

}