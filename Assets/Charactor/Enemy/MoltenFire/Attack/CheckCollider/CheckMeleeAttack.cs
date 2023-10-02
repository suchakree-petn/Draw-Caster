using System.Collections;
using System.Collections.Generic;
using MoltenFire;
using UnityEngine;

public class CheckMeleeAttack : MonoBehaviour
{
    [SerializeField] private MoltenFireBehaviour moltenFireBehaviour;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float activeDuration;

    private void Awake()
    {
        moltenFireBehaviour = transform.root.GetComponentInChildren<MoltenFireBehaviour>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.root.CompareTag(moltenFireBehaviour.targetTag) && other.CompareTag("Hitbox"))
        {
            animator.SetBool("IsWalk", false);
            moltenFireBehaviour.moveSpeed = moltenFireBehaviour.moltenFireData._moveSpeed;
            gameObject.SetActive(false);
            moltenFireBehaviour.currentState = State.MeleeAttack;

        }
    }
    private void Update()
    {
        moltenFireBehaviour.FacingToTarget(moltenFireBehaviour.target);
        animator.SetBool("IsWalk", true);
        animator.SetBool("IsWalkBackward", false);
        moltenFireBehaviour.moveSpeed = moveSpeed;
        animator.speed = moveSpeed;
    }
    private void OnEnable()
    {
        StartCoroutine(ActiveDuration());

    }
    private void OnDisable()
    {
        animator.speed = 1;
    }
    IEnumerator ActiveDuration()
    {
        yield return new WaitForSeconds(activeDuration);
        moltenFireBehaviour.currentState = State.Wandering;

        gameObject.SetActive(false);
    }
}
