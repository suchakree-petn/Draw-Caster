
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
            animator.SetFloat("Velocity", 0.2f);
        }
        else
        {
            animator.SetFloat("Velocity", 0f);
        }
    }
    
}
