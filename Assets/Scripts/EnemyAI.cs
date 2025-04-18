using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Patrolling,
    Chasing,
    Fleeing,
    Searching,
    Returning
}

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public Transform[] waypoints;
    public float viewRadius = 15f;
    public float viewAngle = 90f;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float health = 100;

    private Renderer enemyRenderer;
    public Color normalColor = Color.white;
    public Color lowHealthColor = Color.red;

    private EnemyState currentState;
    private int waypointIndex = 0;
    private Vector3 lastSeenPosition;
    private Vector3 originalPosition;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        enemyRenderer = GetComponent<Renderer>();
        if (enemyRenderer != null)
        {
            enemyRenderer.material.color = normalColor;
        }

        if (!agent.isOnNavMesh)
        {
            Debug.LogError("El enemigo no est� sobre la NavMesh. Aseg�rate de haber horneado (baked) el NavMesh.");
            return;
        }

        originalPosition = transform.position;
        currentState = EnemyState.Patrolling;
        Patrol();
    }

    void Update()
    {
        CheckHealth();

        if (PlayerDetected())
        {
            if (health <= 50)
            {
                currentState = EnemyState.Fleeing;
            }
            else
            {
                currentState = EnemyState.Chasing;
            }
        }

        switch (currentState)
        {
            case EnemyState.Patrolling:
                Patrol();
                break;
            case EnemyState.Chasing:
                Chase();
                break;
            case EnemyState.Fleeing:
                Flee();
                break;
            case EnemyState.Searching:
                Search();
                break;
            case EnemyState.Returning:
                ReturnToPosition();
                break;
        }
    }

    void Patrol()
    {
        if (!agent.isActiveAndEnabled || !agent.isOnNavMesh) return;

        if (!agent.hasPath || agent.remainingDistance < 1f)
        {
            agent.SetDestination(waypoints[waypointIndex].position);
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
        }
    }

    void Chase()
    {
        if (!agent.isActiveAndEnabled || !agent.isOnNavMesh) return;

        if (PlayerDetected())
        {
            agent.SetDestination(player.position);
            lastSeenPosition = player.position;
            if (Vector3.Distance(transform.position, player.position) < 0.5f)
            {
                EndGame();
            }
        }
        else
        {
            currentState = EnemyState.Searching;
        }
    }

    void Flee()
    {
        if (!agent.isActiveAndEnabled || !agent.isOnNavMesh) return;

        Vector3 fleeDirection = (transform.position - player.position).normalized; 
        Vector3 fleePosition = transform.position + fleeDirection * 2; 

        agent.SetDestination(fleePosition); 
        if (Vector3.Distance(transform.position, player.position) > 1)
        {
            currentState = EnemyState.Patrolling;
            Patrol();
        }
    }

    void Search()
    {
        if (!agent.isActiveAndEnabled || !agent.isOnNavMesh) return;

        agent.SetDestination(lastSeenPosition);

        if (Vector3.Distance(transform.position, lastSeenPosition) < 2f)
        {
            currentState = EnemyState.Returning;
        }
    }

    void ReturnToPosition()
    {
        if (!agent.isActiveAndEnabled || !agent.isOnNavMesh) return;

        agent.SetDestination(originalPosition);

        if (Vector3.Distance(transform.position, originalPosition) < 1f)
        {
            currentState = EnemyState.Patrolling;
            Patrol();
        }
    }

    void CheckHealth()
    {
        if (health <= 50 && enemyRenderer != null)
        {
            enemyRenderer.material.color = lowHealthColor;
        }

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
        Time.timeScale = 0;
        UIController.instance.PlayerWin();

    }

    bool PlayerDetected()
    {
        Collider[] playersInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);
        foreach (Collider col in playersInRange)
        {
            Transform target = col.transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    void EndGame()
    {
        UIController.instance.AIWin();
        Time.timeScale = 0; 
    }
}
