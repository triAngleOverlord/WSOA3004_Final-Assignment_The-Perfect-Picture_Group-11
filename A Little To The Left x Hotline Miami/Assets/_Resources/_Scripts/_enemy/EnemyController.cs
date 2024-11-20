using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using static Unity.VisualScripting.Member;

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
    private float originalRotation;

    //Inspect
    private Vector2 siteToInspect;
    private EnemyVision enemyVision;

    private ScoreDirectionSystem SCDirection;

    private ScorTrack SCTrack;
    public string deathBy;
    public string knife= "Knife";


    //vars from the enemy vision script, now here
    public float viewRadius;
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public List<Transform> foundTargets = new List<Transform>();

    public float timeBeforeForget = 10f;
    private bool hasSeenPlayer;

    private bool hasHeardPlayer;

    private idleStates currentState;
    [SerializeField] private float searchDuration;

    [SerializeField] private GameObject aliveSprite;
    [SerializeField] private GameObject kiaSprite;
    [SerializeField] private GameObject knockedOutSprite;
    private differentDeadBodies deadBodySprites;

    [SerializeField] private float randomDist;
    private float defaultRandomDist;


    public static bool isOverMe;
    public static GameObject me;

    [SerializeField] private LayerMask pathClearance;
    [SerializeField] private float pathCheckDist;

    private WeaponAttributes weaponAttributes;

    public Transform cast;
    [SerializeField] private Transform meleeAttackRangePos;

    //anim
    [SerializeField] private Animator[] motionState;


    public bool isdeadalready = false;

    //path
    private NavMeshPath path;
    private bool intel;

    //sfx and music

    AudioSource weaponSFx;
    AudioSource shotgunSFx;


    void Start()
    {

        SCDirection = this.gameObject.GetComponent<ScoreDirectionSystem>();
        SCTrack = this.gameObject.GetComponent<ScorTrack>();

        weaponSFx = transform.Find("_sfx").Find("_weaponSFx").GetComponent<AudioSource>();
        shotgunSFx = transform.Find("_sfx").Find("_shotgunSFx").GetComponent<AudioSource>();

        viewRadius = 8f;
        viewAngle = 170f;

        SpriteManager(enemyState.idle);

        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.updatePosition = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        path = new NavMeshPath();

        randomPosition = transform.position + new Vector3(Random.Range(-7, 7f), Random.Range(-7, 7f), 0);

        currentState = state;

        originalPos = transform.position;
        originalRotation = transform.rotation.eulerAngles.z;
        enemyVision = FindObjectOfType<EnemyVision>();

        //enemy vision
        StartCoroutine(FindTarget());
        hasSeenPlayer = false;

        randomDist = 10;
        defaultRandomDist = randomDist;

        hasWeapon = false;

        baseState = enemyState.lookForWeapon;

        deadBodySprites = Resources.Load<differentDeadBodies>("allDeadEnemyBodySprites");
    }

    void Update()
    {
        EnemyLife();
        if (baseState == enemyState.dead) return;
        HasSeenPlayer();
        FaceWhereverYoureHeaded();
        Weapons();
        CoolDown();
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

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(cast.position, detectionDistance);
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
                theWeapon.GetComponent<Animator>().enabled = true;
            }

            theWeapon.transform.SetParent(weaponPos);
            theWeapon.transform.localPosition = Vector3.zero;
            theWeapon.transform.localRotation = Quaternion.identity;
        }
    }

    private void ClearChildren(GameObject parent)
    {
        foreach (Transform kid in parent.transform)
        {
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

            if (motionState != null)
            {
                foreach (var anim in motionState)
                {
                    anim.SetFloat("movement", 1);
                }
            }
        }
        else
        {
            if (motionState != null)
            {
                foreach (var anim in motionState)
                {
                    anim.SetFloat("movement", 0);
                }
            }
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
            if (CheckPathStatus(target.position))
            {
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
    }

    #region Enemy Attack and Other....
    private void HasSeenPlayer()
    {
        if (hasWeapon)
        {
            if (foundTargets.Count > 0)
            {
                if (CheckPathStatus(player.position))
                {
                    baseState = enemyState.inspect;
                    soundState = inspectStates.sight;
                    siteToInspect = player.position;
                    hasSeenPlayer = true;
                    hasHeardPlayer = false;

                    Debug.DrawLine(transform.position, siteToInspect, Color.green);
                    if (player != null)
                    {
                        if (weapon == EnemyWeapon.range)
                        {
                            if (Vector2.Distance(transform.position, player.position) < 20f)
                            {
                            RangeStyle(weaponAttributes.projectilePrefab, weaponAttributes.spawnPoint, weaponAttributes.amountOfBullets, weaponAttributes.spread, weaponAttributes.speed, weaponAttributes.timeBeforeNextShot, weaponSFx, weaponAttributes.rangedSFx);
                            }
                        }
                        else if (weapon == EnemyWeapon.melee)
                        {
                            if (Vector2.Distance(transform.position, player.position) < 2f)
                            {
                            MeleeStyle(meleeAttackRangePos, transform, weaponAttributes.meleeAttackRadius, weaponAttributes.targetMask, weaponAttributes.meleeWaitTime, weaponAttributes.anim, weaponSFx, weaponAttributes.meleeSFx);
                            }
                        }
                    }
                }
            }
        }
    }

    private float untilNxtShot;
    private float timeUntilMelee;
    public void RangeStyle(GameObject projectilePrefab, Transform projectileSpawnPoint, float amountOfBullets, float spread, float speed, float timeBeforeNxtShot, AudioSource source, AudioClip clip)
    {
        if (untilNxtShot <= 0)
        {
            for (int i = 0; i < amountOfBullets; i++)
            {
                var randomRot = Random.Range(-spread, spread);
                Quaternion rot = Quaternion.Euler(0, 0, randomRot);
                GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation * rot);

                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                rb.AddForce(projectile.transform.up * speed, ForceMode2D.Impulse);

                source.clip = clip;
                source.Play();

                untilNxtShot = timeBeforeNxtShot;
                Destroy(projectile, 5f);
            }
        }
        else
        {
            untilNxtShot -= Time.deltaTime;
        }
    }

    
    internal void MeleeStyle(Transform attackPos, Transform me, float attackRadius, LayerMask mask, float waitTime, Animator anim, AudioSource source, AudioClip clip)
    {
        
        if (timeUntilMelee <= 0)
        {
            anim.SetTrigger("Attack");
            Collider2D targetCol = Physics2D.OverlapCircle(attackPos.position, attackRadius, mask);

            if (targetCol != null)
            {
                PlayerInteraction player = targetCol.GetComponent<PlayerInteraction>();
                if (player != null)
                {
                    Rigidbody2D enemyRB = targetCol.GetComponent<Rigidbody2D>();

                    Vector3 fallDir = (me.transform.position - targetCol.gameObject.transform.position).normalized;
                    float zAxis = Mathf.Atan2(fallDir.y, fallDir.x) * Mathf.Rad2Deg - 90f;
                    enemyRB.transform.rotation = Quaternion.Euler(0, 0, zAxis);

                    float power = 10f;
                    enemyRB.AddForce(-fallDir * power, ForceMode2D.Impulse);
                    enemyRB.drag = 5f;

                    player.StatusUpdate();

                }
            }

            source.clip = clip;
            source.Play();

            timeUntilMelee = waitTime;
        }
    }

    internal void CoolDown()
    {
        if (untilNxtShot>0)
        {

            if (weaponAttributes != null)
            {
                if (weaponAttributes.weaponName == "Shotgun")
                {
                    if (!shotgunSFx.isPlaying)
                    {
                        shotgunSFx.Play();
                    }
                }
            }

            untilNxtShot -= Time.deltaTime;
        }

        if (timeUntilMelee > 0.000000001f)
        {
            timeUntilMelee -= Time.deltaTime;
        }
    }

    #endregion

    public void HearSound(Vector2 soundSite)
    {
        if (hasWeapon)
        if (hasWeapon && CheckPathStatus(player.position))
        {
            baseState = enemyState.inspect;
            soundState = inspectStates.sound;
            siteToInspect = soundSite;
            hasHeardPlayer = true;
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
            if(isdeadalready == false)
            {
                if(this.gameObject.GetComponent<ScoreDirectionSystem>().stateofenemy == "DeadbyBull")
                {
                    //Debug.Log("IN HEREE");
                    SCTrack.DeathBYBULLET();
                    isdeadalready = true;
                     Dead();
                }
                if(this.gameObject.GetComponent<ScoreDirectionSystem>().stateofenemy == "DeadbyMelSwip")
                {
                    SCTrack.DeathBYMELSWIP();
                    isdeadalready = true;
                     Dead();
                }
                if(this.gameObject.GetComponent<ScoreDirectionSystem>().stateofenemy == "DeadbyMeleeWep")
                {
                    SCTrack.DeathBYMELTHROW();
                    isdeadalready = true;
                     Dead();
                }
            }
            
           
        }
    }

    private void Patrol()
    {
        agent.stoppingDistance = 0f;

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
        agent.stoppingDistance = 0.7f;

        Collider2D collider = Physics2D.OverlapCircle(cast.transform.position, detectionDistance, obstacleMask);
        if (collider != null)
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
                if (Vector3.Distance(transform.position, randomPosition) < 20f)
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
        agent.stoppingDistance = 0.05f;

        if (Vector3.Distance(transform.position, originalPos) > 0.01f)
        {
            GoToDestination(originalPos);
            if (Vector2.Distance(transform.position, originalPos) < 0.01f && agent.velocity.magnitude == 0)
            {
                agent.transform.rotation = Quaternion.Euler(0, 0, originalRotation);
            }
        }
    }

    #region be tested (Roam Behaviour)
    internal void NewRandomPosition(Vector3 newPos)
    {
        newPos = transform.position + new Vector3(Random.Range(-randomDist, randomDist), Random.Range(-randomDist, randomDist), 0);
    }
    #endregion

    private void Dead()
    {
        SpriteManager(enemyState.dead);

        Vector2 fallDir = (player.position - transform.position).normalized;
        float zAxis = Mathf.Atan2(fallDir.y, fallDir.x) * Mathf.Rad2Deg - 90f;
       // rb.transform.rotation = Quaternion.Euler(0, 0, zAxis);

        var power = 2f;
       // rb.AddForce(-fallDir * power, ForceMode2D.Impulse);
        rb.drag = 5f;
        theWeapon = null;
        ClearChildren(weaponPos.gameObject);
        //Destroy(gameObject.GetComponent<Rigidbody2D>());

        Destroy(gameObject.GetComponent<NavMeshAgent>());
        Destroy(gameObject.GetComponent<Collider2D>());

       Destroy(gameObject.GetComponent<EnemyController>());
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
        agent.stoppingDistance = 0.05f;

        StartCoroutine(GoBackToIdle());
        GoToDestination(siteToInspect);

        if (soundState == inspectStates.sight)
        if (soundState == inspectStates.sight || soundState == inspectStates.sound)
        {
            if (Vector2.Distance(transform.position, siteToInspect) < 0.1f)
            {
                hasHeardPlayer = false;
                baseState = enemyState.idle;
            }
        }
        else if (soundState == inspectStates.sound)
        {
            if (Vector2.Distance(transform.position, siteToInspect) < 0.1f)

                if (hasWeapon)
                {
                    StartCoroutine(SearchTime());
                }
                else
                {
                    baseState = enemyState.lookForWeapon;
                }
            }
        

        //handle the hasseeenplayer boolean
        if (agent.velocity == Vector3.zero && foundTargets.Count <= 0)
        {
            hasSeenPlayer = false;
        }
    }

    private IEnumerator GoBackToIdle()
    {
        if (hasSeenPlayer)
        {
            yield return new WaitUntil (() => !hasSeenPlayer);
        }

        if (hasHeardPlayer)
        {
            yield return new WaitUntil(() => !hasHeardPlayer);
        }

        yield return new WaitForSeconds(10f);

        if (theWeapon == null)
        {
            baseState = enemyState.lookForWeapon;
        }
        else
        {
            StartCoroutine(SearchTime());
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
                SpriteRenderer sp = kiaSprite.GetComponent<SpriteRenderer>();
                if (deathBy == "Knife")
                    sp.GetComponent<SpriteRenderer>().sprite = deadBodySprites.stabbed;
                else if (deathBy == "Gun")
                    sp.sprite = deadBodySprites.headShot;
                if (deathBy == "Chainsaw")
                    sp.sprite = deadBodySprites.gutsOut;
                else if (deathBy == "Shotgun")
                    sp.sprite = deadBodySprites.limbsOff;
                else if (deathBy == "Sword")
                    sp.sprite = deadBodySprites.inHalf;
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
            if (hasSeenPlayer)
            {
                yield return new WaitUntil(() => !hasSeenPlayer);
            }

            if (hasHeardPlayer)
            {
                yield return new WaitUntil(() => !hasHeardPlayer);
            }

            yield return null;
            baseState = enemyState.idle;
            state = idleStates.roamer;
            randomDist = 3f;
            timeSearched += Time.deltaTime;
        }

        state = currentState;
        randomDist = defaultRandomDist;
    }

    internal bool CheckPathStatus(Vector3 destination)
    {
        if (agent.enabled)
        {
            agent.CalculatePath(destination, path);

            switch (path.status)
            {
                case NavMeshPathStatus.PathComplete:
                    intel = true;
                    break;
                case NavMeshPathStatus.PathPartial:
                    intel = false;
                    break;
                case NavMeshPathStatus.PathInvalid:
                    intel = false;
                    break;
            }
        }
        return intel;
    }

    private void LookForWeapon()
    {
        if (weaponInRange.Count > 0)
        {
            float closestDistance = Mathf.Infinity;
            Transform closestElement = null;

            foreach (var weapon in weaponInRange)
            {
                if (CheckPathStatus(weapon.transform.position))
                {
                    float distance = Vector2.Distance(transform.position, weapon.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestElement = weapon.transform;
                    }
                }
            }

            GameObject nearestWeapon = closestElement.gameObject;
            GoToDestination(nearestWeapon.transform.position);

            if (Vector2.Distance(transform.position, nearestWeapon.transform.position) < 0.5f)
            {
                theWeapon = nearestWeapon;
                hasWeapon = true;
                baseState = enemyState.idle;
                weaponAttributes = theWeapon.GetComponent<WeaponAttributes>();

                if (weaponAttributes != null)
                {
                    weaponAttributes.AnnounceSelf();
                }
            }
        }
    }

    
    
}

