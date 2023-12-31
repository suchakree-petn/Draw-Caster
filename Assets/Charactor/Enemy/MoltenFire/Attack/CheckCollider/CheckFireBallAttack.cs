using System.Collections;
using System.Collections.Generic;
using MoltenFire;
using UnityEngine;

public class CheckFireBallAttack : MonoBehaviour
{
    [SerializeField] private MoltenFireBehaviour moltenFireBehaviour;
    [SerializeField] private float activeDuration;
    private void OnEnable()
    {
        StartCoroutine(ActiveDuration());
    }
    private void Awake()
    {
        moltenFireBehaviour = transform.root.GetComponentInChildren<MoltenFireBehaviour>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.root.CompareTag(moltenFireBehaviour.targetTag) && other.CompareTag("Hitbox"))
        {
            moltenFireBehaviour.currentState = State.FireBallAttack;
            gameObject.SetActive(false);

        }
    }
    IEnumerator ActiveDuration()
    {
        yield return new WaitForSeconds(activeDuration);
        moltenFireBehaviour.currentState = State.Wandering;
        Debug.Log(moltenFireBehaviour.currentState);
        gameObject.SetActive(false);
    }
}
