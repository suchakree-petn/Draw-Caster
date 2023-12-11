using System.Collections;
using System.Collections.Generic;
using MoltenFire;
using UnityEngine;

public class CheckFlamePillarAttack : MonoBehaviour
{
    [SerializeField] private MoltenFireBehaviour moltenFireBehaviour;
    [SerializeField] private float activeDuration;

    private void OnEnable()
    {
        Invoke("GoWandering", 2);
    }
    private void OnDisable()
    {
        Debug.Log("Here!!");
        moltenFireBehaviour.CoolDownFlamePillar(activeDuration, gameObject);
    }
    private void Awake()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.root.CompareTag(moltenFireBehaviour.targetTag) && other.CompareTag("Hitbox"))
        {
            moltenFireBehaviour.currentState = State.FlamePillarAttack;

            gameObject.SetActive(false);

        }

    }
    private void GoWandering()
    {
        moltenFireBehaviour.currentState = State.Wandering;
        gameObject.SetActive(false);

    }
}
