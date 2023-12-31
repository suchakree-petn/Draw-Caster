using DrawCaster.Util;
using UnityEngine;

public class State_Crystal_Mauler_Attack : StateMachineBehaviour
{
    [SerializeField] private Crystal_Mauler crystal_Mauler;
    [SerializeField] private Vector2 boxCenter;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float boxAngle;
    private float timer;
    private bool isAttacked;
    Transform attacker;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (crystal_Mauler == null)
        {
            crystal_Mauler = animator.GetComponent<Crystal_Mauler>();
        }
        timer = 0;
        isAttacked = false;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        float attack_frame = crystal_Mauler.attack_clip.length / 2;
        if (timer >= attack_frame && !isAttacked)
        {
            isAttacked = true;
            attacker = DrawCasterUtil.GetMidTransformOf(animator.transform);
            PerformMeleeAttack(attacker, crystal_Mauler.Detect_range, crystal_Mauler);
        }
    }

    public void PerformMeleeAttack(Transform attacker, float attackRange, Crystal_Mauler crystal_Mauler)
    {
        boxCenter = (Vector2)attacker.position + (Vector2)attacker.right * attackRange / 2f;
        boxSize = new Vector2(attackRange, attackRange);
        boxAngle = 0f;

        RaycastHit2D[] hits = Physics2D.BoxCastAll(boxCenter, boxSize, boxAngle, attacker.right, attackRange);

        Elemental elementalDamage = crystal_Mauler.GetMeleeAttackDMG();
        foreach (RaycastHit2D hit in hits)
        {
            GameObject hitTarget = hit.collider.gameObject;
            if (hit.transform.root.TryGetComponent<IDamageable>(out var damageable) && hitTarget.CompareTag("Hitbox")
                && elementalDamage.targetLayer == (1 << hitTarget.layer))
            {
                damageable.TakeDamage(elementalDamage);
            }
        }
    }
   

}
