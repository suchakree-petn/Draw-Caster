using DrawCaster.Util;
using UnityEngine;

public class State_Crystal_Mauler_Knockback : StateMachineBehaviour
{
    [SerializeField] private Crystal_Mauler crystal_Mauler;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (crystal_Mauler == null)
        {
            crystal_Mauler = animator.GetComponent<Crystal_Mauler>();
        }
        crystal_Mauler.SetZeroVelocity();

        Vector2 dir = DrawCasterUtil.GetDirectionFromLower(animator.transform, crystal_Mauler.target);
        crystal_Mauler.crystalMaulerRb.AddForce(-1 * crystal_Mauler.enemyKnockbackDistance * dir.normalized);

    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        crystal_Mauler.SetZeroVelocity();
        crystal_Mauler.isKnockback = false;

    }

}
