using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkIfAllEnemiesDead : MonoBehaviour
{
    private EnemyController[] enemyControllers= new EnemyController[10];
    void Start()
    {
        GameObject[] eachEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < eachEnemy.Length; i++) 
        {
            if (eachEnemy[i].GetComponent<EnemyController>() != null)
                enemyControllers[i]= eachEnemy[i].GetComponent<EnemyController>();
        }
    }

    // Update is called once per frame
    public void checkEnemies()
    {
        for(int i = 0;i < enemyControllers.Length;i++)
        {
            if (enemyControllers[i] != null)
                return;
            Debug.Log("All enemies are dead");
        }
    }
}
