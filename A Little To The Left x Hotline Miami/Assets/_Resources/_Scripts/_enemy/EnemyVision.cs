using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public float viewRadius;
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List <Transform> foundTargets = new List<Transform>();

    private EnemyController enemyController;
    public bool hasSeenPlayer;
    private void Start()
    {
        StartCoroutine(FindTarget());
        enemyController = FindObjectOfType<EnemyController>();
        hasSeenPlayer = false;
    }

    private IEnumerator FindTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(.2f);
            FOV();
        }
    }

    private void Update()
    {
        HasSeenPlayer();
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

            // Ensure the target is within the angle and distance radius
            if (Vector2.Angle(transform.up, dirToTarget) < viewAngle / 2f && distanceToTarget <= viewRadius)
            {
                if (!Physics2D.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask))
                {
                    Debug.DrawLine(transform.position, target.position, Color.red); // This will draw a line to the detected target
                    foundTargets.Add(target);
                }
            }
        }
    }


    private float timeSinceLastSeenPlayer = 0f;
    public float timeBeforeForget = 10f;  // Time before the enemy forgets the player

    private void HasSeenPlayer()
    {
        if (foundTargets.Count > 0)
        {
            if (foundTargets[0].gameObject.tag == "Player")
            {
                enemyController.baseState = EnemyController.enemyState.inspect;
                enemyController.siteToInspect = foundTargets[0].position;
                hasSeenPlayer = true;
                timeSinceLastSeenPlayer = 0f; 
            }
        }
        else
        {
            timeSinceLastSeenPlayer += Time.deltaTime;
            if (timeSinceLastSeenPlayer >= timeBeforeForget)
            {
                hasSeenPlayer = false;
                enemyController.baseState = EnemyController.enemyState.idle;
            }
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
}
