using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using System.Linq;


public abstract class Spell : MonoBehaviour
{
    public SpellObj spellObj { get; set; }
    public bool _isReadyToCast;
    public int castLevel;

    public abstract void CastSpell(float score);
    public abstract void Cast1(GameObject player, GameObject target);
    public abstract void Cast2(GameObject player, GameObject target);
    public abstract void Cast3(GameObject player, GameObject target);

    public int CalThreshold(float score)
    {
        float low = spellObj._lowThreshold;
        float mid = spellObj._midThreshold;

        int castLevel = 1;
        if (score >= 0 && score <= low)
        {
            castLevel = 1;
        }
        else if (score > low && score <= mid)
        {
            castLevel = 2;
        }
        else if (score > mid && score <= 1)
        {
            castLevel = 3;
        }
        else
        {
            Debug.Log("CastThershold ERROR");
        }
        return castLevel;
    }
    public virtual void BeginCooldown(GameObject gameObject)
    {

    }


    public IEnumerator Cooldown(SpellObj spell)
    {
        yield return new WaitForSeconds(spell._cooldown);
        _isReadyToCast = true;
    }

}


