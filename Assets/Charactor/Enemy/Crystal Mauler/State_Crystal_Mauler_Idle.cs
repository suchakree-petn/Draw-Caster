using DrawCaster.Util;
using UnityEngine;

public class State_Crystal_Mauler_Idle : StateMachineBehaviour
{
    [SerializeField] private Crystal_Mauler crystal_Mauler;
    [SerializeField] private float timer;
    [SerializeField] private float attackDelay;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (crystal_Mauler == null)
        {
            crystal_Mauler = animator.GetComponent<Crystal_Mauler>();
        }
        timer = 0;
        attackDelay = Random.Range(2f, 4f);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        CheckTarget(crystal_Mauler.target, animator);
    }

    private void CheckTarget(Transform target, Animator animator)
    {
        if (target == null)
        {
            animator.SetBool("isChase", false);
            return;
        }
        Transform crystalTrans = DrawCasterUtil.GetMidTransformOf(animator.transform);
        Transform targetTrans = DrawCasterUtil.GetMidTransformOf(target);
        if (crystal_Mauler.Detect_range < Vector2.Distance(targetTrans.position, crystalTrans.position))
        {
            animator.SetBool("isChase", true);
        }
        else
        {
            if (timer >= attackDelay)
            {
                animator.SetTrigger("Attack");
                timer = 0;
                attackDelay = Random.Range(2f, 4f);
            }
        }
    }
}
