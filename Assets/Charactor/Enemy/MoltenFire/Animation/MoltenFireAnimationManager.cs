using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoltenFire;

public class MoltenFireAnimationManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D moltenFireRb;
    public Transform target => transform.parent.GetComponentInChildren<MoltenFireBehaviour>().target;
    private EnemyData moltenFireData;

    private void Awake()
    {
        moltenFireData = GetComponentInParent<CharactorManager<EnemyData>>().GetCharactorData();
        moltenFireRb = GetComponentInParent<Rigidbody2D>();
    }

   

}
