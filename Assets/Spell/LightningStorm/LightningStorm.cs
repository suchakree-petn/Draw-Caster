using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_LightningStorm", menuName = "Spell/Lightningstorm")]
public class LightningStorm : Spell
{
    [SerializeField] private float _baseSkillDamageMultiplier;
    [SerializeField] private GameObject _gameObjectSprite;

    [SerializeField] private float _range;
    

    
    
    public override void Cast1(GameObject player, GameObject target)
    {
        base.Cast1(player,target);
        
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
    public override void BeginCooldown(GameObject gameObject)
    {
        base.BeginCooldown(gameObject);
    }
    

}