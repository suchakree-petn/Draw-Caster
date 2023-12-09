using UnityEngine;

public class State_Crystal_Mauler_Idle : StateMachineBehaviour
{
    [SerializeField] private Crystal_Mauler crystal_Mauler;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (crystal_Mauler == null)
        {
            crystal_Mauler = animator.GetComponent<Crystal_Mauler>();
        }
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        CheckTarget(crystal_Mauler.target, animator);
    }

    private void CheckTarget(Transform target, Animator animator)
    {
        if (target == null)
        {
            animator.SetBool("isChase", false);
            return;
        }

        animator.SetBool("isChase", true);
    }
}
