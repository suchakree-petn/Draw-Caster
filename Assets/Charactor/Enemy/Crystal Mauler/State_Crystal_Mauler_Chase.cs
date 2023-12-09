using UnityEngine;

public class State_Crystal_Mauler_Chase : StateMachineBehaviour
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
        CheckAttack(crystal_Mauler, animator);
    }

    private void CheckAttack(Crystal_Mauler crystal_Mauler, Animator animator)
    {
        if (crystal_Mauler.target == null)
        {
            return;
        }

        if (crystal_Mauler.IsTargetInRange())
        {
            animator.SetTrigger("Attack");
        }
    }
}
