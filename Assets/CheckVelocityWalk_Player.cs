using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckVelocityWalk_Player : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerAction playerAction = PlayerInputSystem.Instance.playerAction;
        Vector2 movement = playerAction.Player.Movement.ReadValue<Vector2>();
        if (movement != Vector2.zero)
        {
            if (movement.x < 0)
            {
                animator.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                animator.GetComponent<SpriteRenderer>().flipX = false;

            }
            animator.SetFloat("Velocity", 0.2f);
        }
        else
        {
            animator.SetFloat("Velocity", 0f);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
