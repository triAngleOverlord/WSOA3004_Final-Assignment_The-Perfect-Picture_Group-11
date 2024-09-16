using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (EnemyVision))]
public class EnemyFOV : Editor
{
    private void OnSceneGUI()
    {
        EnemyVision enemyVision = (EnemyVision)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(enemyVision.transform.position, Vector3.forward, enemyVision.transform.right, 360, enemyVision.viewRadius);

        Vector3 viewAngleA = enemyVision.DirFromAngle(-enemyVision.viewAngle/ 2, false);
        Vector3 viewAngleB = enemyVision.DirFromAngle(enemyVision.viewAngle/ 2, false);


        Handles.DrawLine(enemyVision.transform.position, enemyVision.transform.position + viewAngleA * enemyVision.viewRadius);
        Handles.DrawLine(enemyVision.transform.position, enemyVision.transform.position + viewAngleB * enemyVision.viewRadius);
    }
}
