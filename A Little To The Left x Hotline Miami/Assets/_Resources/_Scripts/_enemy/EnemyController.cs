using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enemyState baseState;
    public enum enemyState { idle, inspect, attack, dead, knockedDown }

    public idleStates state;
    public enum idleStates { patrol, roamer, staticState }

    public inspectStates soundState;
    public enum inspectStates { sight, sound }
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
    public Vector2 siteToInspect;
    private EnemyVision enemyVision;
    private float inspectionTime = 2f;


    //vars from the enemy vision script, now here
    public float viewRadius;
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public List<Transform> foundTargets = new List<Transform>();
    private float timeSinceLastSeenPlayer = 0f;
    public float timeBeforeForget = 10f;
    public bool hasSeenPlayer;

    private bool canShootPlayer;

    private idleStates currentState;
    [SerializeField] private float searchDuration;

    [SerializeField] private GameObject aliveSprite;
    [SerializeField] private GameObject kiaSprite;

    [SerializeField] private float randomDist;
    private float defaultRandomDist;


    public static bool isOverMe;
    public static GameObject me;

    void Start()
    {
        kiaSprite.SetActive(false);
        aliveSprite.SetActive(true);

        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        randomPosition = transform.position + new Vector3(Random.Range(-7, 7f), Random.Range(-7, 7f), 0);
        baseState = enemyState.idle;

        currentState = state;

        originalPos = transform.position;
        enemyVision = FindObjectOfType<EnemyVision>();


        //enemy vision
        StartCoroutine(FindTarget());
        hasSeenPlayer = false;

        randomDist = 10;
        defaultRandomDist = randomDist;
    }

    void Update()
    {
        EnemyLife();
        if (baseState == enemyState.dead) return;
        HasSeenPlayer();
        FaceWhereverYoureHeaded();
    }

    public void OnMouseOver()
    {
        isOverMe = true;
        me = gameObject;
    }

    public void OnMouseExit()
    {
        isOverMe = false;
    }

    private void FaceWhereverYoureHeaded()
    {
        Vector3 targetDir = agent.velocity;
        float newDir = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;

        if (agent.velocity != Vector3.zero)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, newDir));
        }
    }

    private IEnumerator FindTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(.2f);
            FOV();
        }
    }

    private void FOV()
    {
        foundTargets.Clear();

        Collider2D[] visibleTargets = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        for (int i = 0; i < visibleTargets.Length; i++)
        {
            Transform target = visibleTargets[i].transform;
            Vector2 dirToTarget = (target.position - transform.position).normalized;
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            if (Vector2.Angle(transform.up, dirToTarget) < viewAngle / 2f && distanceToTarget <= viewRadius)
            {
                if (!Physics2D.Raycast(transform.position, dirToTarget, distanceToTarget, obstructionMask))
                {
                    Debug.DrawLine(transform.position, target.position, Color.red);
                    foundTargets.Add(target);
                    canShootPlayer = true;
                }
                else
                {
                    canShootPlayer = false;
                }
            }
        }
    }

    private void HasSeenPlayer()
    {
        if (foundTargets.Count > 0)
        {
            baseState = enemyState.inspect;
            soundState = inspectStates.sight;
            siteToInspect = foundTargets[0].position;
            timeSinceLastSeenPlayer = 0f;
            hasSeenPlayer = true;
        }
    }

    public void HearSound(Vector2 soundSite)
    {
        baseState = enemyState.inspect;
        soundState = inspectStates.sound;
        siteToInspect = soundSite;
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool isAngleGlobal)
    {
        if (!isAngleGlobal)
        {
            angleInDegrees -= transform.eulerAngles.z;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
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
            Kill();
        }

        else if (baseState == enemyState.knockedDown)
        {
            KnockedDown();
        }

        else if (baseState == enemyState.dead)
        {
            Dead();
        }
    }

    private void Patrol()
    {
        target = patrolPoints[pointIndex];
        if (target != null)
        {
            GoToDestination(target.position);
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
            GoToDestination(randomPosition);
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

                    randomPosition = transform.position + new Vector3(Random.Range(-randomDist, randomDist), Random.Range(-randomDist, randomDist), 0);
                }
            }
        }

        GoToDestination(randomPosition);
    }

    private void StaticIdle()
    {
        if (Vector3.Distance(transform.position, originalPos) > 0.01f)
        {
            GoToDestination(originalPos);
        }
    }

    private void Kill()
    {
        // here we will check the type of weapon the enemy has equipped
        //  if it's ranged, we will calculate the distance from which the enemy can shoot
        //else if it's melee, we will also calculate an attack range then make it attack

        if (hasSeenPlayer)
        {
            if (Vector3.Distance(transform.position, player.position) < .5f)
            {
                print("shot");
            }
            else
            {
                print("Target on sight but far");
            }
        }
    }

    private void Dead()
    {
        if (aliveSprite != null && kiaSprite != null)
        {
            aliveSprite.SetActive(false);
            kiaSprite.SetActive(true);
        }

        Vector3 fallDir = (player.position - transform.position).normalized;
        rb.transform.up = fallDir;

        Destroy(gameObject.GetComponent<NavMeshAgent>());
        Destroy(gameObject.GetComponent<EnemyController>());
        Destroy(gameObject.GetComponent<Collider2D>());
    }

    private void KnockedDown()
    {

    }


    private void Inspect()
    {
        GoToDestination(siteToInspect);

        if (soundState == inspectStates.sight)
        {
            if (Vector2.Distance(transform.position, siteToInspect) < 0.1f)
            {
                baseState = enemyState.idle;
            }
        }
        else if (soundState == inspectStates.sound)
        {
            if (Vector2.Distance(transform.position, siteToInspect) < 0.1f)
            {
                StartCoroutine(SearchTime());
            }
        }
    }

    private void GoToDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    private IEnumerator SearchTime()
    {
        float timeSearched = 0f;

        while (timeSearched < searchDuration)
        {
            yield return null;
            baseState = enemyState.idle;
            state = idleStates.roamer;
            randomDist = 3f;
            timeSearched += Time.deltaTime;
        }

        state = currentState;
        randomDist = defaultRandomDist;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            baseState = enemyState.dead;
        }
    }
}

