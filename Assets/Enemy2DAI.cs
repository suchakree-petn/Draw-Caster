using System.Collections;
using UnityEngine;

public class Enemy2DAI : MonoBehaviour
{
    public enum State
    {
        Idle,
        Patrol,
        Chase
    }

    public State currentState;
    public Transform target; // The player or any other target
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    public float speed = 2.0f;

    private void Start()
    {
        currentState = State.Idle;

        // Start the coroutine that waits 2 seconds and then caches the player's transform
        StartCoroutine(CachePlayerAfterDelay());
    }

    private void Update()
    {
        if (target == null) return;

        switch (currentState)
        {
            case State.Idle:
                IdleUpdate();
                break;
            case State.Patrol:
                PatrolUpdate();
                break;
            case State.Chase:
                ChaseUpdate();
                break;
        }
    }

    private IEnumerator CachePlayerAfterDelay()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2.0f);

        // Cache the player's transform after the delay
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("No GameObject with the tag 'Player' was found.");
        }
    }
    void IdleUpdate()
    {
        // Check for some condition to change state, like distance to player.
        if (Vector2.Distance(target.position, transform.position) < 5f) // arbitrary distance
        {
            currentState = State.Chase;
        }
        else
        {
            currentState = State.Patrol;
        }
    }

    void PatrolUpdate()
    {
        if (patrolPoints.Length == 0) return;

        // Move towards the current patrol point
        Vector2 direction = patrolPoints[currentPatrolIndex].position - transform.position;
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPatrolIndex].position, step);

        if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.1f)
            GoToNextPatrolPoint();

        // Check for some condition to change state, like distance to player.
        if (Vector2.Distance(target.position, transform.position) < 5f)
        {
            currentState = State.Chase;
        }
    }

    void ChaseUpdate()
    {
        // Move towards the target
        Vector2 direction = target.position - transform.position;
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);

        // Condition to stop chasing. For this example, let's use a distance check.
        if (Vector2.Distance(target.position, transform.position) > 10f) // arbitrary distance
        {
            currentState = State.Patrol;
        }
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }
}
