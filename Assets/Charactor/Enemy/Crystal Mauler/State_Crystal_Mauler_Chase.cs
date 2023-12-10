using DrawCaster.Util;
using UnityEngine;
using DG.Tweening;
public class State_Crystal_Mauler_Chase : StateMachineBehaviour
{
    [SerializeField] private Crystal_Mauler crystal_Mauler;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (crystal_Mauler == null)
        {
            crystal_Mauler = animator.GetComponent<Crystal_Mauler>();
        }
        crystal_Mauler.canMove = true;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        CheckAttack(crystal_Mauler, animator);

        Transform target = DrawCasterUtil.GetMidTransformOf(crystal_Mauler.target);
        CheckFaceDir(animator.transform, target.position);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetBool("isChase", false);
        crystal_Mauler.canMove = false;

    }

    private void CheckFaceDir(Transform crystalMauler, Vector2 targetPos)
    {
        Vector2 crystalMaulerPos = crystalMauler.position;
        if (crystalMaulerPos.x > targetPos.x)
        {
            crystal_Mauler.transform.DORotate(new Vector3(0, -180, 0), 0);
        }
        else
        {
            crystal_Mauler.transform.DORotate(new Vector3(0, 0, 0), 0);
        }
    }
    private void CheckAttack(Crystal_Mauler crystal_Mauler, Animator animator)
    {
        if (crystal_Mauler.target == null)
        {
            return;
        }
        if (crystal_Mauler.IsTargetInRange())
        {
            animator.SetBool("isChase", false);
            animator.SetTrigger("Attack");
        }

    }
}
