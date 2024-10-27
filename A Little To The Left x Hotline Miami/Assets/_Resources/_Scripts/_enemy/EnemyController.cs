using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public EnemyWeapon weapon;
    [SerializeField] private Transform weaponPos;
    [SerializeField] private GameObject theWeapon;
    [SerializeField] private List<GameObject> weaponInRange = new List<GameObject>();
    [SerializeField] private float range;
    [SerializeField] private LayerMask weaponMask;
    private bool hasWeapon;

    public enum EnemyWeapon { melee, range }

    public enemyState baseState;
    public enum enemyState { idle, inspect, attack, dead, knockedDown, lookForWeapon }

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
    private Vector2 siteToInspect;
    private EnemyVision enemyVision;


    //vars from the enemy vision script, now here
    public float viewRadius;
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public List<Transform> foundTargets = new List<Transform>();

    public float timeBeforeForget = 10f;
    public bool hasSeenPlayer;


    private idleStates currentState;
    [SerializeField] private float searchDuration;

    [SerializeField] private GameObject aliveSprite;
    [SerializeField] private GameObject kiaSprite;
    [SerializeField] private GameObject knockedOutSprite;

    [SerializeField] private float randomDist;
    private float defaultRandomDist;


    public static bool isOverMe;
    public static GameObject me;

    [SerializeField] private LayerMask pathClearance;
    [SerializeField] private float pathCheckDist;

    private AiWeapon enemyAiWeapon;
    private WeaponAttributes weaponAttributes;


    //rangeAttackStyle
    [SerializeField] private Transform prefabSpawnPos;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float projectileSpeed;

    //melleAttackStyle
    [SerializeField] private Transform hitPos;
    [SerializeField] private float hitRadius;
    [SerializeField] private LayerMask enemyMask;
    private PlayerInteraction playerInteraction;

    void Start()
    {
        SpriteManager(enemyState.idle);

        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        randomPosition = transform.position + new Vector3(Random.Range(-7, 7f), Random.Range(-7, 7f), 0);

        currentState = state;

        originalPos = transform.position;
        enemyVision = FindObjectOfType<EnemyVision>();

        //enemy vision
        StartCoroutine(FindTarget());
        hasSeenPlayer = false;

        randomDist = 10;
        defaultRandomDist = randomDist;

        hasWeapon = false;
        baseState = enemyState.lookForWeapon;

        playerInteraction = player.GetComponent<PlayerInteraction>();
    }

    void Update()
    {
        EnemyLife();
        HasSeenPlayer();
        FaceWhereverYoureHeaded();
        Weapons();
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        Gizmos.color = Color.blue;
        if (weaponInRange.Count > 0)
        {
            foreach (var weapon in weaponInRange)
            {
                Gizmos.DrawLine(transform.position, weapon.transform.position);
            }
        }
    }

    private void Weapons()
    {
        weaponInRange.Clear();
        Collider2D[] weaponCollider = Physics2D.OverlapCircleAll(transform.position, range, weaponMask);
        if (weaponCollider.Length > 0)
        {
            foreach (var weapon in weaponCollider)
            {
                weaponInRange.Add(weapon.gameObject);
            }
        }

        if (theWeapon != null)
        {
            if (theWeapon.tag == "Ranged")
            {
                weapon = EnemyWeapon.range;
            }
            else if (theWeapon.tag == "Melee")
            {
                weapon = EnemyWeapon.melee;
            }

            theWeapon.transform.SetParent (weaponPos);
            theWeapon.transform.localPosition = Vector3.zero;
            theWeapon.transform.localRotation = Quaternion.identity;

            int layerName = LayerMask.NameToLayer("Fallback");
            theWeapon.gameObject.layer = layerName;

            if (weapon == EnemyWeapon.melee && theWeapon.GetComponent<MeleeSys>()!= null)
            {
                theWeapon.gameObject.GetComponent<MeleeSys>().enabled = false;
            }

            else if (weapon == EnemyWeapon.range && theWeapon.GetComponent<MeleeSys>()!= null)
            {
                theWeapon.gameObject.GetComponent<Gun>().enabled = false;
            }
        }
    }

    private void ClearChildren(GameObject parent)
    {
        foreach (Transform kid in parent.transform)
        {
            if (weapon == EnemyWeapon.range)
            {
                kid.gameObject.GetComponent<Gun>().enabled = true;
            }
            else if (weapon == EnemyWeapon.melee)
            {
                kid.gameObject.GetComponent<MeleeSys>().enabled = true;
            }

            DestroyImmediate(kid.gameObject.GetComponent<AiWeapon>());
            weaponAttributes = null;
            int layerName = LayerMask.NameToLayer("Weapons");
            kid.gameObject.layer = layerName;
            kid.SetParent(null);
        }
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
                }
            }
        }
    }

    private void HasSeenPlayer()
    {
        if (hasWeapon)
        {
            if (foundTargets.Count > 0)
            {
                baseState = enemyState.inspect;
                soundState = inspectStates.sight;
                siteToInspect = player.position;
                hasSeenPlayer = true;
                Debug.DrawLine(transform.position, siteToInspect, Color.green);
                if (player != null && enemyAiWeapon != null)
                {
                    if (weapon == EnemyWeapon.range)
                    {
                        if (Vector2.Distance(transform.position, player.position) < 20f)
                        {
                            enemyAiWeapon.RangeStyle(weaponAttributes.projectilePrefab, weaponAttributes.spawnPoint, weaponAttributes.amountOfBullets, weaponAttributes.spread ,weaponAttributes.speed, weaponAttributes.timeBeforeNextShot);
                        }
                    }
                    else if (weapon == EnemyWeapon.melee)
                    {
                        if (Vector2.Distance(transform.position, player.position) < 2f)
                        {
                            enemyAiWeapon.MeleeStyle(hitPos, hitRadius, enemyMask, weaponAttributes.timeBeforeNextSlash);
                            if(playerInteraction != null)
                            {
                                playerInteraction.StatusUpdate();
                            }
                        }
                    }
                }
            }
        }
    }

    public void HearSound(Vector2 soundSite)
    {
        if (hasWeapon)
        {
            baseState = enemyState.inspect;
            soundState = inspectStates.sound;
            siteToInspect = soundSite;
        }
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

        else if (baseState == enemyState.lookForWeapon)
        {
            LookForWeapon();
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
            Vector2 pathDir = (target.transform.position - transform.position).normalized;

            RaycastHit2D hit = AllRaycast2D(pathDir, pathCheckDist, pathClearance, Color.red);

            Debug.DrawRay(transform.position, pathDir * pathCheckDist, Color.red);

            if (hit.collider != null)
            {
                pointIndex = (pointIndex + 1) % patrolPoints.Count;
                return;
            }

            GoToDestination(target.position);
        }

        if (Vector3.Distance(transform.position, patrolPoints[pointIndex].position) < 0.1f)
        {
            pointIndex = (pointIndex + 1) % patrolPoints.Count;
        }
    }

    private RaycastHit2D AllRaycast2D(Vector3 dir, float dist, LayerMask mask, Color rayColor)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, dist, mask);
        Debug.DrawRay(transform.position, dir * dist, rayColor);
        return hit;
    }

    private void Roam()
    {
        RaycastHit2D hit = AllRaycast2D(transform.up, detectionDistance, obstacleMask, Color.green);

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


    private void Dead()
    {
        SpriteManager(enemyState.dead);

        Vector2 fallDir = (player.position - transform.position).normalized;
        rb.transform.up = fallDir;
        theWeapon = null;
        ClearChildren(weaponPos.gameObject);
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject.GetComponent<NavMeshAgent>());
        Destroy(gameObject.GetComponent<EnemyController>());
        Destroy(gameObject.GetComponent<Collider2D>());
    }

    private void KnockedDown()
    {
        agent.enabled = false;

        SpriteManager(enemyState.knockedDown);
        hasWeapon = false;

        theWeapon = null;
        ClearChildren(weaponPos.gameObject);
        StartCoroutine(WakeUp());
    }

    private IEnumerator WakeUp()
    {
        yield return new WaitForSeconds(5);
        agent.enabled = true;
        SpriteManager(enemyState.idle);
        if (!hasWeapon)
        {
            baseState = enemyState.lookForWeapon;
        }
        hasSeenPlayer = false;
        foundTargets.Clear();
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
        if (agent != null && agent.enabled)
        {
            agent.SetDestination(destination);
        }
    }

    private void SpriteManager(enemyState currentSate)
    {
        if (kiaSprite != null && aliveSprite != null && knockedOutSprite != null)
        {
            if (currentSate == enemyState.knockedDown)
            {
                kiaSprite.SetActive(false);
                aliveSprite.SetActive(false);
                knockedOutSprite.SetActive(true);
            }
            else if (currentSate == enemyState.dead)
            {
                kiaSprite.SetActive(true);
                aliveSprite.SetActive(false);
                knockedOutSprite.SetActive(false);
            }
            else
            {
                kiaSprite.SetActive(false);
                aliveSprite.SetActive(true);
                knockedOutSprite.SetActive(false);
            }
        }
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

    private void LookForWeapon()
    {
        if (weaponInRange.Count > 0)
        {
            float closestDistance = Mathf.Infinity;
            Transform closestElement = null;

            foreach (var weapon in weaponInRange)
            {
                float distance = Vector2.Distance(transform.position, weapon.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestElement = weapon.transform;
                }
            }

            GameObject nearestWeapon = closestElement.gameObject;
            GoToDestination(nearestWeapon.transform.position);

            if (Vector2.Distance(transform.position, nearestWeapon.transform.position) < 0.5f)
            {
                theWeapon = nearestWeapon;
                hasWeapon = true;
                baseState = enemyState.idle;
                enemyAiWeapon = theWeapon.gameObject.AddComponent<AiWeapon>();
                weaponAttributes = theWeapon.GetComponent<WeaponAttributes>();

                if(weaponAttributes != null )
                {
                    weaponAttributes.AnnounceSelf();
                }

                if (theWeapon.tag == "Ranged")
                {
                    enemyAiWeapon.type = AiWeapon.WeaponType.range;
                }
                else if (theWeapon.tag == "Melee")
                {
                    enemyAiWeapon.type = AiWeapon.WeaponType.melee;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            baseState = enemyState.dead;
        }
    }
}

