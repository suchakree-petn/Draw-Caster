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
    [SerializeField] private float move_speed;
    public float Move_speed => move_speed;
    private float timer;
    private float ChaseTime
    {
        get
        {
            return Random.Range(3f, 5f);
        }
    }
    [SerializeField] private float chaseTime = 3;
    public bool canMove;

    [Header("Reference")]
    public AnimationClip attack_clip;
    public AnimationClip knockback_clip;
    public AnimationClip dead_clip;
    public Animator animator;
    public Rigidbody2D crystalMaulerRb;
    [SerializeField] private Collider2D hitbox_col;
    private void Awake()
    {
        FindPlayer();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            timer += Time.deltaTime;
            MoveCharactor(crystalMaulerRb, target, move_speed);
        }
        else
        {
            timer = 0;
            chaseTime = ChaseTime;
        }
    }
    private void MoveCharactor(Rigidbody2D rb, Transform target, float move_speed)
    {
        Vector2 dir = DrawCasterUtil.GetDirectionFromMid(rb.transform, target);
        SetZeroVelocity();
        rb.AddForce(move_speed * Time.fixedDeltaTime * dir.normalized, ForceMode2D.Impulse);
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
    private void AnimKnockback(Elemental elemental)
    {
        animator.SetTrigger("Knockback");

    }
    public void SetZeroVelocity()
    {
        crystalMaulerRb.velocity = Vector2.zero;
    }
    private void AnimDead(GameObject deadCharactor)
    {
        hitbox_col.enabled = false;
        animator.SetTrigger("Death");
        Destroy(deadCharactor, dead_clip.length);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        OnStartKnockback += AnimKnockback;
        OnEnemyDead += AnimDead;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnStartKnockback -= AnimKnockback;
        OnEnemyDead -= AnimDead;

    }
    private void OnDrawGizmosSelected()
    {
        Transform attacker = DrawCasterUtil.GetMidTransformOf(transform);
        float attackRange = Detect_range;
        Vector2 boxCenter = (Vector2)attacker.position + (Vector2)attacker.right * attackRange / 2f;
        Vector2 boxSize = new Vector2(attackRange, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(attacker.position + attacker.right * (attackRange / 2f), boxSize);

        Debug.DrawLine(transform.root.position, transform.root.position + new Vector3(detect_range, 0, 0), Color.red);
    }
    public void PlayHitSound(){
        transform.GetChild(3).GetChild(0).GetComponent<AudioSource>().Play();
    }
}
