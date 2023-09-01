using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHolder : MonoBehaviour
{
    [SerializeField] private Spell _spell1;
    [SerializeField] private List<GameObject> _enemyList = new List<GameObject>();
    [SerializeField] private float _spellRange;
    void Start()
    {
        // _spell1.Cast(transform.root.gameObject);
    }

    
}
