using UnityEngine;

public class State_Crystal_Mauler_Attack : StateMachineBehaviour
{
    [SerializeField] private Crystal_Mauler crystal_Mauler;
    private float timer;
    private bool isAttacked;
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
            PerformMeleeAttack(animator.transform, crystal_Mauler.Detect_range, crystal_Mauler);
        }
    }

    public void PerformMeleeAttack(Transform attacker, float attackRange, Crystal_Mauler crystal_Mauler)
    {
        Vector2 boxCenter = (Vector2)attacker.position + (Vector2)attacker.right * attackRange / 2f;
        Vector2 boxSize = new Vector2(attackRange, attackRange);
        float boxAngle = 0f;

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
