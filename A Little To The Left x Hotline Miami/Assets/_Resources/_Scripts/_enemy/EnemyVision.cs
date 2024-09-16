using System.Collections;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public float viewRadius;
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    private void Start()
    {
        StartCoroutine(FindTarget());
    }

    private IEnumerator FindTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(.2f);
            VisblePlayer();
        }
    }

    private void VisblePlayer()
    {
        Collider2D[] visibleTargets = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        for (int i = 0; i < visibleTargets.Length; i++)
        {
            Transform target = visibleTargets[i].transform;
            Vector2 dirToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.up, dirToTarget) < viewAngle/ 2f)
            {
                float distance = Vector2.Distance (transform.position, target.position);

                if(!Physics2D.Raycast (transform.position, dirToTarget, distance, obstacleMask))
                {
                    print("found");
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool isAngleGlobal)
    {
        if (!isAngleGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }
}
