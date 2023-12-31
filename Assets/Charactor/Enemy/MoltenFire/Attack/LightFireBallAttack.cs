using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DrawCaster.Util;
using MoltenFire;
using UnityEngine;


public class LightFireBallAttack : MonoBehaviour
{
    [Header("Fire Ball Attack")]
    public float _baseFireBallDamageMultiplier;
    [SerializeField] private float fireBallKnockbackGaugeDeal;
    [SerializeField] private float repeatInterval;
    [SerializeField] private float launchDuration;
    [SerializeField] private float curveDuration;
    [SerializeField] private float spawnInterval;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private int fireBallAmount;
    [SerializeField] private bool isRepeating;
    [SerializeField] private float iFrame;

    [SerializeField] private MoltenFireBehaviour moltenFireBehaviour;

    private void Start()
    {
    }

    void FireBallAttack()
    {
        Sequence sequence = DOTween.Sequence();

        for (int i = 0; i < fireBallAmount; i++)
        {
            Vector3 spawnPos = moltenFireBehaviour.GetNewSpawnPos(i).position;
            Transform fireBallAttack = DrawCasterUtil.AddAttackHitTo(
                Instantiate(fireballPrefab, spawnPos, Quaternion.identity),
                moltenFireBehaviour.moltenFireData.elementalType,
                transform.root.gameObject,
                _baseFireBallDamageMultiplier,
                (launchDuration + curveDuration + spawnInterval) * i + 1,
                moltenFireBehaviour.moltenFireData.targetLayer,
                fireBallKnockbackGaugeDeal,
                iFrame
                ).transform;
            int index = i;
            sequence.AppendCallback(() =>
            {
                moltenFireBehaviour.Move(fireBallAttack, index);
            });
            sequence.AppendInterval(spawnInterval);
        }
    }
    public void Disable()
    {
        isRepeating = false;
        CancelInvoke(nameof(FireBallAttack));
    }

    private void CheckCondition()
    {
        State s = moltenFireBehaviour.currentState;
        if (s == State.MeleeAttack || s == State.FireBallAttack || s == State.FlamePillarAttack || moltenFireBehaviour.target == null)
        {
            Disable();
        }
        else
        {
            if (!isRepeating)
            {
                isRepeating = true;
                InvokeRepeating("FireBallAttack", 0, repeatInterval);
            }
        }
    }
    private void OnEnable()
    {
        GameController.WhileInGame += CheckCondition;
    }

    private void OnDisable()
    {
        GameController.WhileInGame -= CheckCondition;
        Disable();
    }
}

