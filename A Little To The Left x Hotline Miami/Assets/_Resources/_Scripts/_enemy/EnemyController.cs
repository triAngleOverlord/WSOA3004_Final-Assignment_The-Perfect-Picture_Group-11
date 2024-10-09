using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enemyState baseState;
    public enum enemyState { idle, inspect, attack, dead }

    public idleStates state;
    public enum idleStates { patrol, roamer, staticState }

    private NavMeshAgent agent;
    Transform player;

    private Rigidbody2D rb;

    //Patrol state
    [SerializeField] private List<Transform> patrolPoints = new List<Transform>();
    private Transform target;
    private int pointIndex = 0;

    //Roaming state
    private Vector3 randomPosition;
    private float randomWaitTime = 5f;
    private float waitTimeUpdate;

    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private float detectionDistance;

    //static idle state
    private Vector2 originalPos;

    //Inspect
    private Vector2 siteToInspect;
    private EnemyVision enemyVision;

    private enemyState currentState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        randomPosition = transform.position + new Vector3(Random.Range(-7, 7f), Random.Range(-7, 7f), 0);
        baseState = enemyState.idle;

        currentState = enemyState.idle;

        originalPos = transform.position;
        enemyVision = FindObjectOfType<EnemyVision>();
    }

    void Update()
    {
        EnemyLife();

        Vector3 targetDir = agent.velocity;
        float newDir = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;

        if (agent.velocity != Vector3.zero)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, newDir));
        }
    }

    private void EnemyLife()
    {
        if (baseState == enemyState.idle)
        {
            if (state == idleStates.patrol)
            {
                Patrol();
            }
            else if (state == idleStates.roamer)
            {
                Roam();
            }
            else if (state == idleStates.staticState)
            {
                StaticIdle();
            }
        }
        else if (baseState == enemyState.inspect)
        {
            Inspect();
        }
        else if (baseState == enemyState.attack)
        {

        }
        else if (baseState == enemyState.dead)
        {

        }
    }

    private void Patrol()
    {
        target = patrolPoints[pointIndex];
        if (target != null)
        {
            agent.SetDestination(target.position);
        }

        if (Vector3.Distance(transform.position, patrolPoints[pointIndex].position) < 0.1f)
        {
            pointIndex = (pointIndex + 1) % patrolPoints.Count;
        }
    }

    private void Roam()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, detectionDistance, obstacleMask);
        Debug.DrawRay(transform.position, transform.up * detectionDistance, Color.green);

        if (hit.collider != null)
        {
            Vector3 deflectionDir = Vector3.Cross(transform.up, transform.forward).normalized;

            randomPosition = transform.position + deflectionDir * Random.Range(2, 5);
            agent.SetDestination(randomPosition);
            return;
        }
        else
        {
            waitTimeUpdate += Time.deltaTime;

            if (waitTimeUpdate > randomWaitTime)
            {
                if (Vector3.Distance(transform.position, randomPosition) < 0.1f)
                {
                    waitTimeUpdate = 0;

                    randomPosition = transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
                }
            }
        }

        agent.SetDestination(randomPosition);
    }

    private void StaticIdle()
    {
        if (Vector3.Distance(transform.position, originalPos) > 0.01f)
        {
            agent.SetDestination(originalPos);
        }
    }

    private void Inspect()
    {

    }

    private void BackToWork()
    {

    }
}

