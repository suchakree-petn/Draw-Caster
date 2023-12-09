using System.Collections;
using System.Collections.Generic;
using DrawCaster.Util;
using UnityEngine;

public class Crystal_Mauler : EnemyManager
{
    public Transform target;
    [Header("Crystal Mauler")]
    [SerializeField] private float detect_range;
    public float Detect_range => detect_range;
    [SerializeField] private float melee_dmg_multiplier;
    [SerializeField] private float melee_kb_deal;

    [Header("Reference")]
    public AnimationClip attack_clip;
    private void Awake()
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        target = GameObject.FindWithTag("Player").transform;
    }
    public bool IsTargetInRange()
    {
        Transform targetTransform = DrawCasterUtil.GetMidTransformOf(target);
        Transform crytalMaulerTransform = DrawCasterUtil.GetMidTransformOf(transform);
        return Vector2.Distance(targetTransform.position, crytalMaulerTransform.position) <= detect_range;
    }
    public Elemental GetMeleeAttackDMG()
    {
        return new Elemental(
            enemyData.elementalType,
            melee_dmg_multiplier,
            gameObject,
            enemyData,
            enemyData.targetLayer,
            melee_kb_deal
        );
    }
    protected override void OnEnable()
    {
        base.OnEnable();

    }

    protected override void OnDisable()
    {
        base.OnDisable();

    }
    private void OnDrawGizmosSelected()
    {

        Debug.DrawLine(transform.root.position, transform.root.position + new Vector3(detect_range, 0, 0), Color.red);
    }
}
