using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class test : MonoBehaviour, IDamageable
{
    
    public void TakeDamage(Elemental elementalDamage)
    {
        Debug.Log(gameObject.name + " Take: " + elementalDamage._damage + "\nType: " + elementalDamage._elementalType);
    }
}
