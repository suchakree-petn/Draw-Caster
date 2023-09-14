using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Spell : MonoBehaviour
{
    public SpellObj spellObj;
    public bool _isReadyToCast;

    public abstract void CastSpell(float score);
    public abstract void Cast1(GameObject player, GameObject target);
    public abstract void Cast2(GameObject player, GameObject target);
    public abstract void Cast3(GameObject player, GameObject target);

    public abstract int CalThreshold(float score);
    
    
    public virtual void BeginCooldown(GameObject gameObject)
    {

    }
    
    
    public IEnumerator Cooldown(SpellObj spell)
    {
        yield return new WaitForSeconds(spell._cooldown);
        _isReadyToCast = true;
    }
   
}


